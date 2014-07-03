using System;
using System.Collections.Generic;

using fastJSON;
using FSerialize.Utilities;
using System.Reflection;

namespace FSerialize
{
	public static class FastSerialize
	{
		//Настройки для парсера
		static public void SetSetting()
		{
			//fastJSON.JSON.Instance.Parameters.EnableAnonymousTypes = true;
		}
		
		//сериализатор
		//сириализуем обьект
		static public byte[] Serialize(object obj)
		{
			SetSetting();
			return StringCompressor.CompressString(fastJSON.JSON.Instance.ToJSON(obj));
		}
		
		//сериализует лист
		static public byte[] Serialize(List<object> slist)
		{
			SetSetting();
			return StringCompressor.CompressString(fastJSON.JSON.Instance.ToJSON(slist));
		}
		
		//сериализует дкшенари
		static public byte[] Serialize(Dictionary<object, object> sdict)
		{
			SetSetting();
			return StringCompressor.CompressString(fastJSON.JSON.Instance.ToJSON(sdict));
		}
		
		//сереализуем в стринг
		static public string SerializeToStr( object obj )
		{
			SetSetting();
			return fastJSON.JSON.Instance.ToJSON(obj);
		}
		
		//десериализатор----------------------
		//десериализуем обьект
		static public object DeSerialize<T>(byte[] data)
		{
			SetSetting();
			return fastJSON.JSON.Instance.ToObject<T>(StringCompressor.DecompressString(data));
		}
		
		//десериализуем лист
		static public List<T> DeSerializeList<T>(byte[] data)
		{
			SetSetting();
			return fastJSON.JSON.Instance.ToObject<List<T>>(StringCompressor.DecompressString(data));
		}

		static public List<T> DeSerializeList<T>(string data)
		{
			SetSetting();
			return fastJSON.JSON.Instance.ToObject<List<T>>(data);
		}
		
		//десериализуем дикшенари
		static public Dictionary<T, P> DeSerializeDict<T, P>(byte[] data)
		{
			SetSetting();
			return fastJSON.JSON.Instance.ToObject<Dictionary<T, P>>(StringCompressor.DecompressString(data));
		}

		static public Dictionary<T, P> DeSerializeDict<T, P>(string data)
		{
			SetSetting();
			return fastJSON.JSON.Instance.ToObject<Dictionary<T, P>>(data);
		}
		
		//десериализуем строку
		static public object DeserializeFomStr<T>(string date)
		{
			SetSetting();
			return fastJSON.JSON.Instance.ToObject<T>(date);
		}
		
		///----------------- Вспомогательные методы
		//копируем класы после десериализации
		static public void CopyClassData(object o_in, object o_out)
		{
			Type curType = o_out.GetType();
			FieldInfo[] properties = curType.GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);
			int counter = 0;
			
			foreach (FieldInfo property in properties)
			{
				try
				{
					FieldInfo property2 = o_in.GetType().GetField(property.Name);
					property.SetValue(o_out, property2.GetValue(o_in));
				}
				catch (Exception exception)
				{
					//UnityEngine.Debug.Log("No property type");
				}
				counter++;
			}
		}
	}
}
