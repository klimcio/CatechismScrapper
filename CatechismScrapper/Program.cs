using CatechismScrapper.Entities;
using Microsoft.Extensions.Configuration;

namespace CatechismScrapper
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            GetTableOfContents()
                .ConvertToLinks()
                .ConvertSitesToXmlDocuments()
                .ConvertXmlDocumentsToObjects();
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