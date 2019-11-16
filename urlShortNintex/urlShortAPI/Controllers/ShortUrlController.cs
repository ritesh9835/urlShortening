using System;
using System.Web.Http;
using urlShortAPI.Models;
using urlShortAPI.Repository;
using LiteDB;
using System.Web;
using System.IO;

namespace urlShortAPI.Controllers
{

    [RoutePrefix("api/ShortUrl")]
    public class ShortUrlController : ApiController
    {
        //LiteDB stored in App_Data
        //Use complete path during unit test
        LiteDatabase DB = new LiteDatabase(HttpContext.Current.Server.MapPath("~/App_Data/UrlData.db"));

        //LiteDatabase DB = new LiteDatabase(@"E:\Solutions\Asp.Net\urlShortner\urlshortening\urlShortNintex\urlShortAPI\App_Data\UrlData.db");

        [ActionName("SaveUrl")]
        [HttpPost]
        [Route("SaveUrl")]
        public string SaveUrl(string urlParm)
        {
            var urlData = DB.GetCollection <UrlShort>();
            try
            {
                // check if the URL already exists within our database
                if (urlData.Exists(u => u.URL == urlParm))
                    return urlData.FindOne(u=>u.URL== urlParm).ShortenedURL;
                
                // Shorten the URL and return url
                ShortenerRepo shortURL = new ShortenerRepo(urlParm, DB);
                return shortURL.urlShort.ShortenedURL;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
            
        }

        protected override void Dispose(bool disposing)
        {
            DB.Dispose();
            base.Dispose(disposing);
        }

    }
}
