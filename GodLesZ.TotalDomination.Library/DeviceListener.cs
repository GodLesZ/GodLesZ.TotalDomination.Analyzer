using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using GodLesZ.TotalDomination.Analyzer.Library;
using PacketDotNet;
using SharpPcap;
using SharpPcap.LibPcap;

namespace GodLesZ.TotalDomination.Library {

    public delegate void OnPacketFlowHandler(HttpRequestPacket packet);
    public delegate void OnPacketHandler(HttpPacket packet);

    public class DeviceListener {
        protected static DeviceListener _instance;

        protected string _captureFilter = "host 207.182.155.66 && (port 80 || port 443)";
        protected PcapDevice _device;


        public static DeviceListener Instance {
            get { return _instance ?? (_instance = new DeviceListener()); }
        }

        
        public bool IsActive {
            get;
            protected set;
        }
        public List<HttpPacket> Packets {
            get;
            protected set;
        }

        public event OnPacketFlowHandler OnPacketFlow;
        public event OnPacketHandler OnPacket;
        

        protected DeviceListener() {
            Packets = new List<HttpPacket>();
        }


        public void SwitchDevice(PcapDevice device) {
            if (IsActive) {
                Stop();
            }

            _device = device;
        }

        public void Start() {
            const int readTimeoutMilliseconds = 1000;

            _device.OnPacketArrival += DeviceOnPacketArrival;
            _device.Open(DeviceMode.Promiscuous, readTimeoutMilliseconds);

            _device.Filter = _captureFilter;

            _device.StartCapture();

            IsActive = true;
        }

        public void Stop() {
            if (IsActive == false) {
                return;
            }

            try {
                _device.Close();
            } catch { }

            IsActive = false;
        }


        protected void DeviceOnPacketArrival(object sender, CaptureEventArgs e) {
            if (HttpPacket.IsHttpPacket(e.Packet) == false) {
                var time = e.Packet.Timeval.Date;
                var len = e.Packet.Data.Length;
                var ethPacket = Packet.ParsePacket(e.Packet.LinkLayerType, e.Packet.Data);
                var tcpPacket = (TcpPacket)ethPacket.PayloadPacket.PayloadPacket;

                Console.WriteLine("== TCP Info =======================");
                Console.WriteLine("ACK={0} [{1}], SYN={2}, SEQ-NO={3}", tcpPacket.Ack, tcpPacket.AcknowledgmentNumber, tcpPacket.Syn, tcpPacket.SequenceNumber);
                Console.WriteLine("{0}:{1}:{2},{3} Len={4}", time.Hour, time.Minute, time.Second, time.Millisecond, len);
                Console.WriteLine("===================================");

                return;
            }

            var packet = HttpPacket.Create(e.Packet);
            if (packet == null) {
                return;
            }

            packet.Dump();

            HandleOnPacket(packet);

            Packets.Add(packet);
        }

        protected virtual void HandleOnPacketFlow(HttpRequestPacket packet) {
            OnPacketFlowHandler handler = OnPacketFlow;
            if (handler != null) {
                handler(packet);
            }
        }

        protected virtual void HandleOnPacket(HttpPacket packet) {
            OnPacketHandler handler = OnPacket;
            if (handler != null) {
                handler(packet);
            }
        }

    }

}