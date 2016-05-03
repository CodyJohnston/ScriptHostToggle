namespace ScriptHost.Services
{
    using Microsoft.Win32;

    public class ScriptHostService
    {
        /// <summary>
        /// Instance of the registryService.
        /// </summary>
        private readonly RegistryService registryService;

        /// <summary>
        /// Path to Windows Scripting Host subkey in the registry.
        /// </summary>
        private const string ScriptHostSettingsSubKey = @"Software\Microsoft\Windows Script Host\Settings";

        /// <summary>
        /// Constructor for the ScriptHostService.
        /// </summary>
        public ScriptHostService()
        {
            this.registryService = new RegistryService();
        }

        /// <summary>
        /// Enables Windows Scripting Host for LocalMachine and CurrentUser. 
        /// This overwrites the current settings.
        /// </summary>
        public void Enable()
        {
            registryService.SetValue(RegistryHive.LocalMachine, ScriptHostSettingsSubKey, "Enabled", 1, RegistryValueKind.DWord);
            registryService.SetValue(RegistryHive.CurrentUser, ScriptHostSettingsSubKey, "Enabled", 1, RegistryValueKind.DWord);
        }

        /// <summary>
        /// Disables Windows Scripting Host for LocalMachine and CurrentUser. 
        /// This overwrites the current settings.
        /// </summary>
        public void Disable()
        {
            registryService.SetValue(RegistryHive.LocalMachine, ScriptHostSettingsSubKey, "Enabled", 0, RegistryValueKind.DWord);
            registryService.SetValue(RegistryHive.CurrentUser, ScriptHostSettingsSubKey, "Enabled", 0, RegistryValueKind.DWord);
        }

        /// <summary>
        /// Checks whether the Windows Scripting Host is enabled.
        /// </summary>
        /// <returns>True if one or both are Enabled.</returns>
        public bool IsEnabled()
        {
            var isEnabled = false;

            if (!AnyValueExists())
            {
                return true;
            }

            var localMachineEnabled = registryService.GetValue(RegistryHive.LocalMachine,
                ScriptHostSettingsSubKey, "Enabled");

            var currentUserEnabled = registryService.GetValue(RegistryHive.CurrentUser,
                ScriptHostSettingsSubKey, "Enabled");

            if (localMachineEnabled != null)
            {
                if (localMachineEnabled.ToString() == "1")
                {
                    isEnabled = true;
                }
            }

            if (currentUserEnabled != null)
            {
                if (currentUserEnabled.ToString() == "1")
                {
                    isEnabled = true;
                }
            }

            return isEnabled;
        }

        /// <summary>
        /// Checks for a match between settings in the CurrentUser and LocalMachine.
        /// </summary>
        /// <returns>True if a match is found.</returns>
        public bool IsConfigMatching()
        {
            var isMatching = false;

            var localMachineEnabled = registryService.GetValue(RegistryHive.LocalMachine,
                ScriptHostSettingsSubKey, "Enabled");

            var currentUserEnabled = registryService.GetValue(RegistryHive.CurrentUser,
                ScriptHostSettingsSubKey, "Enabled");

            if ((localMachineEnabled != null) && (currentUserEnabled != null))
            {
                if (localMachineEnabled == currentUserEnabled)
                {
                    isMatching = true;
                }
            }

            return isMatching;
        }

        /// <summary>
        /// Checks value kinds for CurrentUser and LocalMachine to make sure they are DWORD.
        /// </summary>
        /// <returns>True if CurrentUser and LocalMachine values are of type DWORD.</returns>
        public bool IsValueKindDword()
        {
            var isDword = false;
            
            var localMachineValueKind = registryService.GetValueKind(RegistryHive.LocalMachine,
                ScriptHostSettingsSubKey, "Enabled");

            var currentUserValueKind = registryService.GetValueKind(RegistryHive.CurrentUser,
                ScriptHostSettingsSubKey, "Enabled");

            if ((localMachineValueKind == RegistryValueKind.DWord) &&
                (currentUserValueKind == RegistryValueKind.DWord))
            {
                isDword = true;
            }

            return isDword;
        }

        /// <summary>
        /// Checks to see if any of the config values exist.
        /// </summary>
        /// <returns>True if one or both values exist.</returns>
        public bool AnyValueExists()
        {
            var localMachineValueNames = registryService.GetValueNames(RegistryHive.LocalMachine,
                ScriptHostSettingsSubKey);

            var currentUserValueNames = registryService.GetValueNames(RegistryHive.CurrentUser, 
                ScriptHostSettingsSubKey);

            return (localMachineValueNames.Contains("Enabled") || currentUserValueNames.Contains("Enabled"));
        }
    }
}
