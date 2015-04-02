using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using GodLesZ.TotalDomination.Analyzer.Library.Json;
using PacketDotNet;
using SharpPcap;

namespace GodLesZ.TotalDomination.Analyzer.Library {

    public class HttpResponsePacket : HttpPacket {

        /// <summary>
        /// JsonArray or JsonObject
        /// </summary>
        public dynamic BodyJson {
            get;
            protected set;
        }

        public HttpRequestPacket RequestPacket {
            get;
            set;
        }

        public string HeaderContentEncoding {
            get { return mHeaderData.ContainsKey("Content-Encoding") ? mHeaderData["Content-Encoding"].ToLower() : ""; }
        }


        public HttpResponsePacket(RawCapture packet)
            : base(packet) {

            Parse();
        }


        public bool IsGzipEncoded() {
            return HeaderContentEncoding == "gzip";
        }

        public override void Dump() {
            if (RequestPacket == null) {
                base.Dump();
                return;
            }

            Console.WriteLine("== Response Info ===============================");
            Console.WriteLine("Requested: {0}", RequestPacket.HeaderServerMethod);
            Console.WriteLine("== Body ===============================");
            Console.WriteLine(BodyString);
            Console.WriteLine("=======================================");
            Console.WriteLine();
        }


        protected void Parse() {
            var packetData = HttpEncoding.GetString(mRawCapture.Data);
            var matchRequestData = Regex.Match(
                packetData,
                @"HTTP/1\.1 (?<status>[0-9]{3} [^\r\n]+)(?<header>.*Content-Length: [0-9]+)(?<body>.*)$",
                RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture
            );

            RequestHost = ((IPv4Packet)_ethernetPacket.PayloadPacket).SourceAddress.ToString();
            var responseStatus = matchRequestData.Groups["status"].Value.Trim();
            var responseHeader = matchRequestData.Groups["header"].Value.Trim();
            var responseBody = matchRequestData.Groups["body"].Value.Trim();

            ParseHeader(responseHeader);
            ParseBody(responseBody);
        }

        protected override void ParseBody(string data) {
            if (IsGzipEncoded()) {
                data = TryDeflateDecoded(data);
            }
            // Some weired hash leading..
            var jsonString = Regex.Replace(data, @"\![a-z0-9]+$", "", RegexOptions.IgnoreCase);
            // In case of BOM of doom..
            jsonString = Regex.Replace(jsonString, "^ï»¿", "");
            if (jsonString.Length == 0) {
                BodyJson = null;
                BodyString = "";
                return;
            }

            // ... check for missing value..
            if (jsonString.Length > 0 && jsonString.Substring(jsonString.Length - 1, 1) == ":") {
                // Fix it with zero for now...
                jsonString += "0";
            }

            // Omfg.. seems like the removed the closing tags? Try to read it with a custom parser
            BodyJson = JsonParser.Deserialize(jsonString);
            BodyString = BodyJson.ToString();
        }

        protected string TryDeflateDecoded(string data) {
            var compressedBuffer = HttpEncoding.GetBytes(data);

            // Expected (gzip/deflate magic bytes)
            // 0x1F, 0x8B, 0x08
            //   31,  139,    8

            // UTF8: Got
            // 0x1F, 0xEF, 0xBF, 0xBD, 0x08, 0x00.., 0x04
            //   31,  239,  191, 189,     8, (5 times) 0, 4, 0

            // ASCII: Got
            // 0x1F, 0x3F, 0x08, 0x00.., 0x04
            //   31,  63,   8, (5 times) 0, 4, 0

            // Got it, encoding needs to be iso-8859-1 WTF ...

            // Hacky but works: They strip the GZIP trailer.. so we skip the GZIP header (10 bytes) and just decompress the deflate data 
            try {
                const int gzipHeaderSize = 10;
                var deflateBuffer = new byte[compressedBuffer.Length - gzipHeaderSize];
                Buffer.BlockCopy(compressedBuffer, gzipHeaderSize, deflateBuffer, 0, deflateBuffer.Length);
                var decompressedBuffer = Ionic.Zlib.DeflateStream.UncompressBuffer(deflateBuffer);
                var decompressedData = HttpEncoding.GetString(decompressedBuffer);
                return decompressedData;
            } catch { }

            return string.Empty;
        }

    }

}