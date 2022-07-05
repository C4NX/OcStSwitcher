using OcStSwitcher.Exceptions;
using System.Diagnostics;

namespace OcStSwitcher
{
    public partial class MainForm : Form
    {
        #region Consts

        public const string GitHubUrl = "https://github.com/C4NX";
        public const string OculusDashKillerAuthorGithubUrl = "https://github.com/ItsKaitlyn03";
        public const string OculusDashKillerUrl = "https://github.com/ItsKaitlyn03/OculusKiller/releases/latest/download/OculusDash.exe";
        public const string IssuesGithub = "https://github.com/C4NX/OcStSwitcher/issues";

        #endregion

        #region Fields

        private readonly HttpClient httpClient = new ();
        public static string OculusDashPath => Path.Combine(VRApps.OculusPath ?? throw new NullReferenceException("Oculus Path cannot be null"), @"oculus-dash\dash\bin\");

        #endregion

        #region Constructor

        public MainForm()
        {
            try
            {
                VRApps.SearchOculus(true);
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Oculus could not be found on your device, aborting...", "Error");
                Environment.Exit(-1);
            }

            VRApps.SearchSteamVR(true);

            InitializeComponent();

            oculusDashCheckbox.Checked = IsNormalDash();
            oculusDashCheckbox.CheckedChanged += OculusDashCheckbox_CheckedChanged;

            Text = $"OcSt Switcher → {(VRApps.OculusPath != null ? "Oculus: OK" : string.Empty)} | {(VRApps.SteamVRPath != null ? "SteamVR: OK" : "SteamVR: NF")}";
            UpdateOculusStatus();
        }

        #endregion

        #region Utils

        public void UpdateOculusStatus()
        {
            OculusProcRunning(out Process? OVRProc, out Process? dashProc, out Process? runtimeProc);

            OculusStatusLabel.Text = $"OVR: {(OVRProc != null ? $"Running (PID: {OVRProc.Id})" : "Not Started")}\nOculus Dash: {(dashProc != null ? $"Running (PID: {dashProc.Id})" : "Not Started")}\nOculus Runtime: {(runtimeProc != null ? $"Running (PID: {runtimeProc.Id})" : "Not Started")}\nOpenXR Runtime: {SVR.OpenXRRuntimePath} ({SVR.GetVRRuntime()})";
        }

        public static void OculusProcRunning(out Process? ovr, out Process? dash, out Process? runtime)
        {
            Process[] processes = Process.GetProcesses();
            ovr = processes.Where((process) => process.ProcessName.StartsWith("OVRServer", StringComparison.InvariantCultureIgnoreCase))
                .FirstOrDefault();
            dash = processes.Where((process) => process.ProcessName.Contains("OculusDash", StringComparison.InvariantCultureIgnoreCase))
                .FirstOrDefault();
            runtime = processes.Where((process) => process.ProcessName.Contains("oculus-platform-runtime", StringComparison.InvariantCultureIgnoreCase))
                .FirstOrDefault();
        }

        public static void SteamVRProcRunning()
        {
            //TODO: Steam VR proc info
        }

        public static bool IsNormalDash() => new FileInfo(Path.Combine(OculusDashPath, "OculusDash.exe")).Length > 1000000;

        #endregion

        #region Form Events

        private void OculusDashCheckbox_CheckedChanged(object? sender, EventArgs e)
        {
            if (!oculusDashCheckbox.Enabled)
                return;

            oculusDashCheckbox.Enabled = false;

            if (!IsNormalDash() && !File.Exists(OculusDashPath + "OculusDash.exe.bak"))
            {
                MessageBox.Show("Oculus dash backup cannot be found, usually a reinstall of the oculus software is necessary to solve the problem.");
                oculusDashCheckbox.Checked = !oculusDashCheckbox.Checked;
                oculusDashCheckbox.Enabled = true;
                return;
            }

            Task.Run(ToggleOculusAsync).ContinueWith((task) =>
            {
                if (task.IsFaulted)
                {
                    if (task.Exception?.InnerException is UnauthorizedAccessException accessException)
                    {
                        MessageBox.Show($"This software does not have sufficient rights to access the Oculus/SteamVR directory.\n({accessException.Message})", "Error");
                    }
                    else
                    {
                        MessageBox.Show($"An unknown error occurred.\n{task.Exception?.Message}", "Error");
                    }

                    Invoke(() =>
                    {
                        oculusDashCheckbox.Checked = !oculusDashCheckbox.Checked;
                        oculusDashCheckbox.Enabled = true;
                    });
                }
                else
                {
                    Invoke(() =>
                    {
                        oculusDashCheckbox.Enabled = true;
                    });
                }
            });
        }

        private void GithubLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => Process.Start(new ProcessStartInfo(GitHubUrl) { UseShellExecute = true });
        private void ItsKaitlyn03LinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => Process.Start(new ProcessStartInfo(OculusDashKillerAuthorGithubUrl) { UseShellExecute = true });
        private void issuesLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => Process.Start(new ProcessStartInfo(IssuesGithub) { UseShellExecute = true });

        private void StatusTimer_Tick(object sender, EventArgs e) => UpdateOculusStatus();

        #endregion

        #region Main Task

        public async Task ToggleOculusAsync()
        {
            OculusProcRunning(out Process? OVRProc, out Process? dashProc, out Process? runtimeProc);

            // close the runtime first, dash will respawn if not.
            if (runtimeProc != null && !runtimeProc.HasExited)
            {
                runtimeProc.Kill();
                runtimeProc.WaitForExit();
            }
            if (dashProc != null && !dashProc.HasExited)
            {
                dashProc.Kill();
                dashProc.WaitForExit();
            }

            if (oculusDashCheckbox.Checked)
            {
                File.Move(Path.Combine(OculusDashPath, "OculusDash.exe.bak"), Path.Combine(OculusDashPath, "OculusDash.exe"), true);

                try
                {
                    SVR.SetVRRuntime(SVR.OpenXRRuntime.Oculus);
                }
                catch (XRRuntimeNotFoundException ex)
                {
                    MessageBox.Show(ex.Message, "SVR Error");
                }
                try
                {
                    if (SVR.InitSteamVR())
                    {
                        SVR.SteamBlockOculusSDK = false;
                        SVR.CloseSteamVR();
                    }
                }
                catch (OpenVRException ex)
                {
                    MessageBox.Show(ex.Message, "OpenVR Error");
                }
            }
            else
            {
                if (IsNormalDash())
                    File.Copy(Path.Combine(OculusDashPath, "OculusDash.exe"), Path.Combine(OculusDashPath, "OculusDash.exe.bak"), true);

                using FileStream dashStream = File.Create(Path.Combine(OculusDashPath, "OculusDash.exe"));
                using Stream webDashKillerStream = await httpClient.GetStreamAsync(OculusDashKillerUrl);

                await webDashKillerStream.CopyToAsync(dashStream);

                SVR.SetVRRuntime(SVR.OpenXRRuntime.SteamVR);
                if (SVR.InitSteamVR())
                {
                    SVR.SteamBlockOculusSDK = true;
                    SVR.CloseSteamVR();
                }
            }
        }

        #endregion
    }
}