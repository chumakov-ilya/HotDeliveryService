using System;
using Ninject;
using Ninject.Syntax;
using Ninject.Web.Common;

namespace Bringo.HotDeliveryService.Core
{
    public static class Root
    {
        static Root()
        {
            Register();
        }

        public static void Register()
        {
            Register(new StandardKernel());
        }

        public static void Register(StandardKernel kernel)
        {
            Kernel = kernel;

            var appSettings = new AppSettings();
            appSettings.Initialize();
            Kernel.Bind<IAppSettings>().ToConstant(appSettings).InSingletonScope();

            if (appSettings.StorageType == StorageType.Json)
                Bind<IRepository, JsonRepository>();
            if (appSettings.StorageType == StorageType.Sqlite)
                Bind<IRepository, SqliteRepository>();

            BindToSelf<DeliveryFactory>();
            BindToSelf<DeliveryService>();
        }

        internal static IBindingNamedWithOrOnSyntax<TFrom> BindToSelf<TFrom>()
        {
            return Bind<TFrom, TFrom>();
        }

        internal static IBindingNamedWithOrOnSyntax<TTo> Bind<TFrom, TTo>() where TTo : TFrom
        {
            return Kernel.Bind<TFrom>().To<TTo>().InRequestScope();
            //return Kernel.Bind<TFrom>().To<TTo>().InTransientScope();
        }

        public static IKernel Kernel { get; set; }

        public static T Resolve<T>()
        {
            return (T)Kernel.GetService(typeof(T));
        }

        public static object Resolve(Type type)
        {
            return Kernel.GetService(type);
        }
    }
}
