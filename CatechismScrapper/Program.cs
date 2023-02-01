using CatechismScrapper.Entities;
using Microsoft.Extensions.Configuration;

namespace CatechismScrapper
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var configRoot = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var config = new TableOfContents();
            configRoot.Bind(config);

            Console.WriteLine("Hello, World!");
        }
    }
}