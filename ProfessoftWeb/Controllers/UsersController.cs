using System;
using System.Web.Mvc;

namespace ProfessoftWeb.Controllers
{
    [Authorize]
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
            Extensions.logFile.Write("Użytkownik [" + User.Identity.Name + "] odświeżył dane użytkowników z wynikiem = " + result);

            if (result)
                return new HttpStatusCodeResult(200);
            else
                return new HttpStatusCodeResult(422);
        }

        [HttpPost]
        public ActionResult Unlock(string username)
        {
            Boolean result = Cache.users.UnlockAccount(username);
            Extensions.logFile.Write("Użytkownik [" + User.Identity.Name + "] odblokował konto użytkownika [" + username + "] z wynikiem = " + result);
            
            if(result)
                return new HttpStatusCodeResult(200);
            else
                return new HttpStatusCodeResult(422);
        }

        [HttpPost]
        public ActionResult ResetPassword(string username, string password)
        {
            Boolean result = Cache.users.ResetPassword(username, password);
            Extensions.logFile.Write("Użytkownik [" + User.Identity.Name + "] zresetował hasło użytkownika [" + username + "] z wynikiem = " + result);

            if (result)
                return new HttpStatusCodeResult(200);
            else
                return new HttpStatusCodeResult(422);
        }

        [HttpPost]
        public ActionResult ResetSession(string username)
        {
            Boolean result = Cache.users.ResetSession(username);
            Extensions.logFile.Write("Użytkownik [" + User.Identity.Name + "] zresetował sesję użytkownika [" + username + "] z wynikiem = " + result);

            if (result)
                return new HttpStatusCodeResult(200);
            else
                return new HttpStatusCodeResult(422);
        }

        
    }
}