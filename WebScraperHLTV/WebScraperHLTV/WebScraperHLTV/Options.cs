using CommandLine;
using System;

namespace WebScraperHLTV
{
    public class Options
    {
        [Option("URL", Required = false, HelpText = "The url with the prediction thread.")]
        public Uri URL { get; set; }

        [Option("Player", Required = false, HelpText = "The correct prediction. The comparison is case insensitive")]
        public string Player { get; set; }

        [Option("Score", Required = false, HelpText = "The current scoreboard file. A csv file with username,currentscore.")]
        public string Score { get; set; }

        [Option("Predictions", Required = false, HelpText = "The current predictions. A csv file with username, time, current score")]
        public string Predictions { get; set; }

        [Option("CutoffTime", Required = false, HelpText = "The last acceptable time. It has to be in the same format as the predictions file. If not provided, all entries in the file list will be considered.")]
        public string CutoffTime { get; set; }
    }
}
