using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting; //Install NuGet package Microsoft Extention Hosting

namespace ConsoleApp_GenericHost
{
    public interface IMessageWriter
    {
        void Write(string message);
    }

    public class MessageWriter: IMessageWriter
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
    public class Program
    {
        static void Main(string[] args)
        {
            using IHost host = CreateHostBuilder(args).Build();

            host.Services.GetRequiredService<HelloWorld>();

            host.Run();

        }

        static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostBuilder, services) =>
            services.AddSingleton<IMessageWriter, MessageWriter>()
                    .AddSingleton<HelloWorld>());

        }
    }
}
