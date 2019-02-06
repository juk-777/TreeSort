using System;
using System.Collections.Generic;
using System.Linq;
using TreeSort.Config;

namespace TreeSort.Sort
{
    public class TreeSorter : ITreeSorter
    {
        public List<ConfigEntity> Sort(List<ConfigEntity> configEntityList)
        {
            var result = from configEntity in configEntityList
                         orderby configEntity.Pid, configEntity.Text
                         select configEntity;

            var sortedList = GetHierarchy(result.ToList());

            Console.WriteLine("\nСортировка выполнена успешно...");
            return sortedList;
        }

        private List<ConfigEntity> GetHierarchy(List<ConfigEntity> configEntityList)
        {
            var lookup = new Dictionary<int, ConfigEntity>();
            var rootEntities = new List<ConfigEntity>();

            foreach (var configEntity in configEntityList)
            {
                if (lookup.ContainsKey(configEntity.Pid))
                {
                    lookup[configEntity.Pid].Childrens.Add(configEntity);
                }
                else
                {
                    rootEntities.Add(configEntity);
                }
                lookup.Add(configEntity.Id, configEntity);
            }

            return rootEntities;
        }
    }
}
