﻿using System.Management.Automation;
using BluebirdPS.Models;

namespace BluebirdPS.Cmdlets
{
    public abstract class TweetCmdlet : ClientCmdlet
    {
        [Parameter()]
        public SwitchParameter NonPublicMetrics { get; set; }
        [Parameter()]
        public SwitchParameter PromotedMetrics { get; set; }
        [Parameter()]
        public SwitchParameter OrganicMetrics { get; set; }
        [Parameter()]
        public SwitchParameter IncludeExpansions { get; set; }

        internal readonly ExpansionTypes ExpansionType = ExpansionTypes.Tweet;
    }
}
