namespace CatechismScrapper.Entities
{
    internal class CatechismDto
    {
        public IEnumerable<CatechismPointDto> Points { get; set; } = new List<CatechismPointDto>();
    }

    internal class CatechismPointDto
    {
        public int No { get; set; }
        public string? Content { get; set; }
        public IEnumerable<CatechismFootnoteDto> Footnotes { get; set; } = new List<CatechismFootnoteDto>();
    }

    internal class CatechismFootnoteDto
    {
        public string? Content { get; set; }
    }
}
