using System.Collections.Generic;

namespace TreeSort.Config
{
    public interface IConfigReader
    {
        List<ConfigEntity> ReadConfig();
    }
}
