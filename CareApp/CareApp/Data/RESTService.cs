using System;
using System.Linq;
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
    //todo: volverla clase estática?
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

        public async Task<bool> SaveUser(Usuario usr)
        {
            var uri = baseRESTUri + "usuario";
            try
            {
                var json = JsonConvert.SerializeObject(usr);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response;
                response = await client.PostAsync(uri, content);
                if(response.IsSuccessStatusCode)
                    Debug.WriteLine(@"User saved");
                else
                    Debug.WriteLine(response.StatusCode.ToString());
                return response.IsSuccessStatusCode;
            }
            catch(Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
                return false;
            }
        }

        public async Task<List<Usuario>> GetUsers()
        {
            List<Usuario> users = null;// = new List<Usuario>();
            //no podemos usar localhost porque queremos que se conecte a la laptop
            //por eso usamos su IP local
            //todo: change ip to variable in a file
            var uri = baseRESTUri + "usuario";
            try
            {
                var response = await client.GetAsync(uri);
                if (!response.IsSuccessStatusCode)
                    return null;
                var jsonString = await response.Content.ReadAsStringAsync();
                var parsedJson = JObject.Parse(jsonString);
                var objResponse = parsedJson[propertyName];
                users = JsonConvert.DeserializeObject<List<Usuario>>(objResponse.ToString());
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }
            return users;
        }


        //Obtenemos los pacientes de un cuidante
        public async Task<List<Usuario>> GetPatientsFromCarer(string username)
        {
            var allUsers = await GetUsers();
            var patients = from patient in allUsers
                           where patient.Cuidante == username
                           select patient;
            return new List<Usuario>(patients);
        }

        public async Task<Usuario> Login(string username, string password)
        {
            var uri = baseRESTUri + "usuario/" + username;
            Usuario user = null;
            try
            {
                var response = await client.GetAsync(uri);
                //error de conexión o algo
                if (!response.IsSuccessStatusCode)
                    return null;

                var jsonString = await response.Content.ReadAsStringAsync();
                var parsedJson = JObject.Parse(jsonString);
                //if user doesn't exist it returns an object with only one property called message
                if (parsedJson["message"] != null)
                    return null;
                //usuario existe
                user = JsonConvert.DeserializeObject<Usuario>(parsedJson.ToString());
                //cargamos sus pacientes
                user.Pacientes = await GetPatientsFromCarer(username);
                return user.Password == password ? user : null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
                return null;
            }
        }
    }
}
