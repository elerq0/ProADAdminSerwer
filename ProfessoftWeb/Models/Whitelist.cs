using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Management.Automation;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


namespace ProfessoftWeb.Models
{
    public class Whitelist
    {
        public UInt32 version = 0;
        protected string type = "";
        protected Collection<PSObject> collection;

        public Whitelist()
        {
            BuildCollection();
            version = 1;
        }

        public Boolean Refresh()
        {
            Extensions.logFile.Write("Whitelist - Refresh " + type + " List");
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


        public virtual void BuildCollection() { }

        public string GetPathsList()
        {
            Extensions.logFile.Write("Whitelist - Get " + type + " List");
            try
            {
                List<Objects.Address> list = new List<Objects.Address>();
                foreach (PSObject psobject in collection)
                {
                    Objects.Address address;
                    if (Extensions.debug)
                        address = new Objects.Address
                        {
                            Path = psobject.Properties["ProcessName"].Value.ToString()
                        };
                    else
                    {
                        string path = GetPathRegex(psobject);
                        if (path == string.Empty)
                            continue;

                        address = new Objects.Address
                        {
                            Path = path
                        };
                    }
                    list.Add(address);
                }

                Extensions.logFile.Write("            Completed Successfully");
                if (list.Count != 0)
                    return JsonConvert.SerializeObject(list);
                else
                    return String.Empty;
            }
            catch (Exception e)
            {
                Extensions.logFile.Write("            Failed");
                Extensions.logFile.Write(e.Message);
                throw e;
            }
        }

        private string GetPathRegex(PSObject psobject)
        {
            try
            {
                string text = JsonConvert.SerializeObject(psobject, Formatting.Indented, new JsonConverter[] { new StringEnumConverter() });

                Regex rx = new Regex(@"<S>([\w\.@\-_*]*)<\/S>");
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