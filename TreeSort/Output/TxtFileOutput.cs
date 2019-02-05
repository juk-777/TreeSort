using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TreeSort.Config;

namespace TreeSort.Output
{
    class TxtFileOutput : ITreeOutput
    {
        public async Task OutputAsync(List<ConfigEntity> configEntityList)
        {
            var path = AppDomain.CurrentDomain.BaseDirectory + @"Files";
            var dirInfo = new DirectoryInfo(path);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }

            var writePath = Path.Combine(path, "output_tree.txt");

            var spacing = "";
            const string gap = "  ";
            var outTree = new StringBuilder();

            for (var i = 0; i < configEntityList.Count; i++)
            {
                if (i > 0)
                {
                    if (configEntityList[i].Pid > configEntityList[i - 1].Pid)
                    {
                        spacing += gap;
                    }
                }

                outTree.Append(spacing + configEntityList[i].Id + ";" + configEntityList[i].Pid + ";" + configEntityList[i].Text);
                outTree.AppendLine();
            }

            using (var sw = new StreamWriter(writePath, false, Encoding.Default))
            {
                await sw.WriteLineAsync(outTree.ToString());
            }

            Console.WriteLine("\nФайл сохранен");
        }
    }
}
