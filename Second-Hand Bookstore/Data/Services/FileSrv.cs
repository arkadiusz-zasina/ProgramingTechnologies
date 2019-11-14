using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Data.DataModels;
using Data.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Data.Services
{
    public class FileSrv : IFileSrv
    {
        private IBookSrv bookSrv;

        public FileSrv(IBookSrv bookSrv)
        {
            this.bookSrv = bookSrv;
        }

        public void FillFromFile()
        {
            List<tBook> books;
            using (StreamReader file = File.OpenText("books.txt"))
            {
                JsonSerializer serializer = new JsonSerializer();
                books = JsonConvert.DeserializeObject<List<tBook>>(file.ReadToEnd());
            }
            foreach(var book in books)
            {
                bookSrv.CreateBook(book);
            }
        }
        public void save(List<tBook> listToSave)
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Converters.Add(new JavaScriptDateTimeConverter());
            serializer.NullValueHandling = NullValueHandling.Ignore;

            using (StreamWriter sw = new StreamWriter("books.txt"))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, listToSave);
            }
        }
    }
}
