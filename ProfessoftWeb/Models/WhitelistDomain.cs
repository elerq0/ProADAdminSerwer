using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Management.Automation;
using Newtonsoft.Json;

namespace ProfessoftWeb.Models
{
    public class WhitelistDomain: Whitelist
    {
        public override void BuildCollection()
        {
            string[] args = new string[] { };
            if (Extensions.debug)
                collection = Extensions.RunPowerShellScript(Extensions.listWhitelistMockPath, args);
            else
                collection = Extensions.RunPowerShellScript(Extensions.listWhitelistDomainPath, args);
        }

        public Boolean Add(string domain)
        {
            try
            {
                string[] args = new string[] { domain };
                Extensions.RunPowerShellScript(Extensions.addWhitelistDomainPath, args);
                Refresh();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Boolean Remove(string domain)
        {
            try
            {
                string[] args = new string[] { domain };
                Extensions.RunPowerShellScript(Extensions.removeWhitelistDomainPath, args);
                Refresh();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}