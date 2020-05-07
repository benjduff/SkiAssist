using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using SkiAssistWebsite.Controllers;

namespace SkiAssistWebsite.Helper
{
    public abstract class HttpHelper
    {
        //private static readonly Uri BaseAddress = new Uri("http://localhost:56128");
        private static readonly Uri BaseAddress = new Uri("https://skiassistwebapi.azurewebsites.net");

        private static readonly HttpClient Client = new HttpClient() {BaseAddress = BaseAddress};

        #region Post methods
        
        /// <summary>
        /// Post
        /// </summary>
        /// <param name="json"></param>
        /// <param name="route"></param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> Post(string json, string route)
        {
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = Client.PostAsync(route, content);
            return await response;
        }

        #endregion

        #region Get methods

        public static async Task<T> Get<T>(string route, int id)
        {
            var responseAsync = Client.GetAsync(route + "/" + id).Result;
            var jsonAsString = await responseAsync.Content.ReadAsStringAsync();

            var deserializedStudentObject = JsonConvert.DeserializeObject<T>(jsonAsString);

            return deserializedStudentObject;
        }

        public static async Task<List<T>> GetList<T>(string route)
        {
            var responseAsync = Client.GetAsync(route).Result;
            var jsonAsString = await responseAsync.Content.ReadAsStringAsync();

            var deserializedStudentObject = JsonConvert.DeserializeObject<List<T>>(jsonAsString);

            return deserializedStudentObject;
        }

        public static async Task<List<T>> GetList<T>(string route, int id)
        {
            var responseAsync = Client.GetAsync(route + "/" + id).Result;
            var jsonAsString = await responseAsync.Content.ReadAsStringAsync();

            var deserializedStudentObject = JsonConvert.DeserializeObject<List<T>>(jsonAsString);

            return deserializedStudentObject;
        }

        #endregion

        #region Put methods

        public static void LinkStudentTicket(string json, string route)
        {
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var responseAsync = Client.PutAsync(route, content);
        }

        #endregion


    }
}