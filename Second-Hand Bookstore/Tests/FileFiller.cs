using System;
using System.Collections.Generic;
using System.IO;
using Data.DataModels;
using Data.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Tests
{
    public class FileFiller : IFiller
    {
        public DataContainer Fill()
        {
            List<tBook> books;
            using (StreamReader file = File.OpenText(@"..\..\..\books.txt"))
            {
                JsonSerializer serializer = new JsonSerializer();
                books = JsonConvert.DeserializeObject<List<tBook>>(file.ReadToEnd());
            }

            return new DataContainer(books, null, null, null);
        }
        public void save(List<tBook> listToSave)
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Converters.Add(new JavaScriptDateTimeConverter());
            serializer.NullValueHandling = NullValueHandling.Ignore;

            using (StreamWriter sw = new StreamWriter(@"..\..\..\books.txt"))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, listToSave);
            }
        }
    }
}
