using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Reflection;
//using Microsoft.Practices.Unity;
//using Microsoft.Practices.Unity.InterceptionExtension;
namespace BusinessIntelligence.Persistence
{
    public abstract class PersistenceEngine
    {
        public PersistenceEngine()
        {
            PersistenceSettings.PersistenceEngine = this;
        }
        abstract public void CheckPersistenceStructure(Type persistentObjectType);

        abstract public bool Create(PersistentObject obj);

        abstract public bool Update(PersistentObject obj);

        abstract public bool Delete(PersistentObject obj);

        abstract public bool Refresh(StoredObject obj);

        //     abstract public bool Load(Type objectType, out Dictionary<int, Dictionary<string, object>> objects, IFilterExpression filterExpression, SortExpression sortExpression);
        abstract public T[] FindObjects<T>(IFilterExpression filterExpression, SortExpression sortExpression) where T : StoredObject;


        public T GetObject<T>(int id) where T : StoredObject
        {

            StoredObject newObj;
            if (ObjectsInMemory.ContainsKey(typeof(T)) && ObjectsInMemory[typeof(T)].TryGetValue(id, out newObj))
            {
                return (T)newObj;
            }
            else
            {
                if (DomainsFullInMemory.ContainsKey(typeof(T).Name))
                {
                    return null;

                }
                else
                {
                    var r = PersistenceSettings.PersistenceEngine.FindObjects<T>(FilterExpressions.Equal("Id", id.ToString()), null);
                    if (r != null && r.Count() > 0)
                    {
                        return r[0];
                    }
                    return null;
                }

            }

        }

        public T[] GetObjects<T>() where T : StoredObject
        {
            return GetObjects<T>(null, null);
        }
        public T[] GetObjects<T>(SortExpression sortExpression) where T : StoredObject
        {
            return GetObjects<T>(null, sortExpression);
        }
        public T[] GetObjects<T>(IFilterExpression filterExpression) where T : StoredObject
        {
            return GetObjects<T>(filterExpression, null);
        }
        private Dictionary<string, IList> domainsFullInMemory = new Dictionary<string, IList>();
        public T[] GetObjects<T>(IFilterExpression filterExpression, SortExpression sortExpression) where T : StoredObject
        {

            if (!DomainsFullInMemory.ContainsKey(typeof(T).Name))
            {
                var l = new List<T>();
                var r = FindObjects<T>(filterExpression, sortExpression);
                if (r == null)
                {
                    return l.ToArray();
                }
                l.AddRange(r);



                if (!ObjectsInMemory.ContainsKey(typeof(T)))
                {
                    ObjectsInMemory.Add(typeof(T), new Dictionary<int, StoredObject>());
                }

                if (filterExpression == null)
                {
                    DomainsFullInMemory.Add(typeof(T).Name, l);
                }


                return l.ToArray();
            }
            else
            {
                return ((List<T>)DomainsFullInMemory[typeof(T).Name]).ToArray();
            }
        }
        public T[] CreateObjects<T>(Dictionary<int, Dictionary<string, object>> objectList) where T : StoredObject
        {
            List<T> ret = new List<T>();
            if (!ObjectsInMemory.ContainsKey(typeof(T)))
            {
                ObjectsInMemory.Add(typeof(T), new Dictionary<int, StoredObject>());
            }
            foreach (KeyValuePair<int, Dictionary<string, object>> item in objectList)
            {

                if (!ObjectsInMemory[typeof(T)].ContainsKey(item.Key))
                {
                    T newObj = CreateObject<T>(item.Value);
                    if (!ret.Contains(newObj))
                    {
                        ret.Add(newObj);
                        //     list[typeof(T)].Add(newObj.Id, newObj);
                    }
                }
                else
                {
                    var obj = ObjectsInMemory[typeof(T)][item.Key];
                    obj.Fill(item.Value);
                    ret.Add((T)obj);
                }
            }
            return ret.ToArray();
        }
        //     Dictionary<Type, IUnityContainer> containers = new Dictionary<Type, IUnityContainer>();
        public T CreateReferenceObject<T>(int id) where T : StoredObject
        {
            if (!ObjectsInMemory.ContainsKey(typeof(T)))
            {
                ObjectsInMemory.Add(typeof(T), new Dictionary<int, StoredObject>());
            }
            if (ObjectsInMemory[typeof(T)].ContainsKey(id))
            {
                return (T)ObjectsInMemory[typeof(T)][id];
            }
            else
            {
                T newObj = GetNewEmptyObject<T>();
                var d = new Dictionary<string, object>();
                d.Add("Id", id);
                Add(id, newObj);
                newObj.Fill(d);
                return newObj;
            }
            /*
                StoredObject newObj;
            if (!list[typeof(T)].TryGetValue(id, out newObj))
            {
                if (!containers.ContainsKey(typeof(T)))
                {
                    var container = new UnityContainer();
                    container.AddNewExtension<Interception>();
                    container.RegisterType<T>(new Interceptor<VirtualMethodInterceptor>(), new InterceptionBehavior<PersistentObjectInterception>());
                    containers.Add(typeof(T), container);
                }
                newObj = (T)containers[typeof(T)].Resolve<T>();
                var d = new Dictionary<string, object>();
                d.Add("Id", id);
                newObj.Fill(d);
            }*/

        }

        protected T CreateObject<T>(Dictionary<string, object> row) where T : StoredObject
        {
            T newObj = GetNewEmptyObject<T>();
            Add(Convert.ToInt32(row["Id"]), newObj);
            newObj.Fill(row);
            return newObj;
        }
        public T GetNewEmptyObject<T>() where T : StoredObject
        {
            return (T)Activator.CreateInstance(typeof(T));
        }
        protected void Add(int id, StoredObject item)
        {
            if (!ObjectsInMemory.ContainsKey(item.GetType()))
            {
                ObjectsInMemory.Add(item.GetType(), new Dictionary<int, StoredObject>());
            }
            if (!ObjectsInMemory[item.GetType()].ContainsKey(id))
            {
                ObjectsInMemory[item.GetType()].Add(id, item);
            }
            if (DomainsFullInMemory.ContainsKey(item.GetType().Name))
            {

                DomainsFullInMemory[item.GetType().Name].Add(item);
            }


        }
        private Dictionary<Type, Dictionary<int, StoredObject>> objectsInMemory = new Dictionary<Type, Dictionary<int, StoredObject>>();

        Dictionary<Type, Dictionary<string, PersistentProperty>> pps = new Dictionary<Type, Dictionary<string, PersistentProperty>>();

        protected Dictionary<string, IList> DomainsFullInMemory
        {
            get
            {
                return domainsFullInMemory;
            }

            set
            {
                domainsFullInMemory = value;
            }
        }

        protected Dictionary<Type, Dictionary<int, StoredObject>> ObjectsInMemory
        {
            get
            {
                return objectsInMemory;
            }

            set
            {
                objectsInMemory = value;
            }
        }

        public Dictionary<string, PersistentProperty> GetPropertiesAttributtes(Type persistentObjectType)
        {
            if (pps.ContainsKey(persistentObjectType))
            {
                return pps[persistentObjectType];
            }
            Dictionary<string, PersistentProperty> FieldList = new Dictionary<string, PersistentProperty>();
            foreach (var prop in persistentObjectType.GetRuntimeProperties())
            {

                if (prop.CanRead && !prop.Name.Equals("IsFilled") && !prop.Name.Equals("ObjectTypeName"))
                {
                    PersistentProperty pp = new PersistentProperty();

                    pp.PropertyInfo = prop;
                    Type type = prop.PropertyType;



                    if (type.FullName.IndexOf("System.Collections.Generic.List") > -1)
                    {
                        pp.IsList = true;
                        type = Type.GetType(type.AssemblyQualifiedName.Split('[')[2].Split(']')[0]);
                    }
                    else if (type.FullName.IndexOf("[]") > -1)
                    {
                        pp.IsArray = true;
                        type = Type.GetType(type.AssemblyQualifiedName.Replace("[]", ""));
                    }
                    if (type.GetTypeInfo().IsSubclassOf(typeof(StoredObject)) && !type.GetTypeInfo().IsAbstract)
                    {
                        var na = new NumericAttribute(1, Int32.MaxValue);
                        pp.DataTypeAttribute = na;
                    }
                    else if (prop.PropertyType.GetTypeInfo().IsEnum)
                    {
                        var na = new NumericAttribute(1, Int16.MaxValue);
                        pp.DataTypeAttribute = na;
                    }
                    else
                    {
                        switch (type.Name)
                        {
                            case "Int32":
                            case "Int16":
                            case "Int64":
                            case "Long":
                                {
                                    NumericAttribute na;
                                    na = (NumericAttribute)GetAttribute(prop, typeof(NumericAttribute));
                                    if (na == null)
                                    {
                                        switch (type.Name)
                                        {
                                            case "Int32":
                                                {
                                                    na = new NumericAttribute(Int32.MinValue, Int32.MaxValue);
                                                    break;
                                                }
                                            case "Int16":
                                                {
                                                    na = new NumericAttribute(Int16.MinValue, Int16.MaxValue);
                                                    break;
                                                }
                                            case "Int64":
                                            case "Long":
                                                {
                                                    na = new NumericAttribute(Int64.MinValue, Int64.MaxValue);
                                                    break;
                                                }
                                        }

                                    }
                                    pp.DataTypeAttribute = na;
                                    break;
                                }
                            case "Double":
                            case "Decimal":
                                DecimalAttribute da;
                                da = (DecimalAttribute)GetAttribute(prop, typeof(DecimalAttribute));
                                if (da == null)
                                {
                                    da = new DecimalAttribute();
                                    da.DecimalPlaces = 2;

                                    if (type.Name.Equals("Double"))
                                    {
                                        da.TotalDigits = 18;
                                    }
                                    else
                                    {
                                        da.TotalDigits = 38;
                                    }
                                }
                                pp.DataTypeAttribute = da;
                                break;
                            case "Boolean":
                                pp.DataTypeAttribute = new BooleanAttribute();
                                break;
                            case "DateTime":
                                DateTimeAttribute ta = null;

                                ta = (DateTimeAttribute)GetAttribute(prop, typeof(DateTimeAttribute));
                                if (ta == null)
                                {
                                    ta = new DateTimeAttribute(true);

                                }
                                pp.DataTypeAttribute = ta;
                                break;
                            case "String":
                                {
                                    CharAttribute ca = null;
                                    ca = (CharAttribute)GetAttribute(prop, typeof(CharAttribute));
                                    if (ca == null)
                                    {
                                        ca = new CharAttribute(false, 255);
                                    }
                                    pp.DataTypeAttribute = ca;
                                    break;
                                }


                        }
                    }
                    if (pp.DataTypeAttribute != null)
                    {
                        pp.NotNull = GetAttribute(prop, typeof(NotNullAttribute)) != null;
                        pp.IsUnique = GetAttribute(prop, typeof(IsUniqueAttribute)) != null;
                        pp.IsIndexed = GetAttribute(prop, typeof(IndexedAttribute)) != null;
                        FieldList.Add(pp.PropertyInfo.Name, pp);
                    }
                }
            }
            pps.Add(persistentObjectType, FieldList);
            return FieldList;
        }
        private Attribute GetAttribute(PropertyInfo prop, Type attributeType)
        {
            foreach (object att in prop.GetCustomAttributes(true))
            {
                if (att.GetType().Name.Equals(attributeType.Name))
                {
                    return (Attribute)att;

                }
            }
            return null;
        }
    }
}
