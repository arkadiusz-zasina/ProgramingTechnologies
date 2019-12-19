using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Ninject;
using System.Reflection;
using Data.Interfaces;
using Data.Services;
using Logic.Interfaces;
using Logic.Facades;
using Presentation.ViewModels;
using Data;

namespace Presentation
{
    /// <summary>
    /// Logika interakcji dla klasy App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void AppStartup(object sender, StartupEventArgs e)
        {
            IKernel kernel = new StandardKernel();
            kernel.Bind<IUserFcd>().To<UserFcd>();

            kernel.Bind<IBookSrv>().To<BookSrv>();
            kernel.Bind<ISupplierSrv>().To<SupplierSrv>();
            kernel.Bind<IClientSrv>().To<ClientSrv>();
            kernel.Bind<IEventSrv>().To<EventSrv>();

            kernel.Bind<IEditBookWindow>().To<EditBookWindow>();
            kernel.Bind<IEditClientWindow>().To<EditClientWindow>();
            kernel.Bind<IAddClientWindow>().To<AddClientWindow>();
            kernel.Bind<IEventLogsWindow>().To<EventLogsWindow>();
            kernel.Bind<IAddBookWindow>().To<AddBookWindow>();

            //kernel.Bind<IFiller>().To<StaticFiller>();

            kernel.Bind<IDBContextDataContext>().To<DBContextDataContext>();

            var mainViewModel = kernel.Get<MainViewModel>();

            MainWindow = new MainWindow();
            MainWindow.DataContext = mainViewModel;
            MainWindow.Show();


        }
    }
}
