using System;
using System.Threading;
using System.Threading.Tasks;
using TreeSort.Config;
using TreeSort.Output;
using TreeSort.Sort;

namespace TreeSort.BusinessLogic
{
    public class TreeBusinessLogic : ITreeBusinessLogic
    {
        private readonly IConfigReader _configReader;
        private readonly ITreeSorter _treeSorter;
        private readonly ITreeOutput _treeOutput;

        public TreeBusinessLogic(IConfigReader configReader, ITreeSorter treeSorter, ITreeOutput treeOutput)
        {
            _configReader = configReader;
            _treeSorter = treeSorter;
            _treeOutput = treeOutput;
        }
        public async Task StartJobAsync(CancellationToken token)
        {
            try
            {
                token.ThrowIfCancellationRequested();

                var configEntityList = _configReader.ReadConfig();

                if (configEntityList == null || configEntityList.Count == 0)
                    throw new ArgumentException("Файл конфигурации пуст!");

                var configEntityListSort = _treeSorter.Sort(configEntityList);

                await _treeOutput.OutputAsync(configEntityListSort);

            }
            catch (TaskCanceledException) { Console.WriteLine("\nОтмена операции StartJob до её запуска ..."); }
            catch (OperationCanceledException) { Console.WriteLine("\nОтмена операции StartJob ..."); }
            catch (Exception e) { Console.WriteLine(e.Message); throw; }
        }
    }
}
