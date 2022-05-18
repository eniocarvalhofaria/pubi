using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessIntelligence.Persistence;
namespace BusinessIntelligence.Members.Financial
{
    public abstract class Group : PersistentObject
    {

        private string _Description;
        [NotNull]
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        private int _Level;
        [NotNull]
        public int Level
        {
            get { return _Level; }
            set { _Level = value; }
        }
        Group _GroupAggregator;
        public virtual Group GroupAggregator
        {
            get { return _GroupAggregator; }
            set { _GroupAggregator = value; }
        }
 
        public T[] GetChildren<T>() where T:Group
        {
           var _Children = new List<T>();
            if (_Children.Count == 0)
            {
                foreach (var ga in PersistenceSettings.PersistenceEngine.GetObjects<T>())
                {

                    if (ga.GroupAggregator != null && ga.GroupAggregator.Id == this.Id)
                    {
                        _Children.Add(ga);
                    }
                }
            }
            return _Children.ToArray();
        }
        public static T[] GetHierarchy<T>() where T : Group
        {
            var _Hierarchy = new List<T>();
            if (_Hierarchy.Count == 0)
            {
                foreach (var ga in PersistenceSettings.PersistenceEngine.GetObjects<T>())
                {
                    if (ga.Level == 1)
                    {
                        _Hierarchy.Add(ga);
                    }
                }
            }
           return _Hierarchy.ToArray();
        }


    }
}
