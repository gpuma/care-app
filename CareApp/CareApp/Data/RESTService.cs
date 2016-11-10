using CareApp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CareApp.Data
{
    //todo: volverla clase estática?
    public class RESTService
    {
        HttpClient client;
        //according to sandman2ctl default values
        string propertyName = "resources";
        //string baseRESTUri = "http://10.0.43.40:5000/";
        string baseRESTUri = "http://192.168.0.100:5000/";
        //la cacheamos en memoria
        List<Usuario> allUsers;
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

        public async Task<List<EmergencyConfig>> GetConfigs()
        {
            List<EmergencyConfig> configs = null;
            //no podemos usar localhost porque queremos que se conecte a la laptop
            //por eso usamos su IP local
            var uri = baseRESTUri + "configuracion";
            try
            {
                var response = await client.GetAsync(uri);
                if (!response.IsSuccessStatusCode)
                    return null;
                var jsonString = await response.Content.ReadAsStringAsync();
                var parsedJson = JObject.Parse(jsonString);
                var objResponse = parsedJson[propertyName];
                configs = JsonConvert.DeserializeObject<List<EmergencyConfig>>(objResponse.ToString());
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }
            return configs;
        }

        public async Task<List<Emergencia>> GetEmergencies()
        {
            List<Emergencia> emergencies = null;
            var uri = baseRESTUri + "emergencia";
            try
            {
                var response = await client.GetAsync(uri);
                if (!response.IsSuccessStatusCode)
                    return null;
                var jsonString = await response.Content.ReadAsStringAsync();
                var parsedJson = JObject.Parse(jsonString);
                var objResponse = parsedJson[propertyName];
                emergencies = JsonConvert.DeserializeObject<List<Emergencia>>(objResponse.ToString());
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }
            return emergencies;
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

        string RemoveIdFieldFromJson(string json)
        {
            //we need to remove the id field cause it's auto-incremented
            var tempObj = JObject.Parse(json);
            tempObj["Id"].Parent.Remove();
            return tempObj.ToString();
        }

        string RemoveTZFromJsonDate(string json)
        {
            var tempObj = JObject.Parse(json);
            var temp = tempObj["Timestamp"].ToString();
            //19 es el numero de caracteres incluyendo fecha y hora
            temp = temp.Substring(0, 19);
            tempObj["Timestamp"] = temp;
            return tempObj.ToString();
        }

        public async Task<bool> SaveConfig(EmergencyConfig newConfig)
        {
            var uri = baseRESTUri + "configuracion";
            try
            {
                //TODO: bug: no se puede guardar la hora de una configuración
                var json = JsonConvert.SerializeObject(newConfig,
                    new Newtonsoft.Json.Converters.IsoDateTimeConverter()
                    { DateTimeFormat = "yyyy-MM-dd'T'HH:mm:ss" });
                
                //we need to remove the id field cause it's auto-incremented
                json = RemoveIdFieldFromJson(json);

                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response;
                response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                    Debug.WriteLine(@"CONFIGURACIÓN saved");
                else
                    Debug.WriteLine(response.StatusCode.ToString());
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteConfig(int configId)
        {
            var uri = baseRESTUri + "configuracion/" + configId;
            try
            {
                HttpResponseMessage response;
                response = await client.DeleteAsync(uri);
                if (response.IsSuccessStatusCode)
                    Debug.WriteLine(@"CONFIGURACIÓN deleted");
                else
                    Debug.WriteLine(response.StatusCode.ToString());
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
                return false;
            }
        }

        public async Task<bool> SaveEmergency(Emergencia e, bool update = false)
        {
            var uri = baseRESTUri + "emergencia";
            //para hacer una operación PUT
            //la URL debe apuntar al elemento específico q se quiere actualizar
            if (update)
                uri += "/" + e.Id;
            try
            {
                
                //necesario para la conversión de fechas
                var json = JsonConvert.SerializeObject(e,
                    new Newtonsoft.Json.Converters.IsoDateTimeConverter()
                    { DateTimeFormat= "yyyy-MM-dd'T'HH:mm:ss"  });

                //we need to remove the id field cause it's auto-incremented
                //only when it's a new record
                if (!update)
                    json = RemoveIdFieldFromJson(json);

                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response;
                if(update)
                    response = await client.PutAsync(uri, content);
                else
                    response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                    Debug.WriteLine(@"Emergencia saved");
                else
                    Debug.WriteLine(response.StatusCode.ToString());
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
                return false;
            }
        }

        //Obtenemos los pacientes de un cuidante
        public async Task<List<Usuario>> GetPatientsFromCarer(string username)
        {
            allUsers = await GetUsers();
            var patients = from patient in allUsers
                           where patient.Cuidante == username
                           select patient;
            return new List<Usuario>(patients);
        }

        //Obtenemos las configuraciones de un paciente
        public async Task<List<EmergencyConfig>> GetConfigsFromPatient(string username)
        {
            var allConfigs = await GetConfigs();
            var configs = from config in allConfigs
                           where config.Paciente == username
                           select config;
            return new List<EmergencyConfig>(configs);
        }

        //Obtenemos las emergencias pendientes de todos los pacientes de un cuidante
        public async Task<List<Emergencia>> GetPendingEmergenciesFromCarerPatients(string carer)
        {
            var allEmergencies = await GetEmergencies();
            var emergencies = from emergency in allEmergencies
                          where emergency.Cuidante == carer &&
                                emergency.Estado == false
                          select emergency;
            return new List<Emergencia>(emergencies);
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

                if (user.Password != password)
                    return null;

                //cargamos sus pacientes
                user.Pacientes = await GetPatientsFromCarer(username);
                //cargamos las configuraciones de emergencia (si es que hay)
                user.Configuraciones = await GetConfigsFromPatient(username);
                //si es paciente obtenemos el telefono de su cuidante
                if (!user.Tipo)
                    user.TelCuidante = (from u in allUsers
                                        where u.Username == user.Cuidante
                                        select u.Telefono).FirstOrDefault();
                return user;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
                return null;
            }
        }
    }
}
