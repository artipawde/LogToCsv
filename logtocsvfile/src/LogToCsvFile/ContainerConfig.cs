using System;
using Autofac;

namespace LogToCsvFile
{
    public static class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<LoggerConverter>().As<ILoggerConverter>();
            builder.RegisterType<CommandArgumentConverter>().As<ICommandArgumentConverter>();
            builder.RegisterType<UserInput>().As<IUserInput>();
            builder.RegisterType<Application>().As<IApplication>();
            builder.RegisterType<CsvFile>().As<ICsvFile>();

            return builder.Build();
        }
    }
}