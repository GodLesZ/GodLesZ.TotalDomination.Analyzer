using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GodLesZ.TotalDomination.Analyzer.Library;
using GodLesZ.TotalDomination.Library;

namespace GodLesZ.TotalDomination.AnalyzerGui {

    public partial class FormClan : Form {

        public FormClan() {
            InitializeComponent();

            LoadClanInfo();
        }

        private void LoadClanInfo() {
            var dataTable = new DataTable();
            dataTable.Columns.Add("c");
            dataTable.Columns.Add("j");
            dataTable.Columns.Add("i");
            dataTable.Columns.Add("r");
            dataTable.Columns.Add("u");

            foreach (var packet in DeviceListener.Instance.Packets) {
                var response = packet as HttpResponsePacket;
                if (response == null || response.BodyJson == null) {
                    continue;
                }

                var json = response.BodyJson;
                var properties = (json.GetType() as Type).GetProperties();
                if (properties.Any(p => p.Name == "i") == false || properties.Any(p => p.Name == "n") == false || properties.Any(p => p.Name == "d") == false) {
                    continue;
                }

                Name = string.Format("Clan: {0} (#{1})", json.n, json.i);
                if (json.d.m == null || json.d.m.m == null) {
                    dataTable.Rows.Add("No member data found");
                    break;
                }

                var i = 0;
                foreach (var row in json.d.m.m) {
                    if (row.c == null) {
                        break;
                    }

                    dataTable.Rows.Add(
                        i++,
                        row.c,
                        row.j,
                        row.i,
                        row.r,
                        row.u
                    );
                }

                break;
            }

            dataGrid.DataSource = dataTable;
        }

    }

}
