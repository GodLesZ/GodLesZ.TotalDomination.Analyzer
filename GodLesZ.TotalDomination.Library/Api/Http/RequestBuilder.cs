using System;
using System.Collections.Generic;
using System.Linq;
using GodLesZ.Library.Network.WebRequest;
using GodLesZ.TotalDomination.Analyzer.Library;

namespace GodLesZ.TotalDomination.Library.Api.Http {

    public class RequestBuilder {
        protected static RequestBuilder _instance;

        protected RequestHelper _request;

        public static RequestBuilder Instance {
            get { return _instance ?? (_instance = new RequestBuilder()); }
        }

        public bool IsInitialized {
            get;
            protected set;

        }

        public string TargetUrl {
            get;
            set;
        }


        protected RequestBuilder() {
            _request = new RequestHelper();
            _request.Headers.Clear();

            MergeHeaders(new Dictionary<string, string>{
                {"User-Agent", "libcurl/7.26.0 OpenSSL/1.0.1h zlib/1.2.8"},
                {"Accept", "*/*"},
                {"Accept-Encoding", "gzip,deflate"},
                {"Content-type", "text/html"},
                {"Connection", ""},
                {"locale-name", "de-DE"},
                {"platform-id", "3"}
            });
        }


        /// <summary>
        /// Grabs the basic request headers from the first valid response packet from capture device
        /// </summary>
        public void Initialize(HttpRequestPacket requestPacket) {

            MergeHeaders(new Dictionary<string, string> {
                {"signin-session", requestPacket.HeaderSigninSession},
                {"signin-userId", requestPacket.HeaderSigninUserId},
                {"signin-authKey", requestPacket.HeaderSigninAuthKey},
                {"sign-code", requestPacket.HeaderSignCode},
                {"client-ver", requestPacket.HeaderClientVersion},
                {"platform-id", requestPacket.HeaderPlatformId},
                {"locale-name", requestPacket.HeaderClientLocaleName},
            });
            TargetUrl = "http://" + requestPacket.RequestHost + requestPacket.RequestUrl;

            IsInitialized = true;
        }


        public void MergeHeaders(IEnumerable<KeyValuePair<string, string>> headers) {
            _request.MergeHeaders(headers);
        }


        public RequestResult Post(string data, IEnumerable<KeyValuePair<string, string>> additionalHeaders = null) {
            if (IsInitialized == false) {
                throw new Exception("Please call Initialize() first");
            }
            if (string.IsNullOrEmpty(TargetUrl)) {
                throw new Exception("Target url cant be empty");
            }

            if (additionalHeaders != null) {
                MergeHeaders(additionalHeaders);
            }

            _request.AutoRedirect = false;
            _request.IgnoreCookies = true;
            _request.Type = ERequestType.Post;

            return _request.RequestWithData(TargetUrl, data);
        }

    }

}