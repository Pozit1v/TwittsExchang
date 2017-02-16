using Hammock.Authentication.OAuth;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetSharp;
using System.Runtime.Serialization;
using Hammock.Serialization;

namespace TwittsExchange
{
    
    class Program
    {
        private static string consumerKey = "UK49L81OxOuLTTVFOqchYMgvy";
        private static string consumerSecret = "sO2xnvWNmU3RhD6UAOKPHqSoGMx8aIYfWZ0ZWl9Mo4XkBRohdS";
        private static string accessToken = "832143668987437056-W1RNUvzUQ5BoZRTCPlU6MNgM9PDokyR";
        private static string accessSecret = "vLsgrNmZMEMrXXo1bgaTkR1hRe3yJ0MNRdqIc17tc6Gqg";

        private static TwitterService service = new TwitterService(consumerKey, consumerSecret, accessToken, accessSecret);


        static void Main(string[] args)
        {
            TweetDbEntities db = new TweetDbEntities();

            TwitterUser twitterUser = service.GetUserProfile(new GetUserProfileOptions());
            Console.WriteLine(twitterUser.ScreenName);

            Console.Write("@username: ");
            string username = Console.ReadLine();

            var options = new ListTweetsOnUserTimelineOptions { ScreenName = username, Count = 200};
            foreach (var tweet in service.ListTweetsOnUserTimeline(options))
            {
                Tweet tweets = new Tweet();
                tweets.Id = Guid.NewGuid();
            
                var date = tweet.CreatedDate;
                tweets.CreateDate = date;

                var author = tweet.Author.ScreenName.ToString();
                tweets.Author = author;

                var text = tweet.Text.ToString();
                tweets.Text = text;

                var media = tweet.Entities.Media.ToString();
                tweets.Media = media;

                db.Tweets.Add(tweets);
                db.SaveChanges();

                //var json = JObject.Parse(raw).SelectToken("user");
                //var j = json.SelectToken("profile_background_image_url").ToString();

                Console.WriteLine(tweet.Text);
                Console.WriteLine("Добавлена запись");
            }
                
            Console.ReadLine();
        }
  
        //Первая авторизация приложения
           
        //public static void Authorize()
        //{
        //    //TwitterService service = new TwitterService(consumerKey, consumerSecret);
        //    //OAuthRequestToken requestToken = service.GetRequestToken();
        //    //Console.WriteLine(requestToken.ToString());
        //    //Uri uri = service.GetAuthorizationUri(requestToken);
        //    //Process.Start(uri.ToString());
     
        //    //string verifier = Console.ReadLine();

        //    //OAuthAccessToken accessToken = service.GetAccessToken(requestToken, verifier);
        //    //service.AuthenticateWith(accessToken.Token, accessToken.TokenSecret);
        
        //    //Console.WriteLine(twitterUser.ToString());
        //    //Console.ReadLine();
        
        //}


    }

    
}
