using System;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.IO;

namespace ProfessoftWeb
{
    public static class Extensions
    {
        public static Models.Objects.LogFile logFile = new Models.Objects.LogFile(Properties.Settings.Default.LogFilePath);
        public readonly static Boolean debug = Properties.Settings.Default.Debug;

        private readonly static string scriptsPath = Properties.Settings.Default.PsScriptsPath;

        public readonly static string listUserMockPath = scriptsPath + "list_user_mock.ps1";
        public readonly static string listWhitelistMockPath = scriptsPath + "list_whitelist_mock.ps1";

        public readonly static string listUserPath = scriptsPath + "list_user.ps1";
        public readonly static string resetUserPasswordPath = scriptsPath + "reset_user_password.ps1";
        public readonly static string resetUserSessionPath = scriptsPath + "reset_user_session.ps1";
        public readonly static string unlockUserPath = scriptsPath + "unlock_user.ps1";
        public readonly static string logonHistoryUserPath = scriptsPath + "user_logon_history.ps1";

        public readonly static string listWhitelistDomainPath = scriptsPath + "list_whitelist_domains.ps1";
        public readonly static string addWhitelistDomainPath = scriptsPath + "add_whitelist_domain.ps1";
        public readonly static string removeWhitelistDomainPath = scriptsPath + "remove_whitelist_domain.ps1";

        public readonly static string listWhitelistAddressPath = scriptsPath + "list_whitelist_address.ps1";
        public readonly static string addWhitelistAddressPath = scriptsPath + "add_whitelist_address.ps1";
        public readonly static string removeWhitelistAddressPath = scriptsPath + "remove_whitelist_address.ps1";


        public static Collection<PSObject> RunPowerShellScript(string path, string[] args)
        {
            Collection<PSObject> col;
            using (PowerShell ps = PowerShell.Create())
            {
                string cmd = GetCommand(path, args);
                Extensions.logFile.Write(cmd);
                ps.AddScript(cmd);
                try
                {
                    col = ps.Invoke();

                    if (ps.Streams.Error.Count > 0)
                        throw new Exception("PS Error");
                }
                catch(Exception e)
                {
                    logFile.Write(e.Message);
                    Collection<ErrorRecord> error = ps.Streams.Error.ReadAll();
                    foreach (ErrorRecord err in error)
                    {
                        logFile.Write(err.Exception.Message);
                    }
                    throw new Exception();
                }
            }
            return col;
        }

        private static string GetCommand(string path, string[] args)
        {
            string command = File.ReadAllText(path, System.Text.Encoding.UTF8);
            for (int i = 0; i < args.Length; i++)
            {
                command = command.Replace("$args[" + i + "]", "\"" + args[i] + "\"");
            }

            return command;
        }

    }
}