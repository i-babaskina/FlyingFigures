using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Practice
{

    class Support
    {
        public static int GetMin(int a, int b)
        {
            if (a < b) return a;
            else return b;
        }

        public static void SerializeToBin(ICollection<Figure> figures, string fullFileName)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(fullFileName, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, figures);
            stream.Close();
        }

        public static void SerializeToJson(List<Figure> mainFigures, string fullFileName)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };
            string figures = JsonConvert.SerializeObject(mainFigures, settings);
            StreamWriter sw = new StreamWriter(fullFileName);
            sw.WriteLine(figures);
            sw.Close();
        }

        public static List<Figure> Deserealize(string fullFileName)
        {
            var res = new List<Figure>();
            StreamReader sr = new StreamReader(fullFileName);
            if (fullFileName.Contains(".txt") || !fullFileName.Contains("."))
            {
                JsonSerializerSettings settings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto,
                    MissingMemberHandling = MissingMemberHandling.Error
                };
                string figures = sr.ReadLine();
                res = JsonConvert.DeserializeObject<List<Figure>>(figures, settings);
            }
            if (fullFileName.Contains(".bin"))
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(fullFileName, FileMode.Open, FileAccess.Read, FileShare.Read);                
                res = (List<Figure>)formatter.Deserialize(stream);
            }

            if (fullFileName.Contains(".xml"))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Figure>));
                Stream stream = new FileStream(fullFileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                res = (List<Figure>)xmlSerializer.Deserialize(stream);
            }
            return res;
        }

        public static void SerializeToXml(List<Figure> figures, string fullFileName)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Figure>));
            FileStream fs = new FileStream(fullFileName, FileMode.Create, FileAccess.Write, FileShare.None);
            xmlSerializer.Serialize(fs, figures);
        }

        
    }
}
