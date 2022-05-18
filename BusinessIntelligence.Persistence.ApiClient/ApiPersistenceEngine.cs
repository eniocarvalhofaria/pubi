using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json.Linq;
using System.Reflection;

namespace BusinessIntelligence.Persistence
{
    public class ApiPersistenceEngine : PersistenceEngine
    {
        public ApiPersistenceEngine(string apiUrl)
        {
            if (apiUrl.Substring(apiUrl.Length - 1, 1) != "/")
            {
                apiUrl += "/";
            }
            ApiUrl = apiUrl;
        }
        public string ApiUrl { get; set; }
        public override void CheckPersistenceStructure(Type persistentObjectType)
        {
            throw new NotImplementedException();
        }
        public override T[] FindObjects<T>(IFilterExpression filterExpression, SortExpression sortExpression)
        {
            string uri = ApiUrl;
            if (filterExpression != null && sortExpression != null)
            {
                uri += "api/v1/getObjects/" + typeof(T).Name + "/filter/" + HttpUtility.UrlEncode(filterExpression.GetText()) + "/sort/" + HttpUtility.UrlEncode(sortExpression.GetText());
            }
            else if (filterExpression != null)
            {
                string jfilter = JsonConvert.SerializeObject(filterExpression);
                string jfilterEncoded = HttpUtility.UrlEncode(jfilter);
                uri += "api/v1/getObjects/" + typeof(T).Name + "/filter/" + HttpUtility.UrlEncode(filterExpression.GetText());

            }
            else if (sortExpression != null)
            {
                uri += "api/v1/getObjects/" + typeof(T).Name + "/sort/" + HttpUtility.UrlEncode(sortExpression.GetText());
            }
            else
            {
                uri += "api/v1/getObjects/" + typeof(T).Name;

            }
            T[] ret = GetObjectsFromUriAsync<T>(uri).Result;
            return ret;
        }
        public override bool Create(PersistentObject obj)
        {
            throw new NotImplementedException();
        }

        public override bool Delete(PersistentObject obj)
        {
            throw new NotImplementedException();
        }

        public override bool Refresh(StoredObject obj)
        {
            throw new NotImplementedException();
        }

        public override bool Update(PersistentObject obj)
        {
            throw new NotImplementedException();
        }
        async private Task<T[]> GetObjectsFromUriAsync<T>(string uri) where T : StoredObject
        {

            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(uri))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var JsonStringReturned = await response.Content.ReadAsStringAsync();
                        //Deserializacao por causa dos campos somente leitura
                        var ret = JsonConvert.DeserializeObject<T[]>(JsonStringReturned);
                        /*
                        var objects = JsonConvert.DeserializeObject<JObject[]>(JsonStringReturned);

                        var os = new Dictionary<int, Dictionary<string, object>>();

                        foreach (var j in objects)
                        {
                            var o = getObjectFromJson(j,typeof(T));
                            os.Add(Convert.ToInt32(o["Id"]), o);

                        }
                        var ret = CreateObjects<T>(os);
                        */
                        //         
                        return ret;
                    }
                    else
                    {
                        return null;
                    }
                }
            }

        }

        private Dictionary<string, object> getObjectFromJson(JObject j, Type type)
        {
            var ret = new Dictionary<string, object>();
            foreach (JProperty t in j.Children())
            {
                object o = null;
                /*
                if (t.Value is )
                {
                    o = CreateObject<T>(getObjectFromJson<T>((JObject)t.Value));
                }
                */
                if (t.Value is JObject)
                {
                    PropertyInfo prop = null;
                    foreach (var p in type.GetRuntimeProperties())
                    {
                        if (p.Name == t.Name && p.PropertyType.GetTypeInfo().IsPublic && !p.PropertyType.GetTypeInfo().IsAbstract)
                        {
                            prop = p;
                            break;
                        }
                    }


                    if (prop != null)
                    {
                        if (mi == null)
                        {
                            foreach (var item in this.GetType().GetRuntimeMethods())
                            {
                                if (item.Name.Contains("CreateObject"))
                                {
                                    mi = item;
                                    break;
                                }
                            }

                        }
                        var ps = typeof(Dictionary<string, object>);
                        var method = mi.MakeGenericMethod(prop.PropertyType);
                        var c = getObjectFromJson((JObject)t.Value, prop.PropertyType);
                        object[] param = { c };
                        o = (StoredObject)method.Invoke(this, param);
                    }
                }
                else if (t.Value is JValue)
                {
                    o = ((JValue)t.Value).Value;
                }
                if (o != null)
                {
                    ret.Add(t.Name, ((JValue)t.Value).Value);
                }
            }
            return ret;
        }
        MethodInfo mi = null;
    }
}
