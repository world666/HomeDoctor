using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using HomeDoctor.Providers;
using Ninject;
using Ninject.Parameters;

namespace HomeDoctor.Models
{
    public static class IoC
    {
        private static readonly IKernel _kernel = new StandardKernel();

        static IoC()
        {
            _kernel.Bind<RoleProvider>().To<CustomRoleProvider>();
        }

        public static void RegisterType<TInterface, TClass>() where TClass : TInterface
        {
            _kernel.Bind<TInterface>().To<TClass>();
        }

        public static void RegisterSingleton<TInterface, TClass>() where TClass : TInterface
        {
            _kernel.Bind<TInterface>().To<TClass>().InSingletonScope();
        }

        public static void RegisterInstance<TInterface, TClass> () where TClass : TInterface
        {
            _kernel.Bind<TInterface>().To<TClass>();
        }

        public static T Resolve<T>(params IParameter[] parameters) {
            return _kernel.Get<T>(parameters);
        }
    } 
}