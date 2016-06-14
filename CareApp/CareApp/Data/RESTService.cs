using System;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using CareApp.Models;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace CareApp.Data
{
    public class RESTService
    {
        HttpClient client;
        //according to sandman2ctl default values
        string propertyName = "resources";
        string baseRESTUri = "http://192.168.0.100:5000/";
        public RESTService()
        {
            client = new HttpClient();
            //todo: why?
            client.MaxResponseContentBufferSize = 256000;
        }

        public async Task SaveUser(Usuario usr)
        {
            //todo: remove this shit
            //usr = new Usuario
            //{
            //    Username = "thor",
            //    Password = "thor",
            //    Nombre = "hector",
            //    Apellido = "beltran",
            //    //todo: check this shit
            //    Tipo = true,
            //    //todo: añadir cuidante
            //    Telefono = "666"
            //};
            var uri = baseRESTUri + "usuario";
            try
            {
                var json = JsonConvert.SerializeObject(usr);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response;
                response = await client.PostAsync(uri, content);
                if(response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"User saved");
                }
                else
                {
                    Debug.WriteLine(response.StatusCode.ToString());
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }
        }

        public async Task<List<Usuario>> GetUsers()
        {
            var users = new List<Usuario>();
            //no podemos usar localhost porque queremos que se conecte a la laptop
            //por eso usamos su IP local
            //todo: change ip to variable in a file
            var uri = baseRESTUri + "usuario";
            try
            {
                var response = await client.GetAsync(uri);
                if(response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var parsedJson = JObject.Parse(jsonString);
                    var objResponse = parsedJson[propertyName];
                    users = JsonConvert.DeserializeObject<List<Usuario>>(objResponse.ToString());
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }
            return users;
        }
    }
}
