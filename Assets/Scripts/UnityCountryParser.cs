using UnityEngine;
using System.Collections;
using Country;


public class UnityCountryParser :CountryParser
{
		void Awake ()
		{
       
				base.PushAllToList ();
				Debug.Log (ListOfCountries [0].name);
     
				Texture2D temp = Resources.Load (ListOfCountries [0].code2l) as Texture2D;
				GetComponent<GUITexture> ().texture = temp;


		}

		 

   
}
