using System;
using System.Collections.Generic;
using System.Text;

namespace AttendanceApp.Helpers
{
    public static class ServiceContainer
    {
        static readonly Dictionary<Type, Lazy<object>> Services = new Dictionary<Type, Lazy<object>>();
        static readonly Stack<Dictionary<Type, object>> ScopedServices = new Stack<Dictionary<Type, object>>();
        public static void Register<T>(T service)
        {
            Services[typeof(T)] = new Lazy<object>(() => service);
        }
        public static void Register<T>() where T : new()
        {
            Services[typeof(T)] = new Lazy<object>(() => new T());
        }
        public static void Register<T>(Func<T> function)
        {
            Services[typeof(T)] = new Lazy<object>(() => function());
        }
        public static void Register(Type type, object service)
        {
            Services[type] = new Lazy<object>(() => service);
        }
        public static void Register(Type type, Func<object> function)
        {
            Services[type] = new Lazy<object>(function);
        }
        public static void RegisterScoped<T>(T service)
        {
            Dictionary<Type, object> services;
            if (ScopedServices.Count == 0)
            {
                services = new Dictionary<Type, object>();
                ScopedServices.Push(services);
            }
            else
            {
                services = ScopedServices.Peek();
            }

            services[typeof(T)] = service;
        }
        public static T Resolve<T>()
        {
            return (T)Resolve(typeof(T));
        }
        public static object Resolve(Type type)
        {
            if (ScopedServices.Count > 0)
            {
                var services = ScopedServices.Peek();

                object service;
                if (services.TryGetValue(type, out service))
                {
                    return service;
                }
            }

            //Non-scoped services
            {
                Lazy<object> service;
                if (Services.TryGetValue(type, out service))
                {
                    return service.Value;
                }
                else
                {
                    throw new KeyNotFoundException(string.Format("Service not found for type '{0}'", type));
                }
            }
        }
        public static void AddScope()
        {
            ScopedServices.Push(new Dictionary<Type, object>());
        }
        public static void RemoveScope()
        {
            if (ScopedServices.Count > 0)
                ScopedServices.Pop();
        }
        public static void Clear()
        {
            Services.Clear();
            ScopedServices.Clear();
        }
    }
}
