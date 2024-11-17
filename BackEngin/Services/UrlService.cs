using BackEngin.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Web;

namespace BackEngin.Services
{
    public class UrlService : IUrlService
    {
        private readonly LinkGenerator _linkGenerator;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _frontendBaseUrl;

        public UrlService(LinkGenerator linkGenerator, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _linkGenerator = linkGenerator;
            _httpContextAccessor = httpContextAccessor;
            _frontendBaseUrl = configuration["Frontend:BaseUrl"] ?? Environment.GetEnvironmentVariable("FRONTEND_BASE_URL");
        }

        public string? GenerateBackendUrl(string routeName, object values)
        {
            var request = _httpContextAccessor.HttpContext?.Request;

            if (request == null)
                throw new InvalidOperationException("HTTP context is not available.");

            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
                throw new InvalidOperationException("HTTP context is not available.");

            // Generate the absolute URL
            return _linkGenerator.GetUriByRouteValues(
                httpContext,
                routeName,
                values,
                request.Scheme,
                request.Host
            );
        }

        public string GenerateFrontendUrl(string path, object queryParams = null)
        {
            var baseUrl = _frontendBaseUrl.TrimEnd('/');
            var fullPath = $"{baseUrl}/{path.TrimStart('/')}";

            if (queryParams == null) return fullPath;

            // Build the query string
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            foreach (var property in queryParams.GetType().GetProperties())
            {
                queryString[property.Name] = property.GetValue(queryParams)?.ToString();
            }

            return $"{fullPath}?{queryString}";
        }
    }
}
