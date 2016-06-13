using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using CareApp.Models;
using Newtonsoft.Json.Linq;

namespace CareApp.Data
{
    public class RESTService
    {
        HttpClient client;
        public List<Usuario> Users { get; set; }

        public RESTService()
        {
            client = new HttpClient();
            //todo: why?
            client.MaxResponseContentBufferSize = 256000;
        }

        public async Task<List<Usuario>> GetUsers()
        {
            Users = new List<Usuario>();
            //no podemos usar localhost porque queremos que se conecte a la laptop
            //por eso usamos su IP local
            //todo: change ip to variable in a file
            var uri = "http://192.168.0.100:5000/usuario";
            try
            {
                var response = await client.GetAsync(uri);
                if(response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var jsonResponse = JObject.Parse(jsonString);
                    //resources string is due to sandman2ctl settings
                    //var objResponse = (JObject)jsonResponse["resources"];
                    var objResponse = jsonResponse["resources"];
                    Users = JsonConvert.DeserializeObject<List<Usuario>>(objResponse.ToString());
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }
            return Users;
        }
    }
}
