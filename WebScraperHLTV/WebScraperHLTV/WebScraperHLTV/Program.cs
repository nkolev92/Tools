using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace WebScraperHLTV
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Count() != 1)
            {
                Console.WriteLine("Please only provide the link to be parsed.");
                return 1;
            }
            var html = args[0];

            var web = new HtmlWeb();

            Console.WriteLine($"Loading {html}");
            var htmlDoc = web.Load(html);

            if(htmlDoc == null)
            {

            }
            Console.WriteLine($"Parsing {html}");
            var nodes = GetPostElements(htmlDoc);

            var buffer = new StringBuilder();

            foreach (var node in nodes)
            {
                var username = GetUsername(node);
                var player = GetSelection(node);
                var time = GetTime(node);

                buffer.Append($"{username},{player},{time},\n");
            }           

            var resultsPath = Path.Combine(Directory.GetCurrentDirectory(), $"votes-{DateTime.Now.ToString("yyyy-MM-dd_HH-mm")}.csv");

            File.WriteAllText(resultsPath, buffer.ToString());
            Console.WriteLine($"Done, result at {resultsPath}");

            return 0;
        }

        private static readonly Regex _classNameRegex = new Regex(@"\bpost\b", RegexOptions.Compiled);

        private static IEnumerable<HtmlNode> GetPostElements(HtmlDocument doc)
        {
            return doc.DocumentNode.Descendants().Where(n => n.NodeType == HtmlNodeType.Element)
                .Where(e => e.Name == "div" && _classNameRegex.IsMatch(e.GetAttributeValue("class", "")));
        }

        private static string GetUsername(HtmlNode node)
        {
            return GetNode(node, "forum-topbar").Descendants().Where(e => e.Name == "a" && e.GetAttributeValue("class", "").Equals("authorAnchor")).FirstOrDefault().InnerText;
        }

        private static string GetSelection(HtmlNode node)
        {
            return node.Descendants().Where(e => e.Name == "div" && e.GetAttributeValue("class", "").Equals("forum-middle")).FirstOrDefault().InnerText;
        }

        private static string GetTime(HtmlNode node)
        {
            return GetNode(node, "time").InnerText;
        }

        private static HtmlNode GetNode(HtmlNode node, string clazz)
        {
            return node.Descendants().Where(e => e.Name == "div" && e.GetAttributeValue("class", "").Equals(clazz)).FirstOrDefault();
        }
    }
}
