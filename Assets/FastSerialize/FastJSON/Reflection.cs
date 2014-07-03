using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;

namespace fastJSON
{
    internal struct Getters
    {
        public string Name;
        public Reflection.GenericGetter Getter;
        public Type propertyType;
    }

    internal sealed class Reflection
    {
        public readonly static Reflection Instance = new Reflection();
        private Reflection()
        {
        }

        public bool ShowReadOnlyProperties = false;
        internal delegate object GenericSetter(object target, object value);
        internal delegate object GenericGetter(object obj);
        private delegate object CreateObject();
        
        private SafeDictionary<Type, string> _tyname = new SafeDictionary<Type, string>();
        private SafeDictionary<string, Type> _typecache = new SafeDictionary<string, Type>();

        private SafeDictionary<Type, List<Getters>> _getterscache = new SafeDictionary<Type, List<Getters>>();

        #region [   PROPERTY GET SET   ]
        internal string GetTypeAssemblyName(Type t)
        {
            string val = "";
            if (_tyname.TryGetValue(t, out val))
                return val;
            else
            {
                string s = t.AssemblyQualifiedName;
                _tyname.Add(t, s);
                return s;
            }
        }

        internal Type GetTypeFromCache(string typename)
        {
            Type val = null;
            if (_typecache.TryGetValue(typename, out val))
			{
				//UnityEngine.Debug.Log(val);
                return val;
			}
            else
            {
				//мой колхоз ))
				//UnityEngine.Debug.Log(typename);
                Type t = Type.GetType(typename);

				if(t == null && typename.Contains("Models."))
				{
					string new_Type_name = typename.Split(',')[0];
					t = Type.GetType(new_Type_name);
				}
				else if(t == null && typename.Contains("UnityEngine.Vector3"))
				{
					t = typeof(UnityEngine.Vector3);
				}
				else if(t == null && typename.Contains("UnityEngine.Color"))
				{
					t = typeof(UnityEngine.Color);
				}
                _typecache.Add(typename, t);
                return t;
            }
        }

        internal object FastCreateInstance(Type objtype)
		{
			return Activator.CreateInstance(objtype);
		}

		internal static GenericSetter CreateSetField(Type type, FieldInfo fieldInfo)
		{
			//UnityEngine.Debug.Log(type + " " + fieldInfo);
			return new GenericSetter((target, value) => {fieldInfo.SetValue(target,value); return target;});
		}

        internal static GenericSetter CreateSetMethod(Type type, PropertyInfo propertyInfo)
		{
			return new GenericSetter((target, value) => {propertyInfo.SetValue(target,value, null); return target;});
		}

		internal static GenericGetter CreateGetField(Type type, FieldInfo fieldInfo)
		{
			return new GenericGetter((target) => {return fieldInfo.GetValue(target); });
		}

		internal static GenericGetter CreateGetMethod(Type type, PropertyInfo propertyInfo)
		{
			return new GenericGetter((target) => {return propertyInfo.GetValue(target, null); });
		}

        internal List<Getters> GetGetters(Type type)
        {
            List<Getters> val = null;
            if (_getterscache.TryGetValue(type, out val))
                return val;

            PropertyInfo[] props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            List<Getters> getters = new List<Getters>();
            foreach (PropertyInfo p in props)
            {
                if (!p.CanWrite && ShowReadOnlyProperties == false) continue;
                
                object[] att = p.GetCustomAttributes(typeof(System.Xml.Serialization.XmlIgnoreAttribute), false);
                if (att != null && att.Length > 0)
                    continue;

                GenericGetter g = CreateGetMethod(type, p);
                if (g != null)
                {
                    Getters gg = new Getters();
                    gg.Name = p.Name;
                    gg.Getter = g;
                    gg.propertyType = p.PropertyType;
                    getters.Add(gg);
                }
            }

            FieldInfo[] fi = type.GetFields(BindingFlags.Instance | BindingFlags.Public);
            foreach (var f in fi)
            {
                object[] att = f.GetCustomAttributes(typeof(System.Xml.Serialization.XmlIgnoreAttribute), false);
                if (att != null && att.Length > 0)
                    continue;

                GenericGetter g = CreateGetField(type, f);
                if (g != null)
                {
                    Getters gg = new Getters();
                    gg.Name = f.Name;
                    gg.Getter = g;
                    gg.propertyType = f.FieldType;
                    getters.Add(gg);
                }
            }

            _getterscache.Add(type, getters);
            return getters;
        }

        #endregion
    }
}
