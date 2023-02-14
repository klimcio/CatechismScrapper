using CatechismScrapper.Entities;
using HtmlAgilityPack;
using System.Xml;

namespace CatechismScrapper
{
    internal static class XmlHelperExtensions
    {
        public static IEnumerable<KeyValuePair<XmlDocument, XmlDocument>> ConvertSitesToXmlDocuments(this Dictionary<string, string> pairs)
        {
            var dict = new Dictionary<XmlDocument, XmlDocument>();
            foreach (var pair in pairs)
            {
                var point = GetDocumentFromAsync(pair.Key).Result;
                var footnote = string.IsNullOrEmpty(pair.Value)
                    ? new XmlDocument()
                    : GetDocumentFromAsync(pair.Value).Result;

                dict.Add(point, footnote);
            }

            return dict;
        }

        private static async Task<XmlDocument> GetDocumentFromAsync(string url)
        {
            string websiteContent;
            using (var client = new HttpClient())
            {
                websiteContent = await client.GetStringAsync(url);
            }

            var document = new HtmlDocument();
            document.LoadHtml(websiteContent);

            var xml = new XmlDocument();
            xml.LoadXml(document.DocumentNode.OuterHtml);

            return xml;
        }

        public static IEnumerable<CatechismPointDto> ConvertXmlDocumentsToObjects(this IEnumerable<KeyValuePair<XmlDocument, XmlDocument>> sites)
        {
            ;

            return new[] { new CatechismPointDto() };
        }
    }
}