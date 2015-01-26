namespace GodLesZ.TotalDomination.AnalyzerGui {
    partial class FormMain {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.menuMainProgram = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMainProgramExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMainTools = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMainToolsWorldmap = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.cmbListenerInterface = new System.Windows.Forms.ToolStripComboBox();
            this.statusStripMain = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuMainToolsClan = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMain.SuspendLayout();
            this.toolStripMain.SuspendLayout();
            this.statusStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuMain
            // 
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuMainProgram,
            this.menuMainTools});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Size = new System.Drawing.Size(809, 24);
            this.menuMain.TabIndex = 0;
            this.menuMain.Text = "menuStrip1";
            // 
            // menuMainProgram
            // 
            this.menuMainProgram.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuMainProgramExit});
            this.menuMainProgram.Name = "menuMainProgram";
            this.menuMainProgram.Size = new System.Drawing.Size(65, 20);
            this.menuMainProgram.Text = "Program";
            // 
            // menuMainProgramExit
            // 
            this.menuMainProgramExit.Name = "menuMainProgramExit";
            this.menuMainProgramExit.Size = new System.Drawing.Size(152, 22);
            this.menuMainProgramExit.Text = "Exit";
            this.menuMainProgramExit.Click += new System.EventHandler(this.menuMainProgramExit_Click);
            // 
            // menuMainTools
            // 
            this.menuMainTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuMainToolsWorldmap,
            this.menuMainToolsClan});
            this.menuMainTools.Name = "menuMainTools";
            this.menuMainTools.Size = new System.Drawing.Size(48, 20);
            this.menuMainTools.Text = "Tools";
            // 
            // menuMainToolsWorldmap
            // 
            this.menuMainToolsWorldmap.Name = "menuMainToolsWorldmap";
            this.menuMainToolsWorldmap.Size = new System.Drawing.Size(152, 22);
            this.menuMainToolsWorldmap.Text = "Worldmap";
            this.menuMainToolsWorldmap.Click += new System.EventHandler(this.menuMainToolsWorldmap_Click);
            // 
            // toolStripMain
            // 
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmbListenerInterface});
            this.toolStripMain.Location = new System.Drawing.Point(0, 24);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.Size = new System.Drawing.Size(809, 25);
            this.toolStripMain.TabIndex = 1;
            this.toolStripMain.Text = "toolStrip1";
            // 
            // cmbListenerInterface
            // 
            this.cmbListenerInterface.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbListenerInterface.DropDownWidth = 200;
            this.cmbListenerInterface.Enabled = false;
            this.cmbListenerInterface.Name = "cmbListenerInterface";
            this.cmbListenerInterface.Size = new System.Drawing.Size(121, 25);
            this.cmbListenerInterface.SelectedIndexChanged += new System.EventHandler(this.cmbListenerInterface_SelectedIndexChanged);
            // 
            // statusStripMain
            // 
            this.statusStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.statusStripMain.Location = new System.Drawing.Point(0, 361);
            this.statusStripMain.Name = "statusStripMain";
            this.statusStripMain.Size = new System.Drawing.Size(809, 22);
            this.statusStripMain.TabIndex = 2;
            this.statusStripMain.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(56, 17);
            this.lblStatus.Text = "Loading..";
            // 
            // menuMainToolsClan
            // 
            this.menuMainToolsClan.Name = "menuMainToolsClan";
            this.menuMainToolsClan.Size = new System.Drawing.Size(152, 22);
            this.menuMainToolsClan.Text = "Clan";
            this.menuMainToolsClan.Click += new System.EventHandler(this.menuMainToolsClan_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(809, 383);
            this.Controls.Add(this.statusStripMain);
            this.Controls.Add(this.toolStripMain);
            this.Controls.Add(this.menuMain);
            this.MainMenuStrip = this.menuMain;
            this.Name = "FormMain";
            this.Text = "Total Domination Analyzer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.statusStripMain.ResumeLayout(false);
            this.statusStripMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuMain;
        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.ToolStripComboBox cmbListenerInterface;
        private System.Windows.Forms.StatusStrip statusStripMain;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.ToolStripMenuItem menuMainProgram;
        private System.Windows.Forms.ToolStripMenuItem menuMainProgramExit;
        private System.Windows.Forms.ToolStripMenuItem menuMainTools;
        private System.Windows.Forms.ToolStripMenuItem menuMainToolsWorldmap;
        private System.Windows.Forms.ToolStripMenuItem menuMainToolsClan;
    }
}

