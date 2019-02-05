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

            return result.ToList();
        }
    }
}
