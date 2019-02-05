using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TreeSort.Config;

namespace TreeSort.Output
{
    public class ConsoleOutput : ITreeOutput
    {
        public async Task OutputAsync(List<ConfigEntity> configEntityList)
        {
            Console.WriteLine("\nВывод на консоль...");
            await Task.Run(() => SpaseOutput(configEntityList));
        }

        private void SpaseOutput(List<ConfigEntity> configEntityList)
        {
            var spacing = "";
            const string gap = "  ";

            for (var i = 0; i < configEntityList.Count; i++)
            {
                if (i > 0)
                {
                    if (configEntityList[i].Pid > configEntityList[i - 1].Pid)
                    {
                        spacing += gap;
                        Console.WriteLine("----------");
                    }
                }

                Console.WriteLine(spacing + configEntityList[i].Id + ";" + configEntityList[i].Pid + ";" + configEntityList[i].Text);
            }
        }
    }
}
