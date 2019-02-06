using System.Collections.Generic;

namespace TreeSort.Config
{
    public class ConfigEntity
    {
        public int Id { get; set; }
        public int Pid { get; set; }
        public string Text { get; set; }
        public List<ConfigEntity> Children { get; set; } = new List<ConfigEntity>();
    }
}
