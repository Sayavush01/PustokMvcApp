using PustokMvcApp.Data;

namespace PustokMvcApp.Services
{
    public class LayoutService(PustokMvcAppDbContext context)
    {
        public Dictionary<string,string> GetSettings()
        {
            return context.Settings.ToDictionary(s=>s.Key,s=>s.Value);
        }
    }
}
