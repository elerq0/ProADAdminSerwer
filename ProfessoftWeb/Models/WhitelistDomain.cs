﻿using System;
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
            type = "Domain";
            string[] args = new string[] { };
            if (Extensions.debug)
                collection = Extensions.RunPowerShellScript(Extensions.listWhitelistMockPath, args);
            else
                collection = Extensions.RunPowerShellScript(Extensions.listWhitelistDomainPath, args);
        }

        public Boolean Add(string domain)
        {
            Extensions.logFile.Write("Whitelist - Add " + type);
            Extensions.logFile.Write("            Domain = [" + domain + "]");
            try
            {
                string[] args = new string[] { domain };
                Extensions.RunPowerShellScript(Extensions.addWhitelistDomainPath, args);
                Refresh();
                Extensions.logFile.Write("            Completed Successfully");
                return true;
            }
            catch (Exception)
            {
                Extensions.logFile.Write("            Failed");
                return false;
            }
        }

        public Boolean Remove(string domain)
        {
            Extensions.logFile.Write("Whitelist - Remove " + type);
            Extensions.logFile.Write("            Domain = [" + domain + "]");
            try
            {
                string[] args = new string[] { domain };
                Extensions.RunPowerShellScript(Extensions.removeWhitelistDomainPath, args);
                Refresh();
                Extensions.logFile.Write("            Completed Successfully");
                return true;
            }
            catch (Exception)
            {
                Extensions.logFile.Write("            Failed");
                return false;
            }
        }
    }
}