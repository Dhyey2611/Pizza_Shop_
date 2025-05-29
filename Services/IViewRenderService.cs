using System.Threading.Tasks;

namespace Pizza_Shop_.Services.Interfaces
{
    public interface IViewRenderService
    {
        Task<string> RenderToStringAsync(string viewName, object model);
    }
}
