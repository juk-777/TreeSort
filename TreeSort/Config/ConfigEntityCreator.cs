namespace TreeSort.Config
{
    public class ConfigEntityCreator : IConfigEntityCreator
    {
        public ConfigEntity CreateEntity(string[] words)
        {
            var configEntity = new ConfigEntity();

            if (int.TryParse(words[0], out var id))
            {
                configEntity.Id = id;
            }

            if (int.TryParse(words[1], out var pid))
            {
                configEntity.Pid = pid;
            }

            configEntity.Text = words[2];

            return configEntity;
        }
    }
}
