namespace ScriptHost
{
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.Windows.Forms;
    using Services;

    public partial class Form1 : Form
    {
        private readonly ScriptHostService scriptHostService = new ScriptHostService();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.scriptHostService.AnyValueExists())
                {
                    if (!this.scriptHostService.IsValueKindDword())
                    {
                        scriptHostService.Disable();
                        MessageBox.Show("Your configuration has been set to Disabled.", "Configuration Corrupt!",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    if (!this.scriptHostService.IsConfigMatching())
                    {
                        scriptHostService.Disable();
                        MessageBox.Show("Your configuration has been set to Disabled.", "Configuration Mismatch!",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    } 
                }

                ToggleInterfaceStatus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("It looks like something went wrong! Here is the error message: {0}",  ex.Message), "Ooops!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }
        }

        private void ScriptHostToggleButton_Click(object sender, EventArgs e)
        {
            if (scriptHostService.IsEnabled())
            {
                scriptHostService.Disable();
            }
            else
            {
                scriptHostService.Enable();
            }

            ToggleInterfaceStatus();
        }

        private void profileLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://twitter.com/TeamRocketOps");
        }


        private void ToggleInterfaceStatus()
        {
            var isEnabled = scriptHostService.IsEnabled();
            this.ScriptHostEnabledLabel.ForeColor = isEnabled ? Color.Red : Color.ForestGreen;
            this.ScriptHostEnabledLabel.Text = isEnabled ? "Enabled" : "Disabled";
        }
    }
}
