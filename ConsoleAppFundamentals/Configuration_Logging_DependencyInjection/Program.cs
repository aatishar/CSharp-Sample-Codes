using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace Configuration_Logging_DependencyInjection
{
    public interface IMessageWriter
    {
        void Write(string message);
    }

    public class MessageWriter : IMessageWriter
    {
        public void Write(string message)
        {
            Console.WriteLine(message);
        }
    }

    public class HelloWorld
    {
        public IMessageWriter Writer { get; set; }
        public HelloWorld(IMessageWriter writer)
        {
            this.Writer = writer;
            SayHelloWorld();
        }

        private void SayHelloWorld()
        {
            Writer.Write("Hello World");
        }
    }
    class Program
    {
        private static ServiceProvider serviceProvider;

        static void Main(string[] args)
        {
            ConfigureService();
            serviceProvider.GetService<HelloWorld>();

            var logger = serviceProvider.GetRequiredService<ILogger>();
            logger.LogInformation("End");
        }

        public static void ConfigureService()
        {
            //Add nuget package Microsoft.Extension.Hosting
            //Dependency Injection
            IServiceCollection services = new ServiceCollection();

            services
                .AddSingleton<IMessageWriter, MessageWriter>()
                .AddSingleton<HelloWorld>()
                .AddLogging((loggingBuilder) => {
                    loggingBuilder.ClearProviders();
                    loggingBuilder.AddDebug();
                    loggingBuilder.AddConsole();
                });

            // Create a second logger with category name "App Logger"
            using var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .AddFilter("Microsoft", LogLevel.Warning)
                    .AddFilter("System", LogLevel.Warning)
                    .AddFilter("LoggingConsoleApp.Program", LogLevel.Debug)
                    .AddDebug()
                    .AddConsole();
            });

            ILogger appLogger = loggerFactory.CreateLogger("App Logger");
            //ILogger appLogger = loggerFactory.CreateLogger<Program>();

            services.AddSingleton(appLogger);

            //Create configuration
            //Need to change the property of appsetting.json so that it is copied to output directory 
            var configuration = new ConfigurationBuilder()
                                 .SetBasePath(Directory.GetCurrentDirectory())
                                 .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            var configurationRoot = configuration.Build();

            //Build services for dependency injection
            serviceProvider = services.BuildServiceProvider();

            //Create Logger of Category made of the full name of the class Program
            var logger = serviceProvider.GetRequiredService<ILogger<Program>>();


            logger.LogInformation("Dependency Injection, configuration and logging (Look for me in the Output for debug " +
                "as well on the console");
            Console.WriteLine("Key value pair of  data in appconfig file");
            foreach (var keyValue in configurationRoot.AsEnumerable())
            {
                Console.WriteLine($"{keyValue.Key} = {keyValue.Value}");
            }

            logger.LogInformation($"Accessing value of key \"Section_0: Key_1\" : {configurationRoot["Section_0:Key_1"]}");
        }
    }
}
