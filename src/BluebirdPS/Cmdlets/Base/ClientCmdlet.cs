﻿using AutoMapper;
using BluebirdPS.Core;
using BluebirdPS.Models;
using BluebirdPS.Models.Exceptions;
using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Text;
using Tweetinvi;
using Tweetinvi.Core.Exceptions;
using Tweetinvi.Events;
using Tweetinvi.Models;
using Mapper = BluebirdPS.Core.Mapper;

namespace BluebirdPS.Cmdlets.Base
{
    public abstract class ClientCmdlet : AuthCmdlet
    {
        [Parameter()]
        public SwitchParameter NoPagination { get; set; }

        internal static IMapper mapper = Mapper.GetOrCreateInstance();

        internal static TwitterClient client = GetOrCreateInstance();

        private static TwitterClient GetOrCreateInstance() => client ??= Create();

        private static TwitterClient Create()
        {
            if (Credentials.HasCredentialsInEnvVars())
            {
                oauth = Credentials.ReadCredentialsFromEnvVars();
            }
            else
            {
                oauth = Credentials.ReadCredentialsFromFile();
            }

            if (oauth.IsNull())
            {
                StringBuilder message = new StringBuilder();
                message.AppendLine($"Credentials were not found in environment variables(BLUEBIRDPS_ *) or in { Config.credentialsPath}");
                message.AppendLine("Please use the Set-TwitterAuthentication command to update the required API keys and secrets.");
                message.AppendLine("For more information, see conceptual help topic about_BluebirdPS_Credentials");

                throw new BluebirdPSNullCredentialsException(message.ToString());
            }

            client = new TwitterClient(new TwitterCredentials(
                    oauth.ApiKey,
                    oauth.ApiSecret,
                    oauth.AccessToken,
                    oauth.AccessTokenSecret
                    ));

            // add any Configuration values here
            client.Config.RateLimitTrackerMode = RateLimitTrackerMode.TrackOnly;

            TweetinviEvents.AfterExecutingRequest += AfterExecutingRequest;
            TweetinviEvents.OnTwitterException += OnTwitterException;
            TweetinviEvents.SubscribeToClientEvents(client);

            return client;
        }

        private static void AfterExecutingRequest(object sender, AfterExecutingQueryEventArgs args)
        {

            if (args.Exception != null)
            {
                throw new Exception(args.Exception.Message);
            }

            IMapper mapper = Mapper.GetOrCreateInstance();
            List<ResponseData> history = Core.History.GetOrCreateInstance();

            ResponseData historyRecord = mapper.Map<ResponseData>(args);
            history.Add(historyRecord);
        }

        private static void OnTwitterException(object sender, ITwitterException e)
        {
            System.Console.WriteLine(e.Content);
        }

    }
}
