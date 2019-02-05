using System.Collections.Generic;
using System.Threading.Tasks;
using TreeSort.Config;

namespace TreeSort.Output
{
    public interface ITreeOutput
    {
        Task OutputAsync(List<ConfigEntity> configEntityList);
    }
}
