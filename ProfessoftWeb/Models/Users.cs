using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Internal;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ProfessoftWeb.Models
{
    public class Users
    {
        public UInt32 version = 0;
        private Collection<PSObject> collection;

        public Users()
        {
            BuildCollection();
            version = 1;
        }

        public Boolean Refresh()
        {
            Extensions.logFile.Write("User - Refresh");
            try
            {
                BuildCollection();
                version += 1;
                Extensions.logFile.Write("       Completed Successfully");
                return true;
            }
            catch (Exception)
            {
                Extensions.logFile.Write("       Failed");
                return false;
            }
        }

        private void BuildCollection()
        {
            string[] args = new string[] { };
            if(Extensions.debug)
                collection = Extensions.RunPowerShellScript(Extensions.listUserMockPath, args);
            else
                collection = Extensions.RunPowerShellScript(Extensions.listUserPath, args);

        }

        public string GetUsersList()
        {
            Extensions.logFile.Write("User - Get List");
            try
            {
                List<Objects.User> list = new List<Objects.User>();
                foreach (PSObject psobject in collection)
                {
                    Objects.User user;
                    if (Extensions.debug)
                        user = new Objects.User
                        {
                            Name = psobject.Properties["ProcessName"].Value.ToString()
                        };
                    else
                    {
                        string name = GetUserNameRegex(psobject);
                        if (name == string.Empty)
                            continue;
                        user = new Objects.User
                        {
                            Name = name
                        };
                    }
                    list.Add(user);
                }

                Extensions.logFile.Write("       Completed Successfully");
                if (list.Count != 0)
                    return JsonConvert.SerializeObject(list);
                else
                    return String.Empty;
            }
            catch(Exception e)
            {
                Extensions.logFile.Write("       Failed");
                Extensions.logFile.Write(e.Message);
                throw e;
            }
        }

        public Boolean UnlockAccount(string username)
        {
            Extensions.logFile.Write("User - Unlock Account");
            Extensions.logFile.Write("       UserName = [" + username + "]");
            try
            {
                string[] args = new string[] { username };
                Extensions.RunPowerShellScript(Extensions.unlockUserPath, args);
                Extensions.logFile.Write("       Completed Successfully");
                return true;
            }
            catch(Exception)
            {
                Extensions.logFile.Write("       Failed");
                return false;
            }
        }

        public Boolean ResetPassword(string username, string password)
        {
            Extensions.logFile.Write("User - Reset Password");
            Extensions.logFile.Write("       UserName = [" + username + "]");
            Extensions.logFile.Write("       Password = [" + password + "]");
            try
            {
                string[] args = new string[] { username, password };
                Extensions.RunPowerShellScript(Extensions.resetUserPasswordPath, args);
                Extensions.logFile.Write("       Completed Successfully");
                return true;
            }
            catch (Exception)
            {
                Extensions.logFile.Write("       Failed");
                return false;
            }
        }

        public Boolean ResetSession(string username)
        {
            Extensions.logFile.Write("User - Reset Session");
            Extensions.logFile.Write("       UserName = [" + username + "]");
            try
            {
                string[] args = new string[] { username };
                Extensions.RunPowerShellScript(Extensions.resetUserSessionPath, args);
                Extensions.logFile.Write("       Completed Successfully");
                return true;
            }
            catch (Exception)
            {
                Extensions.logFile.Write("       Failed");
                return false;
            }
        }

        public string GetStatus(string username)
        {
            Extensions.logFile.Write("User - Get Status");
            Extensions.logFile.Write("       UserName = [" + username + "]");

            try
            {
                Collection<PSObject> history;
                string[] args = new string[] { username };
                history = Extensions.RunPowerShellScript(Extensions.logonHistoryUserPath, args);
                List<string> hist = new List<string>();
                foreach (PSObject psobject in history)
                {
                    hist.Add(GetLogonHistoryRegex(psobject));
                }

                Extensions.logFile.Write("       Completed Successfully");

                if (hist.Count != 0)
                    return JsonConvert.SerializeObject(hist);
                else
                    return string.Empty;
            }
            catch(Exception e)
            {
                Extensions.logFile.Write("       Failed");
                Extensions.logFile.Write(e.Message);
                throw e;
            }
        }

        private string GetUserNameRegex(PSObject psobject)
        {
            try
            {
                string text = JsonConvert.SerializeObject(psobject, Formatting.Indented, new JsonConverter[] { new StringEnumConverter() });

                Regex rx = new Regex(@"<S N=\\""propertyValue\\"">([\w\.]*)<\/S>");
                MatchCollection matches = rx.Matches(text);
                if (matches.Count == 0)
                    return String.Empty;
                else
                    return matches[0].Groups[1].Value;
            }
            catch (Exception e)
            {
                throw new Exception("Regex exception: " + e.Message);
            }
        }

        private string GetLogonHistoryRegex(PSObject psobject)
        {
            try
            {
                string text = JsonConvert.SerializeObject(psobject, Formatting.Indented, new JsonConverter[] { new StringEnumConverter() });

                Regex rx = new Regex(@"Serwer=([\w\d]+); Akcja=([\w\d]+); Data=([\w\d\/ :]+);");
                MatchCollection matches = rx.Matches(text);
                if (matches.Count == 0)
                    return String.Empty;
                else
                    return matches[0].Groups[1].Value + " : " + matches[0].Groups[2].Value + " : " + matches[0].Groups[3].Value;
            }
            catch (Exception e)
            {
                throw new Exception("Regex exception: " + e.Message);
            }
        }
    }
}