using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using GodLesZ.TotalDomination.Library;
using SharpPcap;
using SharpPcap.LibPcap;

namespace GodLesZ.TotalDomination.AnalyzerGui {

    public partial class FormMain : Form {
        protected readonly List<PcapDevice> _devices = new List<PcapDevice>();
        protected int _targetDeviceIndex = -1;

        protected DeviceListener _deviceListener = DeviceListener.Instance;


        public FormMain() {
            InitializeComponent();
        }


        private void FormMain_Load(object sender, EventArgs e) {
            SetStatus("Loading devices");

            var worker = new BackgroundWorker();
            worker.DoWork += delegate {
                var devices = CaptureDeviceList.Instance;
                foreach (var device in devices) {
                    _devices.Add((PcapDevice) device);
                }
            };
            worker.RunWorkerCompleted += delegate {
                if (_devices.Count == 0) {
                    MessageBox.Show("Failed to find a network device!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                cmbListenerInterface.Items.Clear();
                foreach (var dev in _devices) {
                    cmbListenerInterface.Items.Add(string.Format("{0} ({1})", dev.Interface.FriendlyName, dev.Description));
                }
                cmbListenerInterface.Enabled = true;

                SetStatus("Please select a device now");
            };
            worker.RunWorkerAsync();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e) {
            if (_deviceListener.IsActive) {
                SetStatus("Stop device listener ..");
                
                _deviceListener.Stop();
            }

            SetStatus("Done");
        }


        private void cmbListenerInterface_SelectedIndexChanged(object sender, EventArgs e) {
            if (cmbListenerInterface.SelectedIndex == -1) {
                return;
            }

            _targetDeviceIndex = cmbListenerInterface.SelectedIndex;

            SetStatus("Starting device listener ..");

            _deviceListener.SwitchDevice(_devices[_targetDeviceIndex]);
            _deviceListener.Start();

            SetStatus("Device listener started. Please start the game now");
        }


        private void menuMainProgramExit_Click(object sender, EventArgs e) {
            Close();
        }

        private void menuMainToolsWorldmap_Click(object sender, EventArgs e) {
            using (var form = new FormWorldmap()) {
                form.ShowDialog(this);
            }
        }

        private void menuMainToolsClan_Click(object sender, EventArgs e) {
            using (var form = new FormClan()) {
                form.ShowDialog(this);
            }
        }


        private void SetStatus(string message) {
            lblStatus.Text = message;
#if DEBUG
            Console.WriteLine(message);
#endif
        }

    }

}
