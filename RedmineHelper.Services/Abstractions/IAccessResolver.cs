namespace RedmineHelper.Services.Abstractions
{
    using System.Threading.Tasks;
    using Models.Dto;
    public interface IAccessResolver
    {
        public bool ResolveAccess();
        public Task<bool> SaveKey(RedmineApiKey redmineApiKey);

    }
}
