using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using urlShortAPI.Models;

namespace urlShortAPI.Repository
{
    public class ShortenerRepo
    {
        public string Token { get; set; }
        public UrlShort urlShort;

        private ShortenerRepo GenerateToken()
        {
            string urlsafe = string.Empty;
            Enumerable.Range(48, 75)
              .Where(i => i < 58 || i > 64 && i < 91 || i > 96)
              .OrderBy(o => new Random().Next())
              .ToList()
              .ForEach(i => urlsafe += Convert.ToChar(i)); // Store each char into urlsafe
            Token = urlsafe.Substring(new Random().Next(0, urlsafe.Length), new Random().Next(2, 6));
            return this;
        }

        public ShortenerRepo(string url,LiteDatabase db)
        {
            var urls = db.GetCollection<UrlShort>();
            //Validate and generate new
            while (urls.Exists(u => u.Token == GenerateToken().Token)) ;

            urlShort = new UrlShort()
            {
                Token = Token,
                URL = url,
                //Prepare a open link
                ShortenedURL = "http://"+ HttpContext.Current.Request.Url.Authority+"/oo?url=" + Token
            };
            // Save the model to  the DB
            urls.Insert(urlShort);
        }


    }
}