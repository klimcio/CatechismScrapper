using CatechismScrapper.Entities;
using HtmlAgilityPack;
using System.Net;
using System.Xml;

namespace CatechismScrapper
{
    internal static class CatechismScrappingTools
    {
        public static Dictionary<string, string> ConvertToLinks(this TableOfContents toc)
        {
            var dict = new Dictionary<string, string>();

            if (!toc.Files.Any())
                return dict;

            foreach(var file in toc.Files)
            {
                var key = $"{toc.Source}{file.Chapter}";
                var value = string.IsNullOrWhiteSpace(file.Footnotes) ? "" : $"{toc.Source}{file.Footnotes}";

                if (!DoesWebsiteExist(key))
                    throw new Exception($"Website {key} does not exists");
                
                if (!string.IsNullOrEmpty(value) && !DoesWebsiteExist(value))
                    throw new Exception($"Website {value} does not exists");

                dict.Add(key, value);
            }

            return dict;
        }

        private static bool DoesWebsiteExist(string url)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "HEAD";

                using var response = (HttpWebResponse)request.GetResponse();
                return response.StatusCode == HttpStatusCode.OK;
            }
            catch
            {
                return false;
            }
        }

        public static IEnumerable<KeyValuePair<XmlDocument, XmlDocument>> ConvertSitesToXmlDocuments(this Dictionary<string, string> pairs)
        {
            var dict = new Dictionary<XmlDocument, XmlDocument>();
            foreach(var pair in pairs)
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

            HtmlDocument document = new HtmlDocument();
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