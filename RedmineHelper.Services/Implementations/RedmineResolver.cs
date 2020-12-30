namespace RedmineHelper.Services.Implementations
{
    using System;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Models.Dto;
    using System.IO;
    using Abstractions;
    public class RedmineResolver : IAccessResolver
    {
        private readonly string _filePath;

        public RedmineResolver()
        {
            const string fileWithKey = "apikey.json";
            _filePath = Path.Combine(Directory.GetCurrentDirectory(), "Configuration" , fileWithKey);
        }

        public bool ResolveAccess()
        {
            return File.Exists(_filePath);
        }

        public async Task<bool> SaveKey(RedmineApiKey redmineApiKey)
        {
            if (redmineApiKey == null || string.IsNullOrEmpty(redmineApiKey.Key))
                throw new ArgumentException("Api ключ не указан");

            try
            {
                await File.WriteAllTextAsync(_filePath, JsonConvert.SerializeObject(redmineApiKey));
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
