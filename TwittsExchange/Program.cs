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
using TwittsExchange.Data;
using System.Timers;

namespace TwittsExchange
{
    
    class Program
    {
        private const string consumerKey = "UK49L81OxOuLTTVFOqchYMgvy";
        private const string consumerSecret = "sO2xnvWNmU3RhD6UAOKPHqSoGMx8aIYfWZ0ZWl9Mo4XkBRohdS";
        private const string accessToken = "832143668987437056-W1RNUvzUQ5BoZRTCPlU6MNgM9PDokyR";
        private const string accessSecret = "vLsgrNmZMEMrXXo1bgaTkR1hRe3yJ0MNRdqIc17tc6Gqg";

        private static TwitterService service = new TwitterService(consumerKey, consumerSecret, accessToken, accessSecret);

        private static Timer CheckTimer;

        private static long LastTweetId;
        private static string Username;

        static void Main(string[] args)
        {
            Console.Write("@username: ");
            Username = Console.ReadLine();

            CheckTimer = new Timer();
            CheckTimer.Interval = 30000;

            CheckTimer.Elapsed += OnTimedEvent;

            CheckTimer.Enabled = true;

            TwitterUser twitterUser = service.GetUserProfile(new GetUserProfileOptions());
            Console.WriteLine(twitterUser.ScreenName);
            
            //CheckTweet(username);
            
                
            Console.ReadLine();
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            Console.WriteLine("Checking");
            CheckTweet(Username);
        }

        private static void CheckTweet(string username)
        {
            var options = new ListTweetsOnUserTimelineOptions { ScreenName = username, Count = 1 };

            foreach (TwitterStatus tweet in service.ListTweetsOnUserTimeline(options))
            {
                if(tweet.Id != LastTweetId)
                {
                    Tweet tweets = new Tweet()
                    {
                        Id = Guid.NewGuid(),
                        Author = tweet.Author.ScreenName.ToString(),
                        Text = tweet.Text,
                        CreateDate = tweet.CreatedDate,
                        Media = tweet.Entities.Media.ToString()
                    };

                    if (tweets.Media == "System.Collections.Generic.List`1[TweetSharp.TwitterMedia]")
                    {
                        TwitterStatus result = service.SendTweet(new SendTweetOptions
                        {
                            Status = tweet.Text
                        });
                    }
                    LastTweetId = tweet.Id;
                    Console.WriteLine(tweet.Id);
                    Console.WriteLine("Tweet opublikovan");
                } else
                {
                    Console.WriteLine("Takoi tweet uge est");
                }
             }
            
              
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
