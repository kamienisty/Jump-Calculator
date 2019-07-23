using Autofac;
using AutofacContainerTypeRegistration;
using MainApplication;

namespace SW_StopNumberCalculator
{
    /// <summary>
    /// Standard consol application class
    /// </summary>
    class Program
    {
        /// <summary>
        /// Method for building Autofac container
        /// </summary>
        /// <returns>Build Autofac container</returns>
        private static IContainer CompositionRoot()
        {
            var builder = new ContainerBuilder();
            AutofacContainer.RegisterTypes(builder);
            return builder.Build();
        }

        /// <summary>
        /// Standard Main method 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            CompositionRoot().Resolve<CalculatorApplication>().Run();
        }
    }
}
