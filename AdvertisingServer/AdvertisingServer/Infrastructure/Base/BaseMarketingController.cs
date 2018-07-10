using AdvertisingServer.Infrastructure.Containers;
using Microsoft.AspNetCore.Mvc;

namespace AdvertisingServer.Infrastructure.Base
{
    public class BaseMarketingController : ControllerBase
    {
        protected StringsContainer Container;

        protected BaseMarketingController()
        {
            Container = new StringsContainer();
        }

        protected bool CheckValue(object value, string name)
        {
            switch (value)
            {
                case string s when string.IsNullOrWhiteSpace(s):
                case int d when d <= 0:
                case null:
                    {
                        Container.Add(name);
                        return true;
                    }
                default: return false;
            }
        }
    }
}
