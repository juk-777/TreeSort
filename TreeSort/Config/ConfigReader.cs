using System;
using System.Collections.Generic;

namespace TreeSort.Config
{
    public class ConfigReader : IConfigReader
    {
        private readonly IConfigStream _configStream;
        private readonly IConfigEntityCreator _configEntityCreator;

        public ConfigReader(IConfigStream configStream, IConfigEntityCreator configEntityCreator)
        {
            _configStream = configStream;
            _configEntityCreator = configEntityCreator;
        }
        public List<ConfigEntity> ReadConfig()
        {
            string[,] confMas = _configStream.ReadStream();

            List<ConfigEntity> configEntityList = new List<ConfigEntity>();

            var rows = confMas.GetUpperBound(0) + 1;
            var columns = confMas.Length / rows;

            var words = new string[columns];

            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < columns; j++)
                {
                    words[j] = confMas[i, j];
                }
                configEntityList.Add(_configEntityCreator.CreateEntity(words));
            }

            Console.WriteLine("\nКонфигурация считана успешно...");
            return configEntityList;
        }
    }
}
