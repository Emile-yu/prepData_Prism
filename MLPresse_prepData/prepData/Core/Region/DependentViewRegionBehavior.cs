using Prism.Ioc;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prepData.Core.Region
{
    public class DependentViewRegionBehavior : RegionBehavior
    {
        public const string BehaviorKey = "DependentViewRegionBehavior";
        private readonly IContainerExtension _container;

        Dictionary<object, List<DependentViewInfo>> _dependentViewCache = new Dictionary<object, List<DependentViewInfo>>();

        protected override void OnAttach()
        {
            Region.ActiveViews.CollectionChanged += ActiveViews_CollectionChanged;
        }

        public DependentViewRegionBehavior(IContainerExtension container)
        {
            this._container = container;
        }

        private void ActiveViews_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var newView in e.NewItems)
                {
                    var dependentViews = new List<DependentViewInfo>();

                    //check if view already has dependents
                    if (_dependentViewCache.ContainsKey(newView))
                    {
                        dependentViews = _dependentViewCache[newView];
                    }
                    else
                    {
                        //get the attributes
                        var atts = GetCustomAttributes<DependentViewAttribute>(newView.GetType());

                        foreach (var att in atts)
                        {
                            var info = CreateDependentViewInfo(att);
                            dependentViews.Add(info);
                        }

                        _dependentViewCache.Add(newView, dependentViews);
                    }

                    dependentViews.ForEach(item => Region.RegionManager.Regions[item.Region].Add(item.View));
                    //inject
                }
                //for each attribute we need to create the view, find the region, then inject the view into the region 
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (var oldview in e.OldItems)
                {
                    if (_dependentViewCache.ContainsKey(oldview))
                    {
                        _dependentViewCache[oldview].ForEach(item => Region.RegionManager.Regions[item.Region].Remove(item.View));
                    }
                }
            }
        }

        DependentViewInfo CreateDependentViewInfo(DependentViewAttribute attribute)
        {

            var info = new DependentViewInfo();

            info.Region = attribute.Region;
            //create instand of the view
            info.View = _container.Resolve(attribute.Type);
            return info;
        }

        //T is a attribute
        private static IEnumerable<T> GetCustomAttributes<T>(Type type)
        {
            return type.GetCustomAttributes(typeof(T), true).OfType<T>();
        }
    }
    public class DependentViewInfo
    {
        public object View { get; set; }

        public string Region { get; set; }
    }
}
