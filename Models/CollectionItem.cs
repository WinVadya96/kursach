using Markdig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kursach.Models
{
    public class CollectionItem
    {
        public int Id { get; set; }

        public int CollectionId { get; set; }
        public Collection CollectionOfItem { get; set; }

        public string String1Value { get; set; }
        public string String2Value { get; set; }
        public string String3Value { get; set; }
        public double Number1Value { get; set; }
        public double Number2Value { get; set; }
        public double Number3Value { get; set; }
        public DateTime? Date1Value { get; set; }
        public DateTime? Date2Value { get; set; }
        public DateTime? Date3Value { get; set; }
        public string Markdown1Value { get; set; }
        public string Markdown2Value { get; set; }
        public string Markdown3Value { get; set; }
        public bool Checkbox1Value { get; set; }
        public bool Checkbox2Value { get; set; }
        public bool Checkbox3Value { get; set; }

        //public static class Markdown
        //{
        //    public static string Parse(string markdown,
        //                    bool usePragmaLines = false,
        //                    bool forceReload = false)
        //    {
        //        if (string.IsNullOrEmpty(markdown))
        //            return "";

        //        var parser = MarkdownParserFactory.GetParser(usePragmaLines, forceReload);
        //        return parser.Parse(markdown);
        //    }

        //    public static HtmlString ParseHtmlString(string markdown,
        //                    bool usePragmaLines = false,
        //                    bool forceReload = false)
        //    {
        //        return new HtmlString(Parse(markdown, usePragmaLines, forceReload));
        //    }
        //}
    }
}