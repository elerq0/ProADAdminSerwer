using System;
using System.Web.Mvc;

namespace ProfessoftWeb.Controllers
{
   
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

            if (result)
                return new HttpStatusCodeResult(200);
            else
                return new HttpStatusCodeResult(422);
        }

        [HttpPost]
        public ActionResult DomainAdd()
        {
            string path = Request.Form.Keys[0];
            Boolean result = Cache.whitelistDomain.Add(path);

            if (result)
                return new HttpStatusCodeResult(200);
            else
                return new HttpStatusCodeResult(422);
        }

        [HttpPost]
        public ActionResult DomainRemove()
        {
            string path = Request.Form.Keys[0];
            Boolean result = Cache.whitelistDomain.Remove(path);

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

            if (result)
                return new HttpStatusCodeResult(200);
            else
                return new HttpStatusCodeResult(422);
        }

        [HttpPost]
        public ActionResult AddressAdd()
        {
            string path = Request.Form.Keys[0];
            Boolean result = Cache.whitelistAddress.Add(path);

            if (result)
                return new HttpStatusCodeResult(200);
            else
                return new HttpStatusCodeResult(422);
        }

        [HttpPost]
        public ActionResult AddressRemove()
        {
            string path = Request.Form.Keys[0];
            Boolean result = Cache.whitelistAddress.Remove(path);

            if (result)
                return new HttpStatusCodeResult(200);
            else
                return new HttpStatusCodeResult(422);
        }


    }
}