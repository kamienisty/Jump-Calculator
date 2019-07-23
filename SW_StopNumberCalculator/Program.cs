using System;

using Autofac;
using AutofacContainerTypeRegistration;
using MainApplication;

namespace SW_StopNumberCalculator
{
    class Program
    {
        private static IContainer CompositionRoot()
        {
            var builder = new ContainerBuilder();
            AutofacContainer.RegisterTypes(builder);
            return builder.Build();
        }
        static void Main(string[] args)
        {
            CompositionRoot().Resolve<CalculatorApplication>().Run();
        }
    }
}
