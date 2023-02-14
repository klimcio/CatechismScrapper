using System.Net;

namespace CatechismScrapper
{
    internal static class WebTools
    { 
        public static bool DoesWebsiteExist(string url)
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

        public static async Task<string> GetContentFromAsync(this string url)
        {
            string websiteContent;
            using (var client = new HttpClient())
            {
                websiteContent = await client.GetStringAsync(url);
            }

            return websiteContent;
        }

        public static async void SaveToFile(this string fileContent, string fileName)
        {
            await File.WriteAllTextAsync($@"C:\_Temp\catechism\{fileName}", fileContent);
        }
    }
}