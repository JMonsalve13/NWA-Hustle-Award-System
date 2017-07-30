using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Threading.Tasks;

namespace HustleCardsServer
{
    /// <summary>
    /// Helper class for converting url query strings into our mezinine query language
    /// </summary>
    public static class EasyQueary
    {
        /// <summary>
        /// Converts the underscore-codes in url query strings into mezinine query language operators
        /// </summary>
        /// <param name="op">the underscore code to convert</param>
        /// <returns>the converted mezinine query language operator</returns>
        public static char OpConversion(string op)
        {
            if (op == "eq") return '=';
            else if (op == "st") return '<';
            else if (op == "en") return '>';
            else if (op == "co") return '|';
            else return '?';
        }

        /// <summary>
        /// Used to mass-convert url query string collection into an array of mezinine query operations
        /// </summary>
        /// <param name="quearyParams">the IQueryCollection pulled from the HTTP request</param>
        /// <param name="allowUpdates">boolean to decide if database-writing operations are allowed</param>
        /// <returns>the array of converted mezinine qeury commands</returns>
        public static string[] ConvertQueary(IQueryCollection quearyParams, bool allowUpdates)
        {
            List<string> parsed = new List<string>();
            foreach (KeyValuePair<string, Microsoft.Extensions.Primitives.StringValues> kv in quearyParams)
            {
                if (!allowUpdates && kv.Key.Contains("_")) continue;
                string key = kv.Key.Split('_')[0];

                string[] split = kv.Value.ToString().Split('_');
                string val = split[0];
                string eq = split[1];

                string op = $"{key}{(kv.Key.Contains("_up") ? '+' : EasyQueary.OpConversion(eq))}{val}";

                parsed.Add(op);
            }
            return parsed.ToArray();
        }

        /// <summary>
        /// A generic way to update the value of a parameter in an object. Currently only supports string data.
        /// </summary>
        /// <typeparam name="T">the type of object that is being updated</typeparam>
        /// <param name="obj">the object being updated</param>
        /// <param name="quearyParams">the string formatted "[fieldName]+[DataToSetFieldTo]"</param>
        /// <returns>the updated object</returns>
        public static T ReflectionUpdate<T>(T obj, string[] quearyParams)
        {
            foreach (string str in quearyParams)
            {
                if (!str.Contains("=")) continue;//Creation only through Post

                string[] split = str.Split('=');
                string var = split[0];
                string val = split[1];

                typeof(T).GetField(var).SetValue(obj, val);
            }
            return obj;
        }
    }
}
