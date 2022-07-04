using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcStSwitcher
{
    public static class VRApps
    {
        public static string? OculusPath {get; private set;}
        public static string? SteamVRPath { get; private set; }

        public static string SearchOculus(bool userPrompt)
        {
            if (OculusPath != null)
            {
#if DEBUG
                Debug.WriteLine($"Use cached oculus path : '{OculusPath}'");
#endif

                return OculusPath;
            }

            var defaultOculusPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), @"Oculus\Support");

            if (Directory.Exists(defaultOculusPath))
            {
                OculusPath = defaultOculusPath;
                return defaultOculusPath;
            }
            else
            {
                if (userPrompt)
                {
                    using FolderBrowserDialog folderBrowserDialog = new() { UseDescriptionForTitle = true, Description = @"Please select the oculus\Support folder" };

                    if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                    {
                        OculusPath = folderBrowserDialog.SelectedPath;
                        return folderBrowserDialog.SelectedPath;
                    }
                }
            }

            throw new FileNotFoundException("Oculus path not found !");
        }

        public static string SearchSteamVR(bool userPrompt)
        {
            if (SteamVRPath != null)
            {
#if DEBUG
                Debug.WriteLine($"Use cached steamvr path : '{SteamVRPath}'");
#endif

                return SteamVRPath;
            }

            var steamvrRuntimePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), @"Steam\steamapps\common\SteamVR");
            if (Directory.Exists(steamvrRuntimePath)) {
                SteamVRPath = steamvrRuntimePath;
                return steamvrRuntimePath;
            }
            else
            {
                if (userPrompt)
                {
                    using FolderBrowserDialog folderBrowserDialog = new() { UseDescriptionForTitle = true, Description = @"Please select the common\SteamVR folder" };

                    if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                    {
                        SteamVRPath = folderBrowserDialog.SelectedPath;
                        return folderBrowserDialog.SelectedPath;
                    }
                }
            }
            throw new FileNotFoundException("SteamVR path not found !");
        }
    }
}
