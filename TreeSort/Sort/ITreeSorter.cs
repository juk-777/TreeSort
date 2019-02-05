using System.Collections.Generic;
using TreeSort.Config;

namespace TreeSort.Sort
{
    public interface ITreeSorter
    {
        List<ConfigEntity> Sort(List<ConfigEntity> configEntityList);
    }
}
