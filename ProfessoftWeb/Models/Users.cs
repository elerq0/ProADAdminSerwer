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
            try
            {
                BuildCollection();
                version += 1;
                return true;
            }
            catch (Exception)
            {
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

                if (list.Count != 0)
                    return JsonConvert.SerializeObject(list);
                else
                    return String.Empty;
            }
            catch(Exception e)
            {
                Extensions.logFile.Write(e.Message);
                throw e;
            }
        }

        public Boolean UnlockAccount(string username)
        {
            try
            {
                string[] args = new string[] { username };
                Extensions.RunPowerShellScript(Extensions.unlockUserPath, args);
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public Boolean ResetPassword(string username, string password)
        {
            try
            {
                string[] args = new string[] { username, password };
                Extensions.RunPowerShellScript(Extensions.resetUserPasswordPath, args);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Boolean ResetSession(string username)
        {
            try
            {
                string[] args = new string[] { username };
                Extensions.RunPowerShellScript(Extensions.resetUserSessionPath, args);
                return true;
            }
            catch (Exception)
            {
                return false;
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
    }
}