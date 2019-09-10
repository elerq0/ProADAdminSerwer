using System;
using System.Web.Mvc;

namespace ProfessoftWeb.Controllers
{
    [Authorize]
    public class WhitelistController : Controller
    {
        [HttpGet]
        public string DomainList()
        {
            return Cache.whitelistDomain.GetPathsList();
        }

        [HttpGet]
        public uint DomainVersion()
        {
            return Cache.whitelistDomain.version;
        }

        [HttpPost]
        public ActionResult DomainRefresh()
        {
            Boolean result = Cache.whitelistDomain.Refresh();
            Extensions.logFile.Write("Użytkownik [" + User.Identity.Name + "] odświeżył domeny whitelisty z wynikiem = " + result);

            if (result)
                return new HttpStatusCodeResult(200);
            else
                return new HttpStatusCodeResult(422);
        }

        [HttpPost]
        public ActionResult DomainAdd(string path)
        {
            Boolean result = Cache.whitelistDomain.Add(path);
            Extensions.logFile.Write("Użytkownik [" + User.Identity.Name + "] dodał domenę [" + path + "] do whitelisty z wynikiem = " + result);

            if (result)
                return new HttpStatusCodeResult(200);
            else
                return new HttpStatusCodeResult(422);
        }

        [HttpPost]
        public ActionResult DomainRemove(string path)
        {
            Boolean result = Cache.whitelistDomain.Remove(path);
            Extensions.logFile.Write("Użytkownik [" + User.Identity.Name + "] usunął domenę [" + path + "] z whitelisty z wynikiem = " + result);

            if (result)
                return new HttpStatusCodeResult(200);
            else
                return new HttpStatusCodeResult(422);
        }

        [HttpGet]
        public string AddressList()
        {
            return Cache.whitelistAddress.GetPathsList();
        }

        [HttpGet]
        public uint AddressVersion()
        {
            return Cache.whitelistAddress.version;
        }

        [HttpPost]
        public ActionResult AddressRefresh()
        {
            Boolean result = Cache.whitelistAddress.Refresh();
            Extensions.logFile.Write("Użytkownik [" + User.Identity.Name + "] odświeżył adresy whitelisty z wynikiem = " + result);

            if (result)
                return new HttpStatusCodeResult(200);
            else
                return new HttpStatusCodeResult(422);
        }

        [HttpPost]
        public ActionResult AddressAdd(string path)
        {
            Boolean result = Cache.whitelistAddress.Add(path);
            Extensions.logFile.Write("Użytkownik [" + User.Identity.Name + "] dodał adres [" + path + "] do whitelisty z wynikiem = " + result);

            if (result)
                return new HttpStatusCodeResult(200);
            else
                return new HttpStatusCodeResult(422);
        }

        [HttpPost]
        public ActionResult AddressRemove(string path)
        {
            Boolean result = Cache.whitelistAddress.Remove(path);
            Extensions.logFile.Write("Użytkownik [" + User.Identity.Name + "] usunął adres [" + path + "] z whitelisty z wynikiem = " + result);

            if (result)
                return new HttpStatusCodeResult(200);
            else
                return new HttpStatusCodeResult(422);
        }


    }
}