using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TreeSort.Config;

namespace TreeSort.Output
{
    public class ConsoleOutput : ITreeOutput
    {
        private const string Gap = "  ";

        public async Task OutputAsync(List<ConfigEntity> configEntityList)
        {
            Console.WriteLine("\nВывод на консоль...\n");
            await Task.Run(() => SpaсeOutput(configEntityList));
        }

        private void SpaсeOutput(List<ConfigEntity> configEntityList)
        {
            const string spacing = "";
            foreach (var configEntity in configEntityList)
            {
                Console.WriteLine(spacing + configEntity.Id + ";" + configEntity.Pid + ";" + configEntity.Text);
                PrintChild(configEntity, spacing);
            }
        }

        private void PrintChild(ConfigEntity configEntity, string spacing)
        {
            if (configEntity.Childrens.Count != 0)
            {
                spacing += Gap;
                foreach (var child in configEntity.Childrens)
                {
                    Console.WriteLine(spacing + child.Id + ";" + child.Pid + ";" + child.Text);
                    PrintChild(child, spacing);
                }
            }
        }
    }
}
