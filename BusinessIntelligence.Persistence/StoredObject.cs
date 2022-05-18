using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Collections;
namespace BusinessIntelligence.Persistence
{
    public abstract class StoredObject
    {
        [AttributeUsage(AttributeTargets.Class)]
        protected sealed class PersistenceEngineAttributte : System.Attribute
        {
            public PersistenceEngineAttributte(string persistenceEngineName)
            {
                PersistenceEngineName = persistenceEngineName;
            }
            public string PersistenceEngineName { get; set; }
        }
        public StoredObject()
        {
        }
        private int _Id = -1;
        virtual public int Id
        {
            get { return _Id; }
            set {
                if (_Id == -1)
                {

                    _Id = value;
                }
            }
        }
        public static StoredObject[] Search(string text)
        {
            return new StoredObject[] { };

        }
        protected Attribute GetAttribute(PropertyInfo prop, Type attributeType)
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

        public string ObjectTypeName
        {
            get { return this.GetType().Name; }
        }
        override public string ToString()
        {
            return _Id.ToString();
        }
        public bool IsFilled { get; private set; }
        public virtual void SetValue(string fieldName, object value)
        {
            if (fieldName.Equals("Id"))
            {
                this._Id = Convert.ToInt32(value);
            }
            else
            {
                PropertyInfo prop = null;
                try
                {

                    foreach (var p in GetType().GetRuntimeProperties())
                    {
                        if (p.Name == fieldName && p.PropertyType.GetTypeInfo().IsPublic && !p.PropertyType.GetTypeInfo().IsAbstract)
                        {
                            prop = p;
                            break;
                        }
                    }
                    //     prop = GetType().GetRuntimeProperty(fieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
                    if (prop == null)
                    {
                        prop = GetType().GetRuntimeProperty(fieldName);
                    }


                }
                catch (Exception ex)
                {

                }
                if (prop.CanWrite)

                {
                    Type type = prop.PropertyType;
                    bool IsList = false;
                    if (type.FullName.IndexOf("System.Collections.Generic.List") > -1)
                    {
                        IsList = true;

                        type = Type.GetType(type.AssemblyQualifiedName.Split('[')[2].Split(']')[0]);
                    }
                    if (type.GetTypeInfo().IsSubclassOf(typeof(StoredObject)) && !type.GetTypeInfo().IsAbstract)
                    {
                        StoredObject newObjFK;
                        MethodInfo method;
                        Type pe = PersistenceSettings.PersistenceEngine.GetType();
                        if (IsList)
                        {
                            List<object> lo = (List<object>)value;
                            var po = (IList)prop.GetValue(this, null);
                            /*
                            Type genericListType = typeof(List<>).MakeGenericType(type);
                            var p = (IList)Activator.CreateInstance(genericListType);
                             */
                            foreach (var item in (List<object>)value)
                            {

                                //  if (prop.GetGetMethod().IsVirtual)
                                if (value.GetType().GetTypeInfo().IsSubclassOf(typeof(StoredObject)))
                                {
                                    po.Add(item);

                                }
                                else
                                {
                                    if (true)
                                    {
                                        var FKvalues = new Dictionary<string, object>();
                                        method = pe.GetRuntimeMethod("CreateReferenceObject", new Type[] { typeof(int) }).MakeGenericMethod(type);
                                    }
                                    else
                                    {
                                        method = pe.GetRuntimeMethod("GetObject", new Type[] { typeof(int) }).MakeGenericMethod(type);
                                    }
                                    object[] param = { item };
                                    newObjFK = (StoredObject)method.Invoke(PersistenceSettings.PersistenceEngine, param);
                                    po.Add(newObjFK);
                                }
                            }

                            prop.SetValue(this, po, null);
                        }
                        else
                        {
                            // if (prop.GetGetMethod().IsVirtual)
                            if (value.GetType().GetTypeInfo().IsSubclassOf(typeof(StoredObject)))
                            {
                                prop.SetValue(this, value, null);

                            }
                            else
                            {
                                if (true)
                                {

                                    var FKvalues = new Dictionary<string, object>();
                                    method = pe.GetRuntimeMethod("CreateReferenceObject", new Type[] { typeof(int) }).MakeGenericMethod(type);
                                }
                                else
                                {
                                    method = pe.GetRuntimeMethod("GetObject", new Type[] { typeof(int) }).MakeGenericMethod(type);
                                }
                                object[] param = { value };
                                newObjFK = (StoredObject)method.Invoke(PersistenceSettings.PersistenceEngine, param);
                                prop.SetValue(this, newObjFK, null);
                            }
                        }
                    }
                    else if (IsList && type.Name.Substring(0, 3) == "Int")
                    {
                        List<object> lo = (List<object>)value;
                        var po = (IList)prop.GetValue(this, null);

                        foreach (var item in (List<object>)value)
                        {
                            po.Add(item);
                        }

                    }
                    else if (type.GetTypeInfo().IsEnum)
                    {
                        prop.SetValue(this, Convert.ToInt32(value), null);
                    }
                    else
                    {
                        prop.SetValue(this, value, null);
                    }
                }
            }
        }
        public virtual void Fill(Dictionary<string, object> row)
        {
            foreach (KeyValuePair<string, object> item in row)
            {
                this.SetValue(item.Key, item.Value);

            }
            IsFilled = true;
        }
        public object GetValue(string fieldName)
        {
            return this.GetType().GetRuntimeProperty(fieldName).GetValue(this, null);
        }
        public void Refresh()
        {
            PersistenceSettings.PersistenceEngine.Refresh(this);
        }
    }
}
