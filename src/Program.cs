using System.Diagnostics;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using Valve.VR;

namespace OcStSwitcher
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (!File.Exists("openvr_api.dll"))
            {
                using (FileStream fileStream = File.Create("openvr_api.dll"))
                {
                    using (Stream openVrRessource = typeof(Program).Assembly.GetManifestResourceStream("OcStSwitcher.openvr_api.dll") ?? throw new NullReferenceException())
                    {
                        openVrRessource.CopyTo(fileStream);
                    }
                }
            }

            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }
    }
}