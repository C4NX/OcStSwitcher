namespace OcStSwitcher
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Timer StatusUpdateTimer;
            System.Windows.Forms.Label titleLabel;
            System.Windows.Forms.GroupBox groupBox1;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.OculusStatusLabel = new System.Windows.Forms.Label();
            this.oculusDashCheckbox = new System.Windows.Forms.CheckBox();
            this.GithubLinkLabel = new System.Windows.Forms.LinkLabel();
            this.ItsKaitlyn03LinkLabel = new System.Windows.Forms.LinkLabel();
            this.descLabel = new System.Windows.Forms.Label();
            this.issuesLinkLabel = new System.Windows.Forms.LinkLabel();
            StatusUpdateTimer = new System.Windows.Forms.Timer(this.components);
            titleLabel = new System.Windows.Forms.Label();
            groupBox1 = new System.Windows.Forms.GroupBox();
            groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // StatusUpdateTimer
            // 
            StatusUpdateTimer.Enabled = true;
            StatusUpdateTimer.Interval = 3000;
            StatusUpdateTimer.Tick += new System.EventHandler(this.StatusTimer_Tick);
            // 
            // titleLabel
            // 
            titleLabel.Dock = System.Windows.Forms.DockStyle.Top;
            titleLabel.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            titleLabel.Location = new System.Drawing.Point(0, 0);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new System.Drawing.Size(790, 61);
            titleLabel.TabIndex = 3;
            titleLabel.Text = "OcSt Switcher";
            titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(this.OculusStatusLabel);
            groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            groupBox1.Location = new System.Drawing.Point(0, 149);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(790, 90);
            groupBox1.TabIndex = 4;
            groupBox1.TabStop = false;
            groupBox1.Text = "Status";
            // 
            // OculusStatusLabel
            // 
            this.OculusStatusLabel.AutoSize = true;
            this.OculusStatusLabel.Location = new System.Drawing.Point(6, 19);
            this.OculusStatusLabel.Name = "OculusStatusLabel";
            this.OculusStatusLabel.Size = new System.Drawing.Size(79, 15);
            this.OculusStatusLabel.TabIndex = 5;
            this.OculusStatusLabel.Text = "Oculus Status";
            // 
            // oculusDashCheckbox
            // 
            this.oculusDashCheckbox.AutoSize = true;
            this.oculusDashCheckbox.Location = new System.Drawing.Point(361, 124);
            this.oculusDashCheckbox.Name = "oculusDashCheckbox";
            this.oculusDashCheckbox.Size = new System.Drawing.Size(63, 19);
            this.oculusDashCheckbox.TabIndex = 1;
            this.oculusDashCheckbox.Text = "Oculus";
            this.oculusDashCheckbox.UseVisualStyleBackColor = true;
            // 
            // GithubLinkLabel
            // 
            this.GithubLinkLabel.AutoSize = true;
            this.GithubLinkLabel.Location = new System.Drawing.Point(6, 32);
            this.GithubLinkLabel.Name = "GithubLinkLabel";
            this.GithubLinkLabel.Size = new System.Drawing.Size(101, 15);
            this.GithubLinkLabel.TabIndex = 5;
            this.GithubLinkLabel.TabStop = true;
            this.GithubLinkLabel.Text = "This tool by C4NX";
            this.GithubLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.GithubLinkLabel_LinkClicked);
            // 
            // ItsKaitlyn03LinkLabel
            // 
            this.ItsKaitlyn03LinkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ItsKaitlyn03LinkLabel.AutoSize = true;
            this.ItsKaitlyn03LinkLabel.Location = new System.Drawing.Point(6, 9);
            this.ItsKaitlyn03LinkLabel.Name = "ItsKaitlyn03LinkLabel";
            this.ItsKaitlyn03LinkLabel.Size = new System.Drawing.Size(181, 15);
            this.ItsKaitlyn03LinkLabel.TabIndex = 6;
            this.ItsKaitlyn03LinkLabel.TabStop = true;
            this.ItsKaitlyn03LinkLabel.Text = "Oculus Dash Killer by ItsKaitlyn03";
            this.ItsKaitlyn03LinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ItsKaitlyn03LinkLabel_LinkClicked);
            // 
            // descLabel
            // 
            this.descLabel.AutoSize = true;
            this.descLabel.Location = new System.Drawing.Point(6, 61);
            this.descLabel.Name = "descLabel";
            this.descLabel.Size = new System.Drawing.Size(728, 45);
            this.descLabel.TabIndex = 7;
            this.descLabel.Text = resources.GetString("descLabel.Text");
            // 
            // issuesLinkLabel
            // 
            this.issuesLinkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.issuesLinkLabel.AutoSize = true;
            this.issuesLinkLabel.Location = new System.Drawing.Point(699, 131);
            this.issuesLinkLabel.Name = "issuesLinkLabel";
            this.issuesLinkLabel.Size = new System.Drawing.Size(79, 15);
            this.issuesLinkLabel.TabIndex = 8;
            this.issuesLinkLabel.TabStop = true;
            this.issuesLinkLabel.Text = "Report issues.";
            this.issuesLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.issuesLinkLabel_LinkClicked);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 239);
            this.Controls.Add(this.issuesLinkLabel);
            this.Controls.Add(this.descLabel);
            this.Controls.Add(this.oculusDashCheckbox);
            this.Controls.Add(this.ItsKaitlyn03LinkLabel);
            this.Controls.Add(this.GithubLinkLabel);
            this.Controls.Add(groupBox1);
            this.Controls.Add(titleLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "OcSt Switcher";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private CheckBox oculusDashCheckbox;
        private Label OculusStatusLabel;
        private LinkLabel GithubLinkLabel;
        private LinkLabel ItsKaitlyn03LinkLabel;
        private Label descLabel;
        private LinkLabel issuesLinkLabel;
    }
}