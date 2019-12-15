using Data.Interfaces;
using Data.Services;
using Logic.Facades;
using Logic.Interfaces;
using Ninject;
using Ninject.Modules;
using Presentation.ViewModels;
using System;
using Tests;

namespace DependencyInjection
{
    public class Injector : NinjectModule
    {

        public override void Load()
        {
            Bind<IUserFcd>().To<UserFcd>();

            Bind<IBookSrv>().To<BookSrv>();
            Bind<ISupplierSrv>().To<SupplierSrv>();
            Bind<IEventSrv>().To<EventSrv>();
            Bind<IClientSrv>().To<ClientSrv>();

            Bind<IFiller>().To<StaticFiller>();

            Bind<MainViewModel>().ToSelf();
        }
    }
}
