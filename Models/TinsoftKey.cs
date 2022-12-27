namespace POREG.Models
{
    public class TinsoftKey
    {
        private List<string> APIKeys { get; set; }
        private int KeyIteration { get; set; }
        public int KeyCount { get; set; }

        public TinsoftKey(string fileLocation)
        {
            APIKeys = new List<string>();
            KeyIteration = 0;
            ReadKey(fileLocation);
            KeyCount = APIKeys.Count;
        }

        private void ReadKey(string fileLocation)
        {
            using var reader = new StreamReader(fileLocation);
            while (reader.Peek() != -1)
            {
                var line = reader.ReadLine();
                if (line != null && !line.Trim().Equals(""))
                {
                    APIKeys.Add(line);
                }
            }
        }

        public string GetKey()
        {
            if (APIKeys == null)
            {
                return string.Empty;
            }
            if (KeyIteration == APIKeys.Count)
            {
                KeyIteration = 0;
            }
            if (APIKeys.Count == 0)
            {
                return string.Empty;
            }
            return APIKeys[KeyIteration++];
        }

        public string ShowKey()
        {
            if (APIKeys == null || APIKeys.Count == 0)
            {
                return "Empty Proxies List";
            }
            return string.Join("\n", APIKeys);
        }
    }
}
