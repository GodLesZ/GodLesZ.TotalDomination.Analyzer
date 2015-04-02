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
using GodLesZ.TotalDomination.Library.Api.Http;

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

            var builder = RequestBuilder.Instance;
            var response = builder.Post("493", new Dictionary<string, string> {
                {"server-method", "GetAlliance"}
            });


            dataGrid.DataSource = dataTable;
        }

    }

}
