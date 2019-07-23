using Autofac;

using Abstractions.Services;
using Abstractions.Models;
using Services.Models;
using MainApplication;
using Services.Services;

namespace AutofacContainerTypeRegistration
{
    /// <summary>
    /// Class for registering types in Autofac
    /// </summary>
    public static class AutofacContainer
    {
        /// <summary>
        /// Method for registering types
        /// </summary>
        /// <param name="builder">Autofac builder instance</param>
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
