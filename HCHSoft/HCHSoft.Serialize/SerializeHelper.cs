using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HCHSoft.Serialize
{
    public class SerializeHelper
    {
        /// <summary>
        /// 反序列化xml文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static T XmlDeserializeFile<T>(string fileName)
        {
            T entity = default(T);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            entity = (T)xmlSerializer.Deserialize(fileStream);
            fileStream.Flush();
            fileStream.Close();
            return entity;
        }

        /// <summary>
        /// 序列化xml文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="entity"></param>
        public static void XmlSerializeFile<T>(string fileName, T entity)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            FileStream fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            xmlSerializer.Serialize(fileStream, entity);
            fileStream.Flush();
            fileStream.Close();
        }

        /// <summary>
        /// 序列化为json格式的字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static string JsonSerializeString<T>(T entity)
        {
            return JsonConvert.SerializeObject(entity, Formatting.Indented);
        }

        /// <summary>
        /// 序列化为json文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <param name="entity"></param>
        public static void JsonSerializeFile<T>(string fileName, T entity)
        {
            var content = JsonConvert.SerializeObject(entity, Formatting.Indented);
            File.WriteAllText(fileName, content);
        }

        /// <summary>
        /// 反序列化json格式的字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="content"></param>
        /// <returns></returns>
        public static T JsonDeserializeString<T>(string content)
        {
            return JsonConvert.DeserializeObject<T>(content);
        }
        /// <summary>
        /// 反序列化json格式的文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="content"></param>
        /// <returns></returns>
        public static T JsonDeserializeFile<T>(string filename)
        {
            T entity = default(T);
            if (File.Exists(filename))
            {
                string content = File.ReadAllText(filename);
                entity = JsonConvert.DeserializeObject<T>(content);
            }
            return entity;
        }
    }
}
