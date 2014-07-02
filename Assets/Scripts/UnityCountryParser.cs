using UnityEngine;
using System.Collections;
using Country;
public class UnityCountryParser :CountryParser {
    void Awake()
    {
       
        base.PushAllToList();
       Debug.Log(ListOfCountries[0].Name);
       Debug.Log(ListOfCountries[0].IconPath);
      Texture2D temp=Resources.Load(ListOfCountries[0].IconPath) as Texture2D;
      GetComponent<GUITexture>().texture = temp;
    }
   
}
