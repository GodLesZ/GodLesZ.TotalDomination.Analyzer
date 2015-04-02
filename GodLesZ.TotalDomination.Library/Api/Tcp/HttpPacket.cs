using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using PacketDotNet;
using SharpPcap;

namespace GodLesZ.TotalDomination.Analyzer.Library {

    public abstract class HttpPacket {
        public static readonly Encoding HttpEncoding = Encoding.GetEncoding("iso-8859-1"); // iso-8859-15 / iso-8859-1

        protected RawCapture mRawCapture;
        protected EthernetPacket _ethernetPacket;
        protected readonly Dictionary<string, string> mHeaderData = new Dictionary<string, string>();


        public TcpPacket BasePacket {
            get;
            protected set;
        }

        public RawCapture RawCapture {
            get { return mRawCapture; }
        }

        public string RequestHost {
            get;
            protected set;
        }

        public string BodyString {
            get;
            protected set;
        }

        public int HeaderContentLength {
            get { return int.Parse(mHeaderData["Content-Length"]); }
        }


        protected HttpPacket(RawCapture packet) {
            _ethernetPacket = (EthernetPacket)Packet.ParsePacket(packet.LinkLayerType, packet.Data);
            mRawCapture = packet;
            BasePacket = (TcpPacket)_ethernetPacket.PayloadPacket.PayloadPacket;
        }


        public static bool IsHttpPacket(RawCapture packet) {
            var packetData = HttpEncoding.GetString(packet.Data);
            if (IsHttpRequest(packetData)) {
                return true;
            }
            if (IsHttpResponse(packetData)) {
                return true;
            }

            return false;
        }

        public static bool IsHttpRequest(string packetData) {
            return Regex.IsMatch(packetData, @"(POST|GET|PUT|OPTIONS|DELETE) ");
        }

        public static bool IsHttpResponse(string packetData) {
            return Regex.IsMatch(packetData, @"HTTP/1\.1 ");
        }

        public static HttpPacket Create(RawCapture packet) {
            var packetData = HttpEncoding.GetString(packet.Data);
            if (IsHttpRequest(packetData)) {
                return new HttpRequestPacket(packet);
            }
            if (IsHttpResponse(packetData)) {
                return new HttpResponsePacket(packet);
            }

            return null;
        }


        public virtual void Dump() {
            var time = mRawCapture.Timeval.Date;
            var len = mRawCapture.Data.Length;

            Console.WriteLine("== {0} Info =======================", (this is HttpRequestPacket ? "Request" : "Response"));
            Console.WriteLine("ACK={0} [{1}], SYN={2}, SEQ-NO={3}", BasePacket.Ack, BasePacket.AcknowledgmentNumber, BasePacket.Syn, BasePacket.SequenceNumber);
            Console.WriteLine("{0}:{1}:{2},{3} Len={4}", time.Hour, time.Minute, time.Second, time.Millisecond, len);
            Console.WriteLine("== Header ===============================");
            foreach (var headerLine in mHeaderData) {
                Console.WriteLine("{0}: {1}", headerLine.Key, headerLine.Value);
            }
            Console.WriteLine("== Body ===============================");
            Console.WriteLine(BodyString);
            Console.WriteLine("=======================================");
            Console.WriteLine();
        }


        protected void ParseHeader(string data) {
            var headerPairs = data.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var headerLine in headerPairs) {
                var headerLinePair = headerLine.Split(new[] { ':' }, 2);
                if (headerLinePair.Length != 2) {
                    continue;
                }

                var headerLineName = headerLinePair[0].Trim();
                var headerLineValue = headerLinePair[1].Trim();
                mHeaderData.Add(headerLineName, headerLineValue);
            }
        }

        protected abstract void ParseBody(string data);

    }

}