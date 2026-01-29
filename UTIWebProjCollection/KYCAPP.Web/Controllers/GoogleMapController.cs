using KYCAPP.Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace KYCAPP.Web.Controllers
{
    public class GoogleMapController : Controller
    {
        // GET: GoogleMap

        public ActionResult Index()
        {
            // Process();
            return View();
        }

        public ActionResult GetGoogleMapDetails(GoogleMapSearchActivities objSea)
        {
            GoogleMap obj = new GoogleMap();

            return new JsonNetResult(obj.GetGogleMapDetails(objSea));
        }


        public void Process()
        {

            GoogleMap obj = new GoogleMap();

            //string jsonData = JsonConvert.SerializeObject(obj.GetGogleMapDetails_1());


            var lstGM = obj.GetGogleMapDetails_1();
            // List<GoogleMapModel> lstGM = obj.GetGogleMapDetails_1();



            //List<string> addresses = lstGM.Select(item => item.CONCAT_ADD, item2=> item2.SRNO).ToList();


            //List<string> addresses = JsonConvert.DeserializeObject<List<string>>(jsonData);
            List<GeocodingResult> geocodingResults = new List<GeocodingResult>();

            // Google Maps Geocoding API endpoint
            string apiUrl = "https://maps.googleapis.com/maps/api/geocode/json";

            // Replace with your Google Maps API key
            string apiKey = "AIzaSyDZy_xb77RUTKb5qXUukWIo80cQtAhZpGA";

            foreach (var item in lstGM)
            {

                GeocodingResult result = GeocodeAddress(apiUrl, apiKey, item.CONCAT_ADD);
                if (result != null)
                {
                    // obj.UpdateLongLat(item.SRNO, result.Address, result.Longitude, result.Latitude);
                    geocodingResults.Add(result);
                }
            }

            // Return JSON data using the JsonResult class
            // return Json(geocodingResults, JsonRequestBehavior.AllowGet);
        }

        private GeocodingResult GeocodeAddress(string apiUrl, string apiKey, string address)
        {
            try
            {
                WebClient client = new WebClient();
                client.Encoding = Encoding.UTF8;
                string url = $"{apiUrl}?address={Uri.EscapeDataString(address)}&key={apiKey}";
                string response = client.DownloadString(url);


                dynamic jsonResponse = JsonConvert.DeserializeObject(response);

                GeocodingResult result = new GeocodingResult
                {
                    Address = address
                };

                // Extract latitude and longitude based on the response structure
                if (jsonResponse != null && jsonResponse.status == "OK" && jsonResponse.results.Count > 0)
                {
                    var location = jsonResponse.results[0].geometry.location;
                    result.Latitude = location.lat;
                    result.Longitude = location.lng;
                }


                return result;

                //GeocodingResponse geocodingResponse = JsonConvert.DeserializeObject<GeocodingResponse>(response);
                //if (geocodingResponse.Status == "OK" && geocodingResponse.Results.Count > 0)
                //{
                //    //double latitude = geocodingResponse.Results[0] .geometry.location.lat;
                //    //double longitude = geocodingResponse.Results[0].geometry.location.lng;

                //    //return new GeocodingResult
                //    //{
                //    //    Address = address,
                //    //    Latitude = latitude,
                //    //    Longitude = longitude
                //    //};
                //    var firstResult = geocodingResponse.Results[0];
                //    return new GeocodingResult
                //    {
                //        Address = address,
                //        Latitude = firstResult.Latitude,
                //        Longitude = firstResult.Longitude
                //    };
                //}
            }
            catch (Exception ex)
            {
                CommonHelper.WriteLog("Error in GeocodeAddress(address: " + address + ")" + ex.Message);
            }

            return null;
        }

        public ActionResult GetGogleMapDetails_1()
        {
            GoogleMap obj = new GoogleMap();
            return new JsonNetResult(obj.GetGogleMapDetails_1());
        }

    }
}

public class GeocodingResult
{
    public string Address { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}

public class GeocodingResponse
{
    public string Status { get; set; }
    public List<GeocodingResult> Results { get; set; }
}