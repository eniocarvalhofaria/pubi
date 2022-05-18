using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Collections;
namespace BusinessIntelligence.Persistence
{
    public abstract class PersistentObject : StoredObject
    {
        private static bool structureChecked;
        public PersistentObject()
        {
            if (!structureChecked)
            {
      //          Persistence.PersistenceSettings.PersistenceEngine.CheckPersistenceStructure(this.GetType());
            }
            structureChecked = true;
        }
        public bool IsFilled
        {
            get
            {
                return (Id != -1 && _LastUpdateDate != DateTime.MinValue);
            }
        }
        private PropertyInfo[] PropertiesNotSetted()
        {
            List<PropertyInfo> l = new List<PropertyInfo>();
            foreach (var prop in this.GetType().GetRuntimeProperties())
            {
                if (GetAttribute(prop, typeof(NotNullAttribute)) != null)
                {
                    if (prop.GetValue(this, null) == null)
                    {
                        l.Add(prop);
                    }
                }
            }
            return l.ToArray();
        }
        /*
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
        */
        public string ObjectTypeName
        {
            get { return this.GetType().Name; }
        }
        private DateTime _CreatedDate = DateTime.MinValue;
        [NotNull]
        [DateTime(true, "2010-01-01", "2050-12-31")]
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set
            {
                if (_CreatedDate == DateTime.MinValue)
                {

                    _CreatedDate = value;
                }
            }
        }
        private DateTime _LastUpdateDate = DateTime.MinValue;
        [NotNull]
        [DateTime(true, "2010-01-01", "2050-12-31")]
        public DateTime LastUpdateDate
        {
            get { return _LastUpdateDate; }

            set
            {
                if (_LastUpdateDate == DateTime.MinValue)
                {

                    _LastUpdateDate = value;
                }
            }
        }
        override public string ToString()
        {
            return Id.ToString();
        }



        public override void SetValue(string fieldName, object value)
        {
            if (fieldName.Equals("LastUpdateDate"))
            {
                this._LastUpdateDate = (DateTime)value;
            }
            else if (fieldName.Equals("CreatedDate"))
            {
                this._CreatedDate = (DateTime)value;
            }
            else
            {
                base.SetValue(fieldName, value);
            }
        }
        public virtual void Create()
        {
            PropertyInfo[] ps = PropertiesNotSetted();
            if (ps.Length > 0)
            {
                List<String> l = new List<string>();
                foreach (var p in ps)
                {
                    l.Add(p.Name);
                }
                throw new BusinessIntelligence.Persistence.FieldNullException(l.ToArray());
            }
            PersistenceSettings.PersistenceEngine.Create(this);
        }

        public virtual void Update()
        {
            PersistenceSettings.PersistenceEngine.Update(this);
        }
        public virtual void Delete()
        {
            PersistenceSettings.PersistenceEngine.Delete(this);
        }
        public Dictionary<string, object> GetValues()
        {
            Dictionary<string, object> FieldList = new Dictionary<string, object>();
            foreach (var prop in this.GetType().GetRuntimeProperties())
            {
                if (prop.CanRead && prop.GetValue(this, null) != null && !prop.Name.Equals("IsFilled") && !prop.Name.Equals("ObjectTypeName"))
                {
                    switch (prop.PropertyType.Name.Replace("[]", ""))
                    {
                        case "Int32":
                        case "Int16":
                        case "Int64":
                        case "Long":
                        case "Double":
                        case "Decimal":
                        case "Boolean":
                        case "DateTime":
                        case "String":
                            FieldList.Add(prop.Name, prop.GetValue(this, null));
                            break;
                        default:

                            Type type;


                            if (prop.PropertyType.GetTypeInfo().IsSubclassOf(typeof(StoredObject)) && !prop.PropertyType.GetTypeInfo().IsAbstract)
                            {
                                FieldList.Add(prop.Name, ((StoredObject)prop.GetValue(this, null)).Id);
                            }
                            else if (prop.PropertyType.GetTypeInfo().IsEnum)
                            {
                                FieldList.Add(prop.Name, Convert.ChangeType(prop.GetValue(this, null), Enum.GetUnderlyingType(prop.PropertyType)));
                            }
                            else if (prop.PropertyType.FullName.IndexOf("[]") > -1)
                            {
                                type = Type.GetType(prop.PropertyType.AssemblyQualifiedName.Replace("[]", ""));
                                if (type.GetTypeInfo().IsSubclassOf(typeof(StoredObject)) && !prop.PropertyType.GetTypeInfo().IsAbstract)
                                {
                                    List<int> l = new List<int>();
                                    foreach (StoredObject po in (StoredObject[])prop.GetValue(this, null))
                                    {
                                        l.Add(po.Id);
                                    }
                                    FieldList.Add(prop.Name, l.ToArray());
                                }
                            }
                            else if (prop.PropertyType.FullName.IndexOf("System.Collections.Generic.List") > -1)
                            {
                                type = Type.GetType(prop.PropertyType.AssemblyQualifiedName.Split('[')[2].Split(']')[0]);
                                List<object> l = new List<object>();
                                foreach (var po in (IList)prop.GetValue(this, null))
                                {
                                    l.Add(po.ToString());
                                }
                                FieldList.Add(prop.Name, l.ToArray());

                            }

                            break;
                    }

                }
            }
            return FieldList;
        }
    }
}
