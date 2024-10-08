using Microsoft.AspNetCore.Http;
using Newtonsoft.Json; // Assure-toi d'avoir la bibliothèque Newtonsoft.Json installée
using System;

namespace Application.Extensions // Ajuste l'espace de noms selon ta structure
{
    public static class SessionExtensions
    {
        public static void SetObject<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObject<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
