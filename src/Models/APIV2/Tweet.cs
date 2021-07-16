﻿using System;
using System.Collections.Generic;
using System.Management.Automation;
using BluebirdPS.Models.APIV2.Metrics.Tweet;

namespace BluebirdPS.Models.APIV2
{
    public class Tweet : TwitterObject
    {
        public string Id { get; set; }
        public string Text { get; set; }

        public Attachments Attachments { get; set; }
        public string AuthorId { get; set; }
        public List<ContextAnnotation> ContextAnnotations { get; set; }
        public string ConversationId { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<object> Entities { get; set; }
        public PSObject Geo { get; set; }
        public string InReplyToUserId { get; set; }
        public string Language { get; set; }
        public NonPublic NonPublicMetrics { get; set; }
        public Organic OrganicMetrics { get; set; }
        public bool PossiblySensitive { get; set; }
        public Promoted PromotedMetrics { get; set; }
        public Public PublicMetrics { get; set; }
        public List<ReferencedTweet> ReferencedTweets { get; set; }
        public string ReplySettings { get; set; }
        public string Source { get; set; }
        public WithheldContent Withheld { get; set; }

        public Tweet() { }
        public Tweet(dynamic input)
        {
            try
            {
                OriginalObject = input;

                if (Core.Helpers.HasProperty(input, "id_str"))
                {
                    // API v1.1 return
                    Id = input.id_str;
                    AuthorId = input.user.id_str;
                    CreatedAt = Core.Helpers.ConvertFromV1Date(input.created_at);
                    InReplyToUserId = input.in_reply_to_user_id_str;

                }
                else
                {
                    Id = input.id;
                    AuthorId = input.author_id;
                    CreatedAt = input.created_at;
                    ConversationId = input.conversation_id;
                    PublicMetrics = new Public(input.public_metrics);
                    InReplyToUserId = input.in_reply_to_user_id;
                }

                Text = input.text;
                Entities = BaseEntity.GetEntities(input.entities);
                Language = input.lang;
                PossiblySensitive = input.possibly_sensitive;
                Source = input.source;

                if (Core.Helpers.HasProperty(input, "attachments"))
                {
                    Attachments = new Attachments(input.attachments);
                }

                if (Core.Helpers.HasProperty(input, "geo"))
                {
                    Geo = input.geo;
                }

                if (Core.Helpers.HasProperty(input, "reply_settings"))
                {
                    ReplySettings = Core.Helpers.ToTitleCase(input.reply_settings);
                }

                if (Core.Helpers.HasProperty(input, "withheld"))
                {
                    Withheld = new WithheldContent(input.withheld);
                }

                if (Core.Helpers.HasProperty(input, "referenced_tweets"))
                {
                    List<ReferencedTweet> referencedTweets = new();
                    foreach (dynamic refTweet in input.referenced_tweets)
                    {
                        referencedTweets.Add(new ReferencedTweet(refTweet));
                    }
                    ReferencedTweets = referencedTweets;
                }

                if (Core.Helpers.HasProperty(input, "context_annotations"))
                {
                    List<ContextAnnotation> contextAnnotations = new();

                }

                if (Core.Helpers.HasProperty(input, "non_public_metrics"))
                {
                    NonPublicMetrics = new NonPublic(input.non_public_metrics);
                }
                if (Core.Helpers.HasProperty(input, "organic_metrics"))
                {
                    OrganicMetrics = new Organic(input.organic_metrics);
                }
                if (Core.Helpers.HasProperty(input, "promoted_metrics"))
                {
                    PromotedMetrics = new Promoted(input.promoted_metrics);
                }
            }
            catch
            {
                // any missing properties that are not on the input object
                // and not caught with Helpers.HasProperty if statement,
                // do not fail (for now)
            }
        }

        public override string ToString()
        {
            return $"{Id}:{Text}";
        }
    }

    public class ReferencedTweet : TwitterObject
    {
        public string Type;
        public string Id;

        public ReferencedTweet() { }
        public ReferencedTweet(dynamic input)
        {
            Type = Core.Helpers.ToTitleCase(input.type);
            Id = input.id;
            OriginalObject = input;
        }
    }
}
