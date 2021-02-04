using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KWweathermap
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        //gets the api key and puts in a constant
        const string API_KEY = "aa247dcb86b3cdc8a25073285ea02461";
        public MainPage()
        {
            InitializeComponent();
        }
        /// <summary>
        /// gets the tempertatue and city name from the api and sens it to the weather page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnShowTemp_Clicked(object sender, EventArgs e)
        {
            //if a zip code is entered
            if (EntryZipCode.Text != "")
            {
                //uses a client
                using (WebClient wc = new WebClient())
                {
                    //
                    try
                    {
                        //standard json
                        string json = wc.DownloadString($"http://api.openweathermap.org/data/2.5/weather?zip={EntryZipCode.Text}&appid={API_KEY}&units=imperial");
                        //parse the json to focus on the parent tags
                        JObject jo = JObject.Parse(json);
                        //created json object and parse it into the main 
                        JObject main = JObject.Parse(jo["main"].ToString());
                        
                        //grabs and pulls the temp and puts into string
                        WeatherGV.CurTemp= main["temp"].ToString();
                        //grabs and pulls low temp and puts into string
                        WeatherGV.LowTemp = main["temp_min"].ToString();
                        //grsbd the high temp and puts into an array
                        WeatherGV.HighTemp = main["temp_max"].ToString();
                        //grabs the city name puts it out into an array
                        WeatherGV.CityName = jo["name"].ToString();
                        //takes you to the weather page
                        Navigation.PushAsync(new WeatherPage());
                    }
                    //displays an error
                    catch (Exception ex)
                    {
                        //displays error
                        DisplayAlert("error", ex.Message, "close");
                    }
                }
            }
            else
            {
                //error to type in zip
                DisplayAlert("invalid input", "please type in a zip code", "Close");
            }
        }
    }
}
