using System.Text;

namespace WebScraperHLTV
{
    public class Prediction
    {
        public string Username { get; set; }
        public string Selection { get; set; }
        public string Time { get; set; }

        public Prediction(string username, string selection, string time)
        {
            Username = username;
            Selection = selection;
            Time = time;
        }

        public void WriteToBuffer(StringBuilder sb)
        {
            sb.Append($"{Username},{Selection},{Time}\n");
        }

        public static Prediction Generate(string value)
        {
            if (value != null)
            {
                var list = value.Trim().Split(',');

                if (list.Length != 3)
                {
                    return null;
                }

                return new Prediction(list[0].Trim(), list[1].Trim(), list[2].Trim());
            }
            return null;
        }
    }
}
