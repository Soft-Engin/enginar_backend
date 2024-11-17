namespace BackEngin.Services.Interfaces
{
    public interface IUrlService
    {
        string GenerateBackendUrl(string routeName, object values);
        string GenerateFrontendUrl(string path, object queryParams = null);
    }
}
