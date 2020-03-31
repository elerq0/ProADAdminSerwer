using System;
using System.Web.Mvc;

namespace ProfessoftWeb.Controllers
{
    
    public class UsersController : Controller
    {
        [HttpGet]
        public string List()
        {
            return Cache.users.GetUsersList();
        }

        [HttpGet]
        public uint Version()
        {
            return Cache.users.version;
        }

        [HttpPost]
        public ActionResult Refresh()
        {
            Boolean result = Cache.users.Refresh();

            if (result)
                return new HttpStatusCodeResult(200);
            else
                return new HttpStatusCodeResult(422);
        }

        [HttpPost]
        public ActionResult Unlock()
        {
            string username = Request.Form.Keys[0];
            Boolean result = Cache.users.UnlockAccount(username);
            
            if(result)
                return new HttpStatusCodeResult(200);
            else
                return new HttpStatusCodeResult(422);
        }

        [HttpPost]
        public ActionResult ResetPassword()
        {
            string username = Request.Form.Keys[0];
            string password = Request.Form.Keys[1];
            Boolean result = Cache.users.ResetPassword(username, password);

            if (result)
                return new HttpStatusCodeResult(200);
            else
                return new HttpStatusCodeResult(422);
        }

        [HttpPost]
        public ActionResult ResetSession()
        {
            string username = Request.Form.Keys[0];
            Boolean result = Cache.users.ResetSession(username);

            if (result)
                return new HttpStatusCodeResult(200);
            else
                return new HttpStatusCodeResult(422);
        }

        [HttpGet]
        public string GetStatus(string username)
        {
            //string username = Request.Form.Keys[0];
            return Cache.users.GetStatus(username);
        }
    }
}