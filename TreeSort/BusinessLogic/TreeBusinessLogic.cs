using System;
using System.Threading;
using System.Threading.Tasks;
using TreeSort.Config;

namespace TreeSort.BusinessLogic
{
    public class TreeBusinessLogic : ITreeBusinessLogic
    {
        private readonly IConfigReader _configReader;

        public TreeBusinessLogic(IConfigReader configReader)
        {
            _configReader = configReader;
        }
        public void StartJob(CancellationToken token)
        {
            Console.WriteLine("\nНачинаю работу ...");

            try
            {
                token.ThrowIfCancellationRequested();

                Console.WriteLine("\nСчитывание конфигурации ...");
                var configEntityList = _configReader.ReadConfig();

                if (configEntityList == null || configEntityList.Count == 0)
                    throw new ArgumentException("Файл конфигурации пуст!");

                foreach (var configEntity in configEntityList)
                {
                    Console.WriteLine(configEntity.Id + " " + configEntity.Pid + " " + configEntity.Text);
                    
                }

            }
            catch (TaskCanceledException) { Console.WriteLine("\nОтмена операции StartJob до её запуска ..."); }
            catch (OperationCanceledException) { Console.WriteLine("\nОтмена операции StartJob ..."); }
            catch (Exception e) { Console.WriteLine(e.Message); throw; }
        }
    }
}
