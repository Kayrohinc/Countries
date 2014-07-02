using UnityEngine;
using System.Collections;
using Newtonsoft.Json;
using UnityEditor;
using System.Collections.Generic;
namespace Country
{
    public class Parametrs
    {

        public string IconPath;
        public string Name;
        public string OfficialName;
        public string Code31;



    
 }

    public class CountryParser:MonoBehaviour
    {
        public TextAsset JsonText;
        public static System.Collections.Generic.List<Parametrs> ListOfCountries;
        public void PushAllToList()
        {
            ListOfCountries = new List<Parametrs>();
            List<Dictionary<string, string>> temp = JsonConvert.DeserializeObject <List<Dictionary<string, string>>>(JsonText.text);
            foreach (Dictionary<string,string> foreachtemp in  temp)
            {
                Parametrs tempPar = new Parametrs();
               tempPar.Code31 = foreachtemp["code3l"];
                tempPar.Name = foreachtemp["name"];
                tempPar.OfficialName = foreachtemp["official_name"];
                tempPar.IconPath = foreachtemp["code2l"].ToLower();
                ListOfCountries.Add(tempPar);
            }
        }
    }
}