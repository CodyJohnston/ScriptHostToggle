namespace ScriptHost
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.ScriptHostEnabledLabel = new System.Windows.Forms.Label();
            this.ScriptHostToggleButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.profileLinkLabel = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // ScriptHostEnabledLabel
            // 
            this.ScriptHostEnabledLabel.AutoSize = true;
            this.ScriptHostEnabledLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ScriptHostEnabledLabel.Location = new System.Drawing.Point(95, 19);
            this.ScriptHostEnabledLabel.Name = "ScriptHostEnabledLabel";
            this.ScriptHostEnabledLabel.Size = new System.Drawing.Size(81, 24);
            this.ScriptHostEnabledLabel.TabIndex = 0;
            this.ScriptHostEnabledLabel.Text = "Enabled";
            // 
            // ScriptHostToggleButton
            // 
            this.ScriptHostToggleButton.Location = new System.Drawing.Point(12, 65);
            this.ScriptHostToggleButton.Name = "ScriptHostToggleButton";
            this.ScriptHostToggleButton.Size = new System.Drawing.Size(254, 23);
            this.ScriptHostToggleButton.TabIndex = 1;
            this.ScriptHostToggleButton.Text = "Toggle";
            this.ScriptHostToggleButton.UseVisualStyleBackColor = true;
            this.ScriptHostToggleButton.Click += new System.EventHandler(this.ScriptHostToggleButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(141, 123);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Coded by";
            // 
            // profileLinkLabel
            // 
            this.profileLinkLabel.AutoSize = true;
            this.profileLinkLabel.Location = new System.Drawing.Point(194, 123);
            this.profileLinkLabel.Name = "profileLinkLabel";
            this.profileLinkLabel.Size = new System.Drawing.Size(77, 13);
            this.profileLinkLabel.TabIndex = 3;
            this.profileLinkLabel.TabStop = true;
            this.profileLinkLabel.Text = "Cody Johnston";
            this.profileLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.profileLinkLabel_LinkClicked);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(278, 145);
            this.Controls.Add(this.profileLinkLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ScriptHostToggleButton);
            this.Controls.Add(this.ScriptHostEnabledLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Toggle Scripting Host";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ScriptHostEnabledLabel;
        private System.Windows.Forms.Button ScriptHostToggleButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel profileLinkLabel;
    }
}

