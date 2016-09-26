using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FourSquareApp.Models
{
    // urlhttp://json2csharp.com/

    public class Venue
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public virtual Location Location { get; set; }
        public static List<Venue> GetVenues()
        {
            
            var client = new RestClient("https://api.foursquare.com/v2/");
            var request = new RestRequest("venues/search?ll=40.7,-74&client_id=XTHMGYYKXJIEAQCYGSDRZIW5W3CNSJRES0CDBDHCSUNALGFU&client_secret=ZIZVKXIGNL3UIYOEUKTFICX5JHHQIQJCF4G5NSUWWTH4ZNV5&v=20160926", Method.GET);
            var response = new RestResponse();
            Task.Run(async () =>
            {
                response = await GetResponseContentAsync(client, request) as RestResponse;
            }).Wait();
            JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(response.Content);
            var venuesList = JsonConvert.DeserializeObject<List<Venue>>(jsonResponse["response"]["venues"].ToString());

            
            return venuesList;

        }
        public void Send()
        {
            var client = new RestClient("https://api.foursquare.com/v2/");
            var request = new RestRequest("venues/search?ll=40.7,-74&client_id=XTHMGYYKXJIEAQCYGSDRZIW5W3CNSJRES0CDBDHCSUNALGFU&client_secret=ZIZVKXIGNL3UIYOEUKTFICX5JHHQIQJCF4G5NSUWWTH4ZNV5&v=20160926", Method.POST);
            request.AddParameter("Name", Name);
            client.ExecuteAsync(request, response => {
                Console.WriteLine(response.Content);
            });
        }
        public static Task<IRestResponse> GetResponseContentAsync(RestClient theClient, RestRequest theRequest)
        {
            var tcs = new TaskCompletionSource<IRestResponse>();
            theClient.ExecuteAsync(theRequest, response =>
            {
                tcs.SetResult(response);
            });
            return tcs.Task;
        }
    }
}
