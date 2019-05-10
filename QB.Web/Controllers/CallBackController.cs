using Intuit.Ipp.OAuth2PlatformClient;
using QB.BAL.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Net.Http;

namespace WebApplication3.Controllers
{
    public class CallBackController : Controller
    {
        public static OAuth2Client oauthClient = new OAuth2Client("Q0TMGv2KUfZT5vfgcg0kKBiU1hDvzB3Gv6Ir4vNdDRSVru6O26", "MCUSCRZF8q78uDethITkr875cGEqu8on7kUb33Qa", "http://localhost:60833/CallBack", "sandbox"); // environment is “sandbox” or “production”

        // GET: CallBack
        public async Task<ActionResult> Index()
        {
            ViewBag.Code = Request.QueryString["code"] ?? "none";
            ViewBag.RealmId = Request.QueryString["realmId"] ?? "none";
            ViewBag.State = Request.QueryString["state"];

            var code = Request.QueryString["code"];
            var realmId = Request.QueryString["realmId"];
            // Get OAuth2 Bearer token
            var tokenResponse = await oauthClient.GetBearerTokenAsync(code);
            //retrieve access_token and refresh_token
            ViewBag.accessToken = tokenResponse.AccessToken;
            Session["refreshToken"] = tokenResponse.RefreshToken;
            ViewBag.refreshToken = tokenResponse.RefreshToken;

            AccountService service = new AccountService();
            var res =  service.PostAccount("shukla gaurav" , "Accounts Payable", tokenResponse.AccessToken);
            return View();
        }
        [HttpPost]
        [ActionName("Index")]
        public async Task<ActionResult> Token()
        {
            var code = Request.QueryString["code"];
            var realmId = Request.QueryString["realmId"];
            ViewBag.Code = Request.QueryString["code"] ?? "none";
            ViewBag.RealmId = Request.QueryString["realmId"] ?? "none";
            ViewBag.State = Request.QueryString["state"];
            // Get OAuth2 Bearer token
            var tokenResponse = await oauthClient.RefreshTokenAsync(Convert.ToString(Session["refreshToken"]));
            ViewBag.accessToken = tokenResponse.AccessToken;
            Session["refreshToken"] = tokenResponse.RefreshToken;
            ViewBag.refreshToken = tokenResponse.RefreshToken;
            return View();
        }

        public ActionResult Test()
        {
            //Prepare scopes
            List<OidcScopes> scopes = new List<OidcScopes>();
            scopes.Add(OidcScopes.Accounting);

            //Get the authorization URL
            string authorizeUrl = oauthClient.GetAuthorizationURL(scopes);

            return Redirect(authorizeUrl);
        }

        public ActionResult SaveAccount(string a, string b)
        {
            return View("Index");
        }

    }
}