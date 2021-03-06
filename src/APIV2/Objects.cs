﻿using System;
using System.Collections.Generic;
using System.Management.Automation;

namespace BluebirdPS.APIV2.Objects
{
    public class BaseEntity
    {
        public long Start { get; set; }
        public long End { get; set; }

        public static List<object> GetEntities(dynamic input)
        {
            List<object> entities = new List<object>();

            try
            {
                if (Helpers.HasProperty(input, "annotations"))
                {
                    foreach (dynamic annotation in input.annotations)
                    {
                        entities.Add(new Annotation(annotation));
                    }
                }
                if (Helpers.HasProperty(input, "cashtags"))
                {
                    foreach (dynamic cashtag in input.cashtags)
                    {
                        entities.Add(new CashTag(cashtag));
                    }
                }
                if (Helpers.HasProperty(input, "hashtags"))
                {
                    foreach (dynamic hashtag in input.hashtags)
                    {
                        entities.Add(new HashTag(hashtag));
                    }
                }
                if (Helpers.HasProperty(input, "mentions"))
                {
                    foreach (dynamic mention in input.mentions)
                    {
                        entities.Add(new Mention(mention));
                    }
                }
                if (Helpers.HasProperty(input, "urls"))
                {
                    foreach (dynamic url in input.urls)
                    {
                        entities.Add(new UrlTag(url));
                    }
                }
                if (Helpers.HasProperty(input, "url"))
                {
                    if (Helpers.HasProperty(input.url, "urls"))
                    {
                        foreach (dynamic url in input.url.urls)
                        {
                            entities.Add(new UrlTag(url));
                        }
                    }
                }
                if (Helpers.HasProperty(input, "description"))
                {
                    if (Helpers.HasProperty(input.description, "urls"))
                    {
                        foreach (dynamic url in input.description.urls)
                        {
                            entities.Add(new UrlTag(url));
                        }
                    }
                }
            }
            catch
            {
                // any missing properties that are not on the input object
                // and not caught with Helpers.HasProperty if statement,
                // do not fail (for now)
            }
            return entities.Count > 0 ? entities : null;
        }
    }

    public class Annotation : BaseEntity
    {
        public double Probability { get; set; }
        public string Type { get; set; }
        public string NormalizedText { get; set; }

        public Annotation() { }
        public Annotation(dynamic input)
        {
            Start = input.start;
            End = input.end;
            Probability = input.probability;
            Type = input.type;
            NormalizedText = input.normalized_text;
        }
    }

    public class CashTag : BaseEntity
    {
        public string Tag { get; set; }

        public CashTag() { }
        public CashTag(dynamic input)
        {
            Start = input.start;
            End = input.end;
            Tag = input.tag;
        }
    }

    public class HashTag : BaseEntity
    {
        public string Tag { get; set; }

        public HashTag() { }
        public HashTag(dynamic input)
        {
            Start = input.start;
            End = input.end;
            Tag = input.tag;
        }
    }

    public class Mention : BaseEntity
    {
        public string Tag { get; set; }

        public Mention() { }
        public Mention(dynamic input)
        {
            Start = input.start;
            End = input.end;
            Tag = input.tag;
        }
    }

    public class UrlTag : BaseEntity
    {
        public Uri Url { get; set; }
        public Uri ExpandedUrl { get; set; }
        public string DisplayUrl { get; set; }
        public List<Image> Images { get; set; }
        public string Status { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Uri UnwoundUrl { get; set; }

        public UrlTag() { }
        public UrlTag(dynamic input)
        {
            Start = input.start;
            End = input.end;
            Url = new Uri(input.url);
            ExpandedUrl = new Uri(input.expanded_url);
            DisplayUrl = input.display_url;

            if (Helpers.HasProperty(input, "status"))
            {
                Status = input.status.ToString();
            }
            if (Helpers.HasProperty(input, "title"))
            {
                Title = input.title;
            }
            if (Helpers.HasProperty(input, "description"))
            {
                Description = input.description;
            }
            if (Helpers.HasProperty(input, "unwound_url"))
            {
                UnwoundUrl = new Uri(input.unwound_url);
            }
            if (Helpers.HasProperty(input, "images"))
            {
                List<Image> images = new List<Image>();
                foreach (dynamic pic in input.images)
                {
                    images.Add(new Image(pic));
                }
                Images = images;
            }

        }
    }

    public class Image
    {
        Uri Url { get; set; }
        long Width { get; set; }
        long Height { get; set; }

        public Image() { }
        public Image(dynamic input)
        {
            Url = new Uri(input.url);
            Width = input.width;
            Height = input.height;
        }
    }

    public class WithheldContent
    {
        public bool Copyright { get; set; }
        public List<string> CountryCodes { get; set; }

        public WithheldContent() { }
        public WithheldContent(dynamic input)
        {
            if (input == null)
            {
                return;
            }
            Copyright = input.copyright;
            CountryCodes = input.country_codes;
        }
    }

    public class Poll
    {
        public string Id { get; set; }
        public List<PollOptions> Options { get; set; }

        public long Duration { get; set; }
        public DateTime EndDateTime { get; set; }

        public string VotingStatus { get; set; }

        public Poll() { }
        public Poll(dynamic input) {

            Id = input.id;
            EndDateTime = input.end_datetime;
            Duration = input.duration_minutes;
            VotingStatus = Helpers.ToTitleCase(input.voting_status);

            List<PollOptions> pollOptions = new List<PollOptions>();
            foreach (dynamic item in input.options)
            {
                pollOptions.Add(new PollOptions(item));
            }
            Options = pollOptions;
        }
    }

    public class PollOptions
    {
        public long Position { get; set; }
        public string Label { get; set; }
        public long Votes { get; set; }

        public PollOptions() { }
        public PollOptions(dynamic input)
        {
            Position = input.position;
            Label = input.label;
            Votes = input.votes;
        }
    }

    public class Place
    {
        //https://developer.twitter.com/en/docs/twitter-api/v1/data-dictionary/object-model/geo
        public string Fullname { get; set; }
        public string Id { get; set; }
        public List<string> ContainedWithin { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public PSObject Geo { get; set; }
        public string Name { get; set; }
        public string PlaceType { get; set; }

        public Place() { }
        public Place(dynamic input) { 
            
        }

    }
    
}
