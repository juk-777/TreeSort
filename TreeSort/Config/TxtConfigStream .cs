using System;
using System.IO;
using System.Text;

namespace TreeSort.Config
{
    public class TxtConfigStream : IConfigStream
    {
        private readonly string _settingsPath;

        public TxtConfigStream(string settingsPath)
        {
            _settingsPath = settingsPath;
        }
        public string[,] ReadStream()
        {
            if (_settingsPath == null)
                throw new ApplicationException("Не указан путь к файлу конфигурации!");

            var countLines = File.ReadAllLines(_settingsPath).Length;
            var retMas = new string[countLines, 3];
            var row = 0;

            try
            {
                using (var sr = new StreamReader(_settingsPath, Encoding.Default))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        var words = line.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                        for (var i = 0; i < words.Length; i++)
                        {
                            retMas[row, i] = words[i];
                        }
                        row++;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return retMas;
        }
    }
}
