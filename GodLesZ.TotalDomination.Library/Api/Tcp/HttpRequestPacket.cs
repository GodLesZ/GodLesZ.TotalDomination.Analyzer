﻿using System;
using System.Text;
using System.Text.RegularExpressions;
using GodLesZ.TotalDomination.Analyzer.Library.Json;
using PacketDotNet;
using SharpPcap;

namespace GodLesZ.TotalDomination.Analyzer.Library {

    public class HttpRequestPacket : HttpPacket {

        /// <summary>
        /// JsonArray or JsonObject
        /// </summary>
        public dynamic BodyJson {
            get;
            protected set;
        }

        public HttpResponsePacket ResponsePacket {
            get;
            set;
        }

        public string RequestUrl {
            get;
            protected set;
        }

        public string RequestType {
            get;
            protected set;
        }

        /// <summary>
        /// 20150122180031.20150123053258.13770
        /// </summary>
        public string HeaderSigninSession {
            get { return GetHeaderSave("signin-session"); }
        }

        /// <summary>
        /// pm3565197
        /// </summary>
        public string HeaderSigninUserId {
            get { return GetHeaderSave("signin-userId"); }
        }

        /// <summary>
        /// 6d551ffd00e723f5003819baaf55cc46
        /// </summary>
        public string HeaderSigninAuthKey {
            get { return GetHeaderSave("signin-authKey"); }
        }

        /// <summary>
        /// GetUserNotesList
        /// </summary>
        public string HeaderServerMethod {
            get { return GetHeaderSave("server-method"); }
        }

        /// <summary>
        /// de-DE
        /// </summary>
        public string HeaderClientLocaleName {
            get { return GetHeaderSave("locale-name"); }
        }

        /// <summary>
        /// 3.9.2
        /// </summary>
        public string HeaderClientVersion {
            get { return GetHeaderSave("client-ver"); }
        }

        /// <summary>
        /// 8304af58ef6d01860fe732a261f15bdd
        /// </summary>
        public string HeaderSignCode {
            get { return GetHeaderSave("sign-code"); }
        }

        /// <summary>
        /// 3
        /// </summary>
        public string HeaderPlatformId {
            get { return GetHeaderSave("platform-id"); }
        }


        public HttpRequestPacket(RawCapture packet)
            : base(packet) {

            Parse();
        }


        protected string GetHeaderSave(string index) {
            return mHeaderData.ContainsKey(index) ? mHeaderData[index] : string.Empty;

        }

        protected void Parse() {
            var packetData = Encoding.UTF8.GetString(mRawCapture.Data);
            var matchRequestData = Regex.Match(
                packetData,
                @"(?<type>POST|GET) (?<url>.+) HTTP/1\.1(?<header>.*Content-Length: [0-9]+)(?<body>.*)$",
                RegexOptions.Singleline | RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture
            );


            RequestHost = ((IPv4Packet)_ethernetPacket.PayloadPacket).DestinationAddress.ToString();
            RequestType = matchRequestData.Groups["type"].Value.Trim().ToUpper();
            RequestUrl = matchRequestData.Groups["url"].Value;
            var requestHeader = matchRequestData.Groups["header"].Value.Trim();
            var requestBody = matchRequestData.Groups["body"].Value.Trim();

            ParseHeader(requestHeader);
            ParseBody(requestBody);
        }

        protected override void ParseBody(string jsonString) {
            try {
                BodyJson = JsonParser.Deserialize(jsonString);
                BodyString = BodyJson.ToString();
            } catch {
                BodyJson = null;
                BodyString = jsonString;
            }
        }

    }

}