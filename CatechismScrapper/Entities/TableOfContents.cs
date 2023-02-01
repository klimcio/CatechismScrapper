namespace CatechismScrapper.Entities
{
    internal class TableOfContents
    {
        public string Source { get; set; } = default!;
        public IEnumerable<File> Files { get; set; } = Enumerable.Empty<File>();
    }

    internal class File
    {
        public string Chapter { get; set; } = default!;
        public string? Footnotes { get; set; }
    }
}
