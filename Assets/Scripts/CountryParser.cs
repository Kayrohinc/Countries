using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using fastJSON;

namespace Country
{
		public class Parametrs
		{
				public string code2l;
				public string name;
				public string official_name;
			



    
		}

		public class CountryParser:MonoBehaviour
		{
				public TextAsset JsonText;
				public static System.Collections.Generic.List<Parametrs> ListOfCountries;

				public void PushAllToList ()
				{ 
						ListOfCountries = JSON.Instance.ToObject<List<Parametrs>> (JsonText.text);

				}

		}







}