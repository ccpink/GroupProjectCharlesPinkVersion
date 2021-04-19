﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace GroupProject
{

    public class FetchData
    {
        public FetchData()
        {

        }


        public async Task<List<ProvinceModel>> GetData()
        {
            List<ProvinceModel> provinces = new List<ProvinceModel>();

            string baseUrl = "https://api.opencovid.ca/summary";

            string province;
            long activeCases;
            long cumalativeCases;
            long cumalativeDeaths;
            long cumalativeVaccine;
            long cumalativeRecovered;
            long cumalativeTesting;
            //string provImage;

            try
            {
                using (HttpClient client = new HttpClient())
                {

                    using (HttpResponseMessage res = await client.GetAsync(baseUrl))
                    {

                        using (HttpContent content = res.Content)
                        {
                            var data = await content.ReadAsStringAsync();

                            if (data != null)
                            {

                                var dataObj = JObject.Parse(data);
                                int length = ((JArray)dataObj["summary"]).Count;


                                for (int i = 0; i < length; i++)
                                {
                                    if (!($"{dataObj["summary"][i]["province"]}").Equals("Repatriated"))
                                    {
                                        province = ($"{dataObj["summary"][i]["province"]}");
                                        activeCases = long.Parse(($"{dataObj["summary"][i]["active_cases"]}"));
                                        cumalativeCases = long.Parse(($"{dataObj["summary"][i]["cumulative_cases"]}"));
                                        cumalativeDeaths = long.Parse(($"{dataObj["summary"][i]["cumulative_deaths"]}"));
                                        cumalativeVaccine = long.Parse($"{dataObj["summary"][i]["cumulative_dvaccine"]}");
                                        cumalativeRecovered = long.Parse(($"{dataObj["summary"][i]["cumulative_recovered"]}"));
                                        cumalativeTesting = long.Parse(($"{dataObj["summary"][i]["cumulative_testing"]}"));

                                        //provImage = GetProvinceFlag(province);

                                        Debug.WriteLine(province + " " + activeCases + " " + cumalativeCases + " " + cumalativeDeaths
                                            + " " + cumalativeVaccine + " " + cumalativeRecovered + " " + cumalativeTesting);

                                        provinces.Add(GetProvinceData(province, activeCases, cumalativeCases, cumalativeDeaths, cumalativeVaccine,
                                            cumalativeRecovered, cumalativeTesting, "No Image Supplied Give Error Pls"));
                                    }

                                }

                            }

                        }
                    }
                }


            }
            catch (Exception ex)
            {
                Debug.WriteLine("Oh noes! Can not read data " + ex.Message);
            }

            return provinces;

        }

        public static ProvinceModel GetProvinceData(string province, long aCases, long cACases, long cDeath, long cVaccine, long cRecovered, long cTesting, string provImage)
        {
            return new ProvinceModel(province, aCases, cACases, cDeath, cVaccine, cRecovered, cTesting, provImage);
        }

        /*public string GetProvinceFlag(string provinceName)
        {
            string pImage = "";


            switch (provinceName)
            {
                case "Alberta":
                    pImage = "Assets/Flag_of_Alberta.png";
                    break;

                case "BC":
                    pImage = "Assets/Flag_of_British_Columbia.png";
                    break;

                case "New Brunswick":
                    pImage = "Assets/Flag_of_New_Brunswick.png";
                    break;

                case "NL":
                    pImage = "Assets/Flag_of_Newfoundland_and_Labrador.png";
                    break;

                case "Nova Scotia":
                    pImage = "Assets/Flag_of_Nova_Scotia.png";
                    break;

                case "Nunavut":
                    pImage = "Assets/Flag_of_Nunavut.png";
                    break;

                case "NWT":
                    pImage = "Assets/Flag_of_the_Northwest_Territories.png";
                    break;

                case "Ontario":
                    pImage = "Assets/Flag_of_Ontario.png";
                    break;

                case "PEI":
                    pImage = "Assets/Flag_of_Prince_Edward_Island.png";
                    break;

                case "Quebec":
                    pImage = "Assets/Flag_of_Quebec.png";
                    break;

                case "Saskatchewan":
                    pImage = "Assets/Flag_of_Saskatchewan.png";
                    break;

                case "Yukon":
                    pImage = "Assets/Flag_of_Yukon.png";
                    break;
                case "Manitoba":
                    pImage = "Assets/Flag_of_Manitoba.png";
                    break;
            }

            return pImage;


        }
*/
    }
}
