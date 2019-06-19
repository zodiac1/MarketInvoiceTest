using System;
using System.Collections.Generic;

namespace SlothEnterprise.ProductApplication.Mediator
{
    public interface IServicesMediator
    {
        IReadOnlyDictionary<Type, object> Services { get; }
        void Add(Type type, object serviceInstance);
    }

    public class ServicesMediator : IServicesMediator
    {
        private Dictionary<Type, object> _services = new Dictionary<Type, object>();

        public IReadOnlyDictionary<Type, object> Services { get { return _services; } }

        public void Add(Type type, object serviceInstance)
        {
            _services.Add(type, serviceInstance);
        }
    }
}