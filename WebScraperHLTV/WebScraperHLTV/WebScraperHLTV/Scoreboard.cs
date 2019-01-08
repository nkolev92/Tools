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
            
            if (lines!= null)
            {
                foreach(var line in lines)
                {
                    if(string.IsNullOrWhiteSpace(line))
                    {
                        string[] results = line.Split(',');
                        Scoreboard.Enter(results[0], int.Parse(results[1]));
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
