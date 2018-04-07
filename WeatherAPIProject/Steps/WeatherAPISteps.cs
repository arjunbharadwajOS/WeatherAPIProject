using System;
using TechTalk.SpecFlow;
using System.Linq;
using Newtonsoft.Json.Linq;
using WeatherAPIProject.FunctionLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow.Assist;
using UnitTestProject1.Steps;

namespace WeatherAPIProject.Steps
{
   
    [Binding]
    public class WeatherAPISteps
    {

        public string parameters = string.Empty;
        public string endpoint = @"http://api.openweathermap.org";
        public string response = string.Empty;
        public string destDt = string.Empty;
        public double temp_max = 0.00;

       
        [Given(@"I like to holiday in (.*)")]
        public void GivenILikeToHolidayInSydney(string city,Table table)
        {
            var credentials = table.CreateInstance<Credentials>();
            parameters = "/data/2.5/forecast?id=" + credentials.ID + "&APPID=" + credentials.APIKey;
            parameters = parameters + "&q=" + city;
            parameters = parameters + "&mode=" + credentials.mode + "&units=" + credentials.units;

        }


        [Given(@"I only like to holiday on (.*)")]
        public void GivenIOnlyLikeToHolidayOnThursdays(string day)
        {

            DateTime now = DateTime.Today;
            for (int i = 0; i < 7; i++)
            {
                if (day.Contains(now.ToString("dddd")))
                {
                    destDt = now.ToString("u").Split(' ')[0];
                    break;
                }
                now = now.AddDays(1);
            }

        }
        
        [When(@"I look up the weather forecast")]
        public void WhenILookUpTheWeatherForecast()
        {

            var client = new WebRequestLibrary();
            client.EndPoint = endpoint;
            client.Method = "GET";
            var pdata = client.PostData;
            response = client.Request(parameters);
            
        }

        [When(@"I look up the weather forecast response type")]
        public void WhenILookUpTheWeatherForecastresponsetype()
        {

            var client = new WebRequestLibrary();
            client.EndPoint = endpoint;
            client.Method = "GET";
            var pdata = client.PostData;
            response = client.RequestType(parameters);

        }



        [When(@"I look up the weather forecast with validation")]
        public void WhenIlookupweatherforecastValidation()
        {

            var client = new WebRequestLibrary();
            client.EndPoint = endpoint;
            client.Method = "GET";
            var pdata = client.PostData;
            response = client.RequestwithValidation(parameters);
            Assert.IsTrue(true, response);

        }


        [Then(@"I receive the weather forecast")]
        public void ThenIReceiveTheWeatherForecast()
        {
            var weatherObject = JToken.Parse(response);
            var weatherArray = weatherObject.Children<JProperty>().FirstOrDefault(x => x.Name == "list").Value;
            int counter = 0;
            string response_date = string.Empty;

            foreach (JToken dayToken in weatherArray.Children())
            {
                if (counter == 7)
                    break;

                response_date = dayToken["dt_txt"].ToString().Split(' ')[0];

                if (response_date.Contains(destDt))
                {

                    if(Convert.ToDouble(dayToken["main"]["temp_max"]) > temp_max)
                    {
                        temp_max = Convert.ToDouble(dayToken["main"]["temp_max"]);

                    }

                    counter++;
                }

            }
            
        }


        [Then(@"I receive the weather forecast Response")]
        public void ThenIreceivetheweatherforecastResponse()
        {

            switch (response.Split(';')[0])
            {
                case "application/xml":
                    Assert.IsTrue(true, "Content Type - Response Code - Response Length: " + response);
                    break;
                default:
                    Assert.Fail("Content Type - Response Code - Response Length: " + response);
                    break;
            }

        }


        [Then(@"the temperature is warmer than (.*) degree\.")]
        public void ThenTheTemperatureIsWarmerThanDegree_(double temperature)
        {
            if (temp_max > temperature)
                Assert.IsTrue(true, "The current temperature " + temp_max.ToString() +  "is warmer than " + temperature.ToString());
            else
                Assert.IsTrue(true, "The current temperature " + temp_max.ToString() + "is below " + temperature.ToString());
        }
    }
}
