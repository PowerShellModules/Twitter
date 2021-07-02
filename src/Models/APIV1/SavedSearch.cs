﻿using System;
using BluebirdPS.Models;

namespace BluebirdPS.Models.APIV1
{
    public class SavedSearch : TwitterObject
    {
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; }
        public string Query { get; set; }

        public SavedSearch() { }
        public SavedSearch(dynamic input)
        {
            OriginalObject = input;

            Id = input.id_str;
            CreatedAt = Core.Helpers.ConvertFromV1Date(input.created_at);
            Name = input.name;
            Query = input.query;
        }
    }

}
