﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;

namespace SyndicationFeedX
{
    static class Converter
    {
        public static bool TryParseValue<T>(string value, out T result)
        {
            result = default(T);

            Type type = typeof(T);

            //
            // String
            if (type == typeof(string))
            {
                result = (T)(object)value;
                return true;
            }

            if (value == null)
            {
                return false;
            }

            //
            // DateTimeOffset
            if (type == typeof(DateTimeOffset))
            {
                if (DateTimeUtils.TryParseDate(value, out DateTimeOffset dt))
                {
                    result = (T)(object)dt;
                    return true;
                }

                return false;
            }

            //
            // DateTime
            if (type == typeof(DateTime))
            {
                if (DateTimeUtils.TryParseDate(value, out DateTimeOffset dt))
                {
                    result = (T)(object) dt.DateTime;
                    return true;
                }

                return false;
            }

            //
            // TODO: being added in netstandard 2.0
            //if (type.GetTypeInfo().IsEnum)
            //{
            //    if (Enum.TryParse(typeof(T), value, true, out T o)) {
            //        result = (T)(object)o;
            //        return true;
            //    }
            //}

            //
            // Uri
            if (type == typeof(Uri))
            {
                if (UriUtils.TryParse(value, out Uri uri))
                {
                    result = (T)(object)uri;
                    return true;
                }

                return false;
            }

            //
            // Fall back default
            return (result = (T)Convert.ChangeType(value, typeof(T))) != null;
        }
    }
}
