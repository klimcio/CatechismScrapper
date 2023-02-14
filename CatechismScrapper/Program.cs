using CatechismScrapper.Entities;
using Microsoft.Extensions.Configuration;

namespace CatechismScrapper
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            var toc = GetTableOfContents();

            await toc.SaveToFilesAsync();
        }

        private static TableOfContents GetTableOfContents()
        {
            var configRoot = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var config = new TableOfContents();
            configRoot.Bind(config);
            return config;
        }
    }
}