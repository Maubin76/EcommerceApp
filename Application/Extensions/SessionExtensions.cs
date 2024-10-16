using Microsoft.AspNetCore.Http;
using Newtonsoft.Json; 
using System;

namespace Application.Extensions 
{
    // This class contains extension methods for ISession interface
    public static class SessionExtensions
    {
        // Converts an object into a json chain before saving it
        public static void SetObject<T>(this ISession session, string key, T value)
        {
            // Use SetString method from ISession to save serialized object
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        //
        // Retrieves an object from the session by deserializing it from a JSON string
        public static T GetObject<T>(this ISession session, string key)
        {
            // Get the value associated to the key
            var value = session.GetString(key);
            // If the value is null, it returns the default value for type T. 
            // Otherwise, it deserializes the JSON string into an object of type T and returns it.
            return value == null ? default : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
