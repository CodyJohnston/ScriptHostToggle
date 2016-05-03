namespace ScriptHost.Services
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Security;
    using Microsoft.Win32;

    public class RegistryService
    {
        /// <summary>
        /// The registry view to use. This ensures that the view is correct for x64.
        /// </summary>
        private readonly RegistryView currentRegistryView;

        /// <summary>
        /// Constructor for RegistryService.
        /// </summary>
        public RegistryService()
        {
            this.currentRegistryView = GetCurrentRegistryView();
        }

        /// <summary>
        /// Gets the data from the specified registry value.
        /// </summary>
        /// <param name="hive">The registry hive to access.</param>
        /// <param name="subKey">The path to the subkey.</param>
        /// <param name="valueName">The name of the value.</param>
        /// <returns>The data from the specified value if it exists.</returns>
        public object GetValue(RegistryHive hive, string subKey, string valueName)
        {
            if (string.IsNullOrEmpty(subKey)) throw new ArgumentNullException("subKey");
            if (string.IsNullOrEmpty(valueName)) throw new ArgumentNullException("valueName");

            try
            {
                using (var baseKey = RegistryKey.OpenBaseKey(hive, this.currentRegistryView))
                {
                    using (var key = baseKey.OpenSubKey(subKey))
                    {
                        if (key != null)
                        {
                            return key.GetValue(valueName);
                        }
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
                Debug.WriteLine("User does not have permission to perform this action.");
            }
            catch (SecurityException)
            {
                Debug.WriteLine("User does not have permission to perform this action.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("GetValue: {0}", ex.Message);
                Debug.WriteLine("Stacktrace: {0}", ex.StackTrace);
            }

            return null;
        }

        /// <summary>
        /// Sets the data for the specified registry value.
        /// </summary>
        /// <param name="hive">The registry hive to access.</param>
        /// <param name="subKey">The path to the subkey.</param>
        /// <param name="valueName">The name of the value.</param>
        /// <param name="valueData">The data to set on the specified value</param>
        /// <param name="valueKind">The value kind of data to set.</param>
        public void SetValue(RegistryHive hive, string subKey, string valueName, object valueData, 
            RegistryValueKind valueKind)
        {
            if (string.IsNullOrEmpty(subKey)) throw new ArgumentNullException("subKey");
            if (string.IsNullOrEmpty(valueName)) throw new ArgumentNullException("valueName");
            if (valueData == null) throw new ArgumentNullException("valueData");

            try
            {
                using (var baseKey = RegistryKey.OpenBaseKey(hive, this.currentRegistryView))
                {
                    using (var key = baseKey.OpenSubKey(subKey, true))
                    {
                        if (key != null)
                        {
                            key.SetValue(valueName, valueData, valueKind);
                        }
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
                Debug.WriteLine("User does not have permission to perform this action.");
                throw;
            }
            catch (SecurityException)
            {
                Debug.WriteLine("User does not have permission to perform this action.");
                throw;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("SetValue: {0}", ex.Message);
                Debug.WriteLine("Stacktrace: {0}", ex.StackTrace);
                throw;
            }
        }

        /// <summary>
        /// Gets the valuekind from the specified value.
        /// </summary>
        /// <param name="hive">The registry hive to access.</param>
        /// <param name="subKey">The path to the subkey.</param>
        /// <param name="valueName">The name of the value.</param>
        /// <returns></returns>
        public RegistryValueKind GetValueKind(RegistryHive hive, string subKey, string valueName)
        {
            if (string.IsNullOrEmpty(subKey)) throw new ArgumentNullException("subKey");
            if (string.IsNullOrEmpty(valueName)) throw new ArgumentNullException("valueName");

            var valueKind = RegistryValueKind.Unknown;

            try
            {
                using (var baseKey = RegistryKey.OpenBaseKey(hive, this.currentRegistryView))
                {
                    using (var key = baseKey.OpenSubKey(subKey))
                    {
                        if (key != null)
                        {
                            valueKind = key.GetValueKind(valueName);
                        }
                    }
                }
            }
            catch (IOException)
            {
                Debug.WriteLine("Value does not exist.");
            }
            catch (UnauthorizedAccessException)
            {
                Debug.WriteLine("User does not have permission to perform this action.");
            }
            catch (SecurityException)
            {
                Debug.WriteLine("User does not have permission to perform this action.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("GetValueKind: {0}", ex.Message);
                Debug.WriteLine("Stacktrace: {0}", ex.StackTrace);
            }

            return valueKind;
        }

        /// <summary>
        /// Gets the value names from the specified key.
        /// </summary>
        /// <param name="hive">The registry hive to access.</param>
        /// <param name="subKey">The path to the subkey.</param>
        /// <returns></returns>
        public List<string> GetValueNames(RegistryHive hive, string subKey)
        {
            if (string.IsNullOrEmpty(subKey)) throw new ArgumentNullException("subKey");

            var valueNames = new List<string>();

            try
            {
                using (var baseKey = RegistryKey.OpenBaseKey(hive, this.currentRegistryView))
                {
                    using (var key = baseKey.OpenSubKey(subKey))
                    {
                        if (key != null)
                        {
                            valueNames = key.GetValueNames().ToList();
                        }
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
                Debug.WriteLine("User does not have permission to perform this action.");
            }
            catch (SecurityException)
            {
                Debug.WriteLine("User does not have permission to perform this action.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("GetValueNames: {0}", ex.Message);
                Debug.WriteLine("Stacktrace: {0}", ex.StackTrace);
            }

            return valueNames;
        }

        /// <summary>
        /// Gets the current registry view. This is for x64 compatibility.
        /// </summary>
        /// <returns>RegistryView based on OS architecture.</returns>
        private static RegistryView GetCurrentRegistryView()
        {
            return ((Environment.Is64BitOperatingSystem) ? RegistryView.Registry64 : RegistryView.Default);
        }
    }
}
