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
        private const string Gap = "  ";
        private readonly StringBuilder _outTree = new StringBuilder();

        public async Task OutputAsync(List<ConfigEntity> configEntityList)
        {
            var path = AppDomain.CurrentDomain.BaseDirectory + @"Files";
            var dirInfo = new DirectoryInfo(path);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }

            var writePath = Path.Combine(path, "output_tree.txt");
            const string spacing = "";

            foreach (var configEntity in configEntityList)
            {
                _outTree.Append(spacing + configEntity.Id + ";" + configEntity.Pid + ";" + configEntity.Text);
                _outTree.AppendLine();
                GetChild(configEntity, spacing);
            }

            using (var sw = new StreamWriter(writePath, false, Encoding.Default))
            {
                await sw.WriteLineAsync(_outTree.ToString());
            }

            Console.WriteLine("\nФайл сохранен");
        }

        private void GetChild(ConfigEntity configEntity, string spacing)
        {
            if (configEntity.Childrens.Count != 0)
            {
                spacing += Gap;
                foreach (var child in configEntity.Childrens)
                {
                    _outTree.Append(spacing + child.Id + ";" + child.Pid + ";" + child.Text);
                    _outTree.AppendLine();
                    GetChild(child, spacing);
                }
            }
        }
    }
}
