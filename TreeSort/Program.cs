using System;
using System.IO;
using System.Threading;
using TreeSort.BusinessLogic;
using TreeSort.Config;
using TreeSort.Output;
using TreeSort.Sort;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace TreeSort
{
    static class Program
    {
        static void Main()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Добро пожаловать в TreeSort ...");
            Console.ForegroundColor = ConsoleColor.Gray;

            var container = new UnityContainer();
            var configPath = GetFile();
            var fileExtension = Path.GetExtension(configPath);
            if (fileExtension == ".txt")
                container.RegisterType<IConfigStream, TxtConfigStream>(new InjectionConstructor(new InjectionParameter<string>(configPath)));
            else throw new ApplicationException("Не поддерживаемый формат файла!");

            #region RegisterTypes

            container.RegisterType<IConfigReader, ConfigReader>();
            container.RegisterType<IConfigEntityCreator, ConfigEntityCreator>();
            container.RegisterType<ITreeSorter, TreeSorter>();
            //container.RegisterType<ITreeOutput, ConsoleOutput>();
            container.RegisterType<ITreeOutput, TxtFileOutput>();
            container.RegisterType<ITreeBusinessLogic, TreeBusinessLogic>(new ContainerControlledLifetimeManager());

            var businessLogic = container.Resolve<ITreeBusinessLogic>();

            #endregion

            try
            {
                var cts = new CancellationTokenSource();
                var token = cts.Token;

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nДля запуска работы нажмите Enter");
                Console.WriteLine("Для завершения работы нажмите Enter");
                Console.ForegroundColor = ConsoleColor.Gray;

                Console.ReadLine();

                businessLogic.StartJobAsync(token);

                Console.WriteLine();
                Console.ReadLine();
                cts.Cancel();

                Console.WriteLine("\nЗавершение работы ...");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                container.Dispose();
            }

            Console.WriteLine("\nДо скорой встречи в TreeSort ...");
            Console.ReadLine();
        }

        private static string GetFile()
        {
            string configPath = Path.Combine(@"Files", @"input_tree.txt");
            Console.WriteLine("--------------------");
            Console.WriteLine($"Введите '1' для того, чтобы считать конфигурацию из предварительно подготовленного XML файла {configPath}");
            Console.WriteLine("\nЛибо введите любой другой путь к файлу конфигурации.");
            configPath = Console.ReadLine();

            switch (configPath)
            {
                case "1":
                    configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Files", @"input_tree.txt");
                    break;
                default:
                    if (!string.IsNullOrEmpty(configPath))
                    {
                        var fileInf = new FileInfo(configPath);
                        if (!fileInf.Exists)
                            throw new ApplicationException("Ошибка при указании пути к файлу конфигурации!");
                    }
                    else throw new ApplicationException("Ошибка при указании пути к файлу конфигурации!");

                    break;
            }

            return configPath;
        }
    }
}
