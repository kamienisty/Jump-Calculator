using Autofac;

using Abstractions.Services;
using Abstractions.Models;
using Services.MainApplication;
using Services.Models;
using MainApplication;
using Services.Services;

namespace AutofacContainerTypeRegistration
{
    public static class AutofacContainer
    {
        public static void RegisterTypes(ContainerBuilder builder)
        {
            builder.RegisterType<CalculatorApplication>();
            builder.RegisterType<StarShipService>().As<IStarShipService>();
            builder.RegisterType<ShipDetailsModel>().As<IShipDetailsModel>();
            builder.RegisterType<APICallerService>().As<IAPICallerService>();
            builder.RegisterType<HoursSuppliesLastService>().As<IHoursSuppliesLastService>();
            builder.RegisterType<ConsolePrintService>().As<IConsolePrintService>();
            builder.RegisterType<InputValidationService>().As<IInputValidationService>();
        }
    }
}
