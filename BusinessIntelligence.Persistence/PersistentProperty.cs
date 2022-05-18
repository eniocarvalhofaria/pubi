using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
namespace BusinessIntelligence.Persistence
{
    public class PersistentProperty
    {
        PropertyInfo _PropertyInfo;
        public PropertyInfo PropertyInfo
        {
            get { return _PropertyInfo; }
            set { _PropertyInfo = value; }
        }

        bool _NotNull;

        public bool NotNull
        {
            get { return _NotNull; }
            set { _NotNull = value; }
        }
        bool _IsUnique;

        public bool IsUnique
        {
            get { return _IsUnique; }
            set { _IsUnique = value; }
        }
        bool _IsIndexed;
        public bool IsIndexed
        {
            get { return _IsIndexed; }
            set { _IsIndexed = value; }
        }
        Attribute _DataTypeAttribute;

        public Attribute DataTypeAttribute
        {
            get { return _DataTypeAttribute; }
            set { _DataTypeAttribute = value; }
        }
        bool _IsArray;
        public bool IsArray
        {
            get { return _IsArray; }
            set { _IsArray = value; }
        }
        bool _IsList;

        public bool IsList
        {
            get { return _IsList; }
            set { _IsList = value; }
        }
    }
}
