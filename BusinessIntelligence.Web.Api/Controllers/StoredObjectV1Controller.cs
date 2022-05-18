using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessIntelligence.Persistence;
using System.Reflection;
using BusinessIntelligence.Members.Marketing;
using BusinessIntelligence.Members.Financial;
using BusinessIntelligence.Members.Security;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BusinessIntelligence.Web.Api.Controllers
{

    public class StoredObjectV1Controller : ApiController
    {
        static Dictionary<string, Type> storedObjectsTypes = new Dictionary<string, Type>();
        public StoredObjectV1Controller()
        {
            if (storedObjectsTypes.Count == 0)
            {
                var assemblies = AppDomain.CurrentDomain.GetAssemblies();
                foreach (Assembly ass in assemblies)
                {
                    if (ass.FullName.Contains("Members"))
                    {
                        foreach (Type type in ass.GetTypes())
                        {
                            if (type.IsSubclassOf(typeof(StoredObject)) && !type.IsAbstract)
                            {
                                storedObjectsTypes.Add(type.FullName, type);
                                storedObjectsTypes.Add(type.FullName.Replace("BusinessIntelligence.Members.", "").Replace(".", ""), type);
                                if (!storedObjectsTypes.ContainsKey(type.Name))
                                {
                                    storedObjectsTypes.Add(type.Name, type);
                                }
                            }
                        }
                    }
                }
            }
            if (GetObjectsMethod == null)
            {
                Type p = PersistenceSettings.PersistenceEngine.GetType();

                var ms = p.GetMethods();
                foreach (var m in ms)
                {
                    if (m.Name == "GetObjects" && m.GetParameters().Count() == 2)
                    {
                        GetObjectsMethod = m;
                        break;
                    }
                }
            }

        }

        static BusinessIntelligence.Persistence.PersistenceEngine pe = new BusinessIntelligence.Persistence.SqlServerPersistenceEngine((System.Data.SqlClient.SqlConnection)BusinessIntelligence.Data.Connections.GetNewConnection("APPPROD"), "appprod");
        [HttpGet]
        [Route("api/v1/getObjects/{objectTypeName}")]
        public IEnumerable<StoredObject> GetObjects(string objectTypeName)
        {
            return GetObjects(objectTypeName, null, null);

        }
        [HttpGet]
        [Route("api/v1/getObjects/{objectTypeName}/filter/{filterExpression}")]
        public IEnumerable<StoredObject> GetObjectsByFilter(string objectTypeName, string filterExpression = null)
        //   public IEnumerable<StoredObject> GetObjects(string objectTypeName)
        {
            return GetObjects(objectTypeName, filterExpression, null);
        }
        [HttpGet]
        [Route("api/v1/getObjects/{objectTypeName}/sort/{sortExpression}")]
        public IEnumerable<StoredObject> GetObjectsSorted(string objectTypeName, string sortExpression = null)
        //   public IEnumerable<StoredObject> GetObjects(string objectTypeName)
        {
            return GetObjects(objectTypeName, null, sortExpression);
        }
        [HttpGet]
        [Route("api/v1/getObjects/{objectTypeName}/filter/{filterExpression}/sort/{sortExpression}")]
        public IEnumerable<StoredObject> GetObjects(string objectTypeName, string filterExpression = null, string sortExpression = null)
        //   public IEnumerable<StoredObject> GetObjects(string objectTypeName)
        {

            var t = GetType(objectTypeName);
            if (t != null)
            {

                var method = GetObjectsMethod.MakeGenericMethod(t);

                IFilterExpression f = null;
                if (!string.IsNullOrEmpty(filterExpression))
                {

                    string jfilter = HttpUtility.UrlDecode(filterExpression);




                    var dic = new Dictionary<string, object>();
                    var objects = JsonConvert.DeserializeObject<JObject[]>(jfilter);
                    if (objects.Length > 0)
                    {
                        foreach (JProperty item in objects[0].Children()) 
                        {
                            var name = t.Name;
                            var o = ((JValue)item.Value).Value;
                            dic.Add(name, o);
                        }
                    }

                    f = FilterExpressions.GetFilterFromProperties(dic);

                }

                SortExpression s = null;
                if (!string.IsNullOrEmpty(sortExpression))
                {
                    s = SortExpression.GetInstanceFromText(HttpUtility.UrlDecode(sortExpression));
                }


                object[] param = { f, s };
                //       object[] param = { null, null };
                var ret = (StoredObject[])method.Invoke(PersistenceSettings.PersistenceEngine, param);
                return ret;
            }
            else
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }
        private Type GetType(string typeName)
        {
            if (storedObjectsTypes.ContainsKey(typeName))
            {
                return storedObjectsTypes[typeName];
            }
            else
            {
                return null;
            }
        }
        static MethodInfo GetObjectsMethod = null;
        [HttpGet]
        [Route("api/v1/getObject/{objectTypeName}/{id}")]
        public StoredObject GetObject(string objectTypeName, int id)
        {
            var t = GetType(objectTypeName);
            if (t != null)
            {
                Type p = PersistenceSettings.PersistenceEngine.GetType();
                MethodInfo method;
                method = p.GetMethod("GetObject").MakeGenericMethod(t);
                object[] param = { id };
                var ret = (StoredObject)method.Invoke(PersistenceSettings.PersistenceEngine, param);
                return ret;
            }
            else
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }
        [HttpPost]
        public HttpResponseMessage PostObject(PersistentObject item)
        {
            PutObject(item);
            var response = Request.CreateResponse<PersistentObject>(HttpStatusCode.Created, item);
            string uri = Url.Link("DefaultApi", new { id = item.Id });
            response.Headers.Location = new Uri(uri);
            return response;
        }
        [HttpPut]
        public void PutObject(StoredObject item)
        {
            if (item.GetType().IsSubclassOf(typeof(PersistentObject)) && !item.GetType().IsAbstract)
            {

                var a = (PersistentObject)item;
                if (item.Id > 0)
                {
                    a.Update();
                }
                else
                {
                    a.Create();
                }
            }
        }
        [HttpGet]
        [Route("api/v1/delete")]
        public void DeleteObject(string objectTypeName, int id)
        {
            var toDelete = GetObject(objectTypeName, id);
            if (toDelete != null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            else if (toDelete.GetType().IsSubclassOf(typeof(PersistentObject)) && !toDelete.GetType().IsAbstract)
            {
                ((PersistentObject)toDelete).Delete();
            }
            else
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }
        }
        [HttpGet]
        [Route("api/v1/refresh")]
        public void RefreshObject(string objectTypeName, int id)
        {
            var toRefresh = GetObject(objectTypeName, id);
            if (toRefresh != null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            else
            {
                toRefresh.Refresh();
            }
        }
    }
}
