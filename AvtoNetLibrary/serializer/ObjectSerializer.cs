using System;
using System.IO;
using System.Xml.Serialization;

namespace AvtoNetLibrary.Serializer
{
    public static class ObjectSerializer
    {
        public static string Serialize<T>(T obj)
        {
            if (obj == null)
                return String.Empty;

            XmlSerializer serializer = new XmlSerializer(typeof(T));

            using (StringWriter writer = new StringWriter())
            {
                serializer.Serialize(writer, obj);
                return writer.ToString();
            }
        }

        public static T Deserialize<T>(string serialized)
        {
            if (String.IsNullOrEmpty(serialized))
                return default(T);

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));

                using (StringReader reader = new StringReader(serialized))
                {
                    return (T)serializer.Deserialize(reader);
                }
            }
            catch (InvalidOperationException)
            {
                return default(T);
            }
        }
    }
}
