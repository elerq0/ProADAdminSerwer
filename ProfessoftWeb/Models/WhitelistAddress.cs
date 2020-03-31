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
            type = "Address";
            string[] args = new string[] { };
            if (Extensions.debug)
                collection = Extensions.RunPowerShellScript(Extensions.listWhitelistMockPath, args);
            else
                collection = Extensions.RunPowerShellScript(Extensions.listWhitelistAddressPath, args);
        }

        public Boolean Add(string address)
        {
            Extensions.logFile.Write("Whitelist - Add " + type);
            Extensions.logFile.Write("            Address = [" + address + "]");
            try
            {
                string[] args = new string[] { address };
                Extensions.RunPowerShellScript(Extensions.addWhitelistAddressPath, args);
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

        public Boolean Remove(string address)
        {
            Extensions.logFile.Write("Whitelist - Remove " + type);
            Extensions.logFile.Write("            Address = [" + address + "]");
            try
            {
                string[] args = new string[] { address };
                Extensions.RunPowerShellScript(Extensions.removeWhitelistAddressPath, args);
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