using System;
using System.Collections.Generic;
using System.Linq;
using TreeSort.Config;

namespace TreeSort.Sort
{
    public class TreeSorter : ITreeSorter
    {
        private List<ConfigEntity> SortedList { get; set; }

        public List<ConfigEntity> Sort(List<ConfigEntity> configEntityList)
        {
            SortedList = new List<ConfigEntity>();

            var result = from configEntity in configEntityList
                         orderby configEntity.Pid, configEntity.Text
                         select configEntity;

            SortedList = FlatToHierarchy(result.ToList());

            Console.WriteLine("\nСортировка выполнена успешно...");
            return SortedList;
        }

        private List<ConfigEntity> FlatToHierarchy(List<ConfigEntity> list)
        {
            var lookup = new Dictionary<int, ConfigEntity>();
            var nested = new List<ConfigEntity>();

            foreach (var item in list)
            {
                if (lookup.ContainsKey(item.Pid))
                {
                    lookup[item.Pid].Children.Add(item);
                }
                else
                {
                    nested.Add(item);
                }
                lookup.Add(item.Id, item);
            }

            return nested;
        }
    }
}
