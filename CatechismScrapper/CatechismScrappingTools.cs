using CatechismScrapper.Entities;

namespace CatechismScrapper
{
    internal static class CatechismScrappingTools
    {
        public static Dictionary<string, string> ConvertToLinks(this TableOfContents toc)
        {
            var dict = new Dictionary<string, string>();

            if (!toc.Files.Any())
                return dict;

            foreach (var file in toc.Files)
            {
                var key = $"{toc.Source}{file.Chapter}";
                var value = string.IsNullOrWhiteSpace(file.Footnotes) ? "" : $"{toc.Source}{file.Footnotes}";

                if (!WebTools.DoesWebsiteExist(key))
                    throw new Exception($"Website {key} does not exists");

                if (!string.IsNullOrEmpty(value) && !WebTools.DoesWebsiteExist(value))
                    throw new Exception($"Website {value} does not exists");

                dict.Add(key, value);
            }

            return dict;
        }

        public static async Task SaveToFilesAsync(this TableOfContents toc)
        {
            foreach (var file in toc.Files)
            {
                var chapter = $"{toc.Source}{file.Chapter}";
                if (WebTools.DoesWebsiteExist(chapter))
                {
                    (await chapter.GetContentFromAsync())
                        .SaveToFile(file.Chapter);
                }

                var footnotes = string.IsNullOrWhiteSpace(file.Footnotes) ? "" : $"{toc.Source}{file.Footnotes}";
                if (!string.IsNullOrEmpty(footnotes) && 
                    WebTools.DoesWebsiteExist(footnotes))
                {
                    (await footnotes.GetContentFromAsync())
                        .SaveToFile(file.Footnotes);
                }
            }
        }
    }
}