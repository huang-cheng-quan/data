using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace Camera_Capture_demo.Helpers
{
	public class XmlHelper02
	{
		/// <summary>     
		/// XML序列化某一类型到指定的文件   
		/// /// </summary>   
		/// /// <param name="filePath"></param>   
		/// /// <param name="obj"></param>  
		/// /// <param name="type"></param>   
		public static void SerializeToXml<T>(T obj, string SaveName, string filePath)
		{
			//if (string.IsNullOrEmpty(filePath))
			//{
				filePath = filePath + "\\" + SaveName + ".xml";
			//}
			try
			{
				using (System.IO.StreamWriter writer = new System.IO.StreamWriter(filePath)) { System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(T)); xs.Serialize(writer, obj); }
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		public static T DeserializeFromXml<T>(string SaveName, string filePath)
		{
			
				filePath = filePath + "\\" + SaveName + ".xml";
			
			try
			{
				if (!System.IO.File.Exists(filePath))
					throw new ArgumentNullException(filePath + " not Exists");
				using (System.IO.StreamReader reader = new System.IO.StreamReader(filePath))
				{
					System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(T));
					T ret = (T)xs.Deserialize(reader);
					return ret;
				}
			}
			catch (Exception ex)
			{
				return default(T);
			}
		}
	}
}
