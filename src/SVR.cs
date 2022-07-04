using Microsoft.Win32;
using OcStSwitcher.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Valve.VR;

namespace OcStSwitcher
{
    public static class SVR
    {
        private static CVRSystem? CVRSystem { get; set; }

        public static string? OpenXRRuntimePath
        {
            get
            {
                return (string?)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Khronos\OpenXR\1", "ActiveRuntime", null);
            }
            set
            {
                if(value == null)
                    throw new ArgumentNullException(nameof(value));

                 Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Khronos\OpenXR\1", "ActiveRuntime", value);
            }
        }

        public static bool SteamBlockOculusSDK
        {
            get
            {
                EVRSettingsError errorResult = EVRSettingsError.None;
                var boolResult = OpenVR.Settings.GetBool(OpenVR.k_pch_SteamVR_Section, OpenVR.k_pch_SteamVR_BlockOculusSDKOnAllLaunches_Bool, ref errorResult);

                if (errorResult != EVRSettingsError.None)
                    throw new OpenVRException($"An error occurred while resolving OpenVR.k_pch_SteamVR_BlockOculusSDKOnAllLaunches_Bool");

                return boolResult;
            }
            set
            {
                EVRSettingsError errorResult = EVRSettingsError.None;
                OpenVR.Settings.SetBool(OpenVR.k_pch_SteamVR_Section, OpenVR.k_pch_SteamVR_BlockOculusSDKOnAllLaunches_Bool, value, ref errorResult);

                if (errorResult != EVRSettingsError.None)
                    throw new OpenVRException($"An error occurred while setting OpenVR.k_pch_SteamVR_BlockOculusSDKOnAllLaunches_Bool to {value}");
            }
        }

        public static bool InitSteamVR()
        {
            EVRInitError errorResult = EVRInitError.None;
            CVRSystem = OpenVR.Init(ref errorResult, EVRApplicationType.VRApplication_Other);

            if (errorResult != EVRInitError.None) {
                MessageBox.Show($"Steam VR Error: {Marshal.PtrToStringUTF8(OpenVRInterop.GetStringForHmdError(errorResult))}", "SVR Error");
                return false;
            }

            return CVRSystem != null;
        }

        public static void CloseSteamVR()
        {
            if (CVRSystem != null)
            {
                CVRSystem = null;
                OpenVR.Shutdown();

#if DEBUG
                Debug.WriteLine("SteamVR Shutdown");
#endif

            }
        }

        public static void SetVRRuntime(OpenXRRuntime runtime)
        {
            if (GetVRRuntime() == runtime)
                return;

            string? platformRuntime = null;

            switch (runtime)
            {
                case OpenXRRuntime.Oculus:

                    var oculusRuntimePath = Path.Combine(VRApps.SearchOculus(false), @"oculus-runtime\");

                    if (!Directory.Exists(oculusRuntimePath))
                        throw new XRRuntimeNotFoundException($@"'oculus-runtime\' was not found in '{VRApps.OculusPath}'");

                    platformRuntime = Directory.EnumerateFiles(oculusRuntimePath, "oculus_openxr_*.json")
                                                                        .Where((e) => FastResolveVRRuntimeArchitecture(e) == RuntimeInformation.OSArchitecture)
                                                                        .FirstOrDefault();
                    OpenXRRuntimePath = platformRuntime ?? throw new XRRuntimeNotFoundException($"No Oculus VR runtime found for {RuntimeInformation.OSArchitecture}");

                    break;
                case OpenXRRuntime.SteamVR:
                    var steamvrRuntimePath = VRApps.SearchSteamVR(false);

                    platformRuntime = Directory.EnumerateFiles(steamvrRuntimePath, "steamxr_*.json")
                        .Where((e) => FastResolveVRRuntimeArchitecture(e) == RuntimeInformation.OSArchitecture)
                        .FirstOrDefault();

                    OpenXRRuntimePath = platformRuntime ?? throw new XRRuntimeNotFoundException($"No SteamVR runtime found for {RuntimeInformation.OSArchitecture}");

                    break;
                default:
                    throw new ArgumentException($"{runtime} is not a valid runtime");
            }

#if DEBUG
                Debug.WriteLine($"Set OpenXR Runtime to {platformRuntime} (found for {RuntimeInformation.OSArchitecture})");
#endif
        }

        public static OpenXRRuntime GetVRRuntime()
        {
            var path = OpenXRRuntimePath ?? throw new ArgumentException($"Registry key not found for {nameof(OpenXRRuntimePath)}");

            if (path.Contains(@"oculus-runtime\oculus_openxr_", StringComparison.InvariantCultureIgnoreCase))
                return OpenXRRuntime.Oculus;
            else if (path.Contains(@"SteamVR\steamxr_", StringComparison.InvariantCultureIgnoreCase))
                return OpenXRRuntime.SteamVR;

            return OpenXRRuntime.None;
        }

        private static Architecture? FastResolveVRRuntimeArchitecture(string path)
        {
            if (path.Contains("64"))
                return Architecture.X64;
            else if (path.Contains("32"))
                return Architecture.X86;

            return null;
        }

        public enum OpenXRRuntime
        {
            None,
            Oculus,
            SteamVR
        }
    }
}
