using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScraperHLTV
{
    class Scoreboard
    {

        public static Scoreboard FromLines(string[] lines)
        {
            var Scoreboard = new Scoreboard();

            if (lines != null)
            {
                foreach (var line in lines)
                {
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        string[] results = line.Split(',');
                        if (results.Count() == 2)
                        {
                            try
                            {
                                var user = results[0].Trim();
                                var currentScore = results[1].Trim();

                                if (!string.IsNullOrEmpty(user) || !string.IsNullOrEmpty(currentScore))
                                {
                                    Scoreboard.Enter(user, int.Parse(currentScore));
                                }
                                else
                                {
                                    Console.WriteLine($"Skipping processing {line} from the scoreboard");
                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine($"Problem parsing, line={line}, {e.ToString()}");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Skipping processing {line} from the scoreboard");
                        }
                    }
                }
            }

            return Scoreboard;
        }

        public Dictionary<string, int> Table { get; } = new Dictionary<string, int>(StringComparer.InvariantCultureIgnoreCase);

        public void Enter(string name, int value)
        {
            Table[name] = value;
        }

        public void AddScore(string name)
        {
            if (Table.ContainsKey(name))
            {
                Table[name] += 1;
            }
            else
            {
                Table[name] = 1;
            }
        }

    }
}
