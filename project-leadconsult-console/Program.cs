using Autofac;
using project_leadconsult_core.BC;
using System.Diagnostics.CodeAnalysis;

namespace project_leadconsult
{
    /// <summary>
    /// Program
    /// </summary>
    [ExcludeFromCodeCoverage]
    internal static class Program
    {
        /// <summary>
        /// Compositions the root.
        /// </summary>
        /// <returns></returns>
        private static IContainer CompositionRoot()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Application>();
            builder.RegisterType<CoordinatesBC>().As<ICoordinatesBC>();
            return builder.Build();
        }

        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        public static void Main(string[] args)  //Main entry point
        {
            CompositionRoot().Resolve<Application>().Run(args);
        }
    }
}