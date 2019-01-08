using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace WebScraperHLTV
{

    public class PredictionThreadParser
    {
        public static void ParsePredicitionThread(Uri uri)
        {
            var web = new HtmlWeb();

            var htmlDoc = web.Load(uri);

            if (htmlDoc == null)
            {
                Console.WriteLine($"The {uri} could not be loaded");
            }
            Console.WriteLine($"Parsing {uri}");
            var nodes = GetPostElements(htmlDoc);


            var predictions = new List<Prediction>();

            foreach (var node in nodes)
            {
                var username = GetUsername(node);
                var player = GetSelection(node);
                var time = GetTime(node);

                predictions.Add(new Prediction(username, player, time));
            }

            var buffer = new StringBuilder();

            predictions.ForEach((e) => e.WriteToBuffer(buffer));

            var resultsPath = Path.Combine(Directory.GetCurrentDirectory(), $"votes-{DateTime.Now.ToString("yyyy-MM-dd_HH-mm")}.csv");

            File.WriteAllText(resultsPath, buffer.ToString());
            Console.WriteLine($"Done, result at {resultsPath}");

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
            return node.Descendants().Where(e => e.Name == "div" && e.GetAttributeValue("class", "").Equals("forum-middle")).FirstOrDefault().InnerText.Replace(',', '.').Replace('\n',' ').Replace("\r\n", " "); // replace , with . so it doesn't break the csv, ugly but works.
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
