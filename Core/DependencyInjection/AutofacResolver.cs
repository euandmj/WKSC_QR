using Autofac;
using System;

namespace Core.DependencyInjection
{
    public static class AutofacResolver
    {
        private static IContainer _container;


        public static T ResolveType<T>(params Autofac.Core.Parameter[] paramaters)
        {
            if (_container is null) throw new InvalidOperationException("null container");

            return _container.Resolve<T>(paramaters);
        }

        public static void Initialise(IContainer container)
        {
            if(container is null) throw new ArgumentNullException(nameof(container));

            _container = container;
        }
    }
}
