using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using urlShortAPI.Models;

namespace urlShortAPI.Controllers
{
    public class ooController : Controller
    {
        //LiteDB stored in App_Data
        LiteDatabase DB = new LiteDatabase(System.Web.HttpContext.Current.Server.MapPath("~/App_Data/UrlData.db"));
        // GET: oo
        [Route("oo")]
        public ActionResult Index(string url)
        {
            var urlData = DB.GetCollection<UrlShort>();
            var data = urlData.FindOne(u => u.Token == url);
            //If url found then redirect else redirect to home page
            if (data != null)
                return Redirect(data.URL);
            else
                return Redirect("Index.html");
        }
    }
}