﻿using BluebirdPS.Models;
using System;
using System.IO;

namespace BluebirdPS.Core
{


    public class Configuration
    {

        public string AuthUserId { get; set; } = "292670084";
        public string AuthUserName { get; set; } = "thedavecarroll";
        public DateTime? AuthValidationDate { get; set; }
        public DateTime? AuthLastExportDate { get; set; }
        public RateLimitAction RateLimitAction { get; set; } = RateLimitAction.Warning;
        public int RateLimitThreshold { get; set; } = 5;
        public string ConfigurationPath { get; set; } = Path.Join(Core.Helpers.GetFileSavePath(), "Configuration.Json");
        public string CredentialsPath { get; set; } = Path.Join(Core.Helpers.GetFileSavePath(), "twittercred.sav");
        public OutputType OutputType { get; set; } = OutputType.JSON;
        public Configuration() { }

    }
}
