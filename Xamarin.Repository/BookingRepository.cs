using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Entity;
using Xamarin.Repository.Interfaces;

namespace Xamarin.Repository
{
    public class BookingRepository : IBookingRepository
    {
        public async Task<List<Booking>> GetBookingsBySpaceId(int idspace)
        {
            try
            {
                string apiUrl = $"https://c6m3zvixpg.execute-api.us-east-1.amazonaws.com/prod/bookings/getbyspaceid/{idspace}";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiUrl);
                request.Method = "GET";
                request.ContentType = "application/json";
                request.Accept = "application/json";

                StreamReader responseReader = new StreamReader(request.GetResponse().GetResponseStream());
                string responseData = await responseReader.ReadToEndAsync();
                responseReader.Close();

                return JsonConvert.DeserializeObject<List<Booking>>(responseData);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> CreateBooking(Booking bookingObject)
        {
            try
            {
                var client = new RestClient("https://c6m3zvixpg.execute-api.us-east-1.amazonaws.com/prod/bookings");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", JsonConvert.SerializeObject(bookingObject), ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
