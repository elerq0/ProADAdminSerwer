using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProfessoftWeb.Models
{
    public class WhitelistAddress : Whitelist
    {
        public override void BuildCollection()
        {
            string[] args = new string[] { };
            if (Extensions.debug)
                collection = Extensions.RunPowerShellScript(Extensions.listWhitelistMockPath, args);
            else
                collection = Extensions.RunPowerShellScript(Extensions.listWhitelistAddressPath, args);
        }

        public Boolean Add(string address)
        {
            try
            {
                string[] args = new string[] { address };
                Extensions.RunPowerShellScript(Extensions.addWhitelistAddressPath, args);
                Refresh();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Boolean Remove(string address)
        {
            try
            {
                string[] args = new string[] { address };
                Extensions.RunPowerShellScript(Extensions.removeWhitelistAddressPath, args);
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