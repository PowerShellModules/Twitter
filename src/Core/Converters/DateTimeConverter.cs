﻿using AutoMapper;
using System;

namespace BluebirdPS.Core.Converters
{
    internal class DateTimeConverter : ITypeConverter<DateTimeOffset, DateTime>
    {
        public DateTime Convert(DateTimeOffset source, DateTime destination, ResolutionContext context)
        {
            return source.DateTime;
        }
    }
}
