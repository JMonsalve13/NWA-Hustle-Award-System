using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Threading.Tasks;

namespace HustleCardsServer
{
    public static class EasyQueary
    {
        public static char OpConversion(string op)
        {
            if (op == "eq") return '=';
            else if (op == "st") return '<';
            else if (op == "en") return '>';
            else if (op == "co") return '|';
            else return '?';
        }

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
