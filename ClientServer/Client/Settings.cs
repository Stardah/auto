using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Rpi
{
    public static class Settings
    {
        public static void FromFile(string filename)
        {
            var sr = new StreamReader(filename);
            string line = null;

            while ((line = sr.ReadLine()) != null)
            {
                line = line.Trim();

                if (line.Length > 0 && line[0] != '#')
                {
                    var tokens = line.Split('=');
                    var key = tokens[0];
                    var value = tokens[1];

                    if (!m_settings.ContainsKey(key))
                    {
                        m_settings.Add(key, value);
                    }
                    else
                    {
                        m_settings[key] = value;
                        Logger.WriteLine(
                            new object(), 
                            "Настройка `{0}` переопределено значением `{1}`",
                            key, 
                            value
                        );
                    }
                }
            }
        }

        private static Dictionary<string, string> m_settings = new Dictionary<string, string>();
    }
}
