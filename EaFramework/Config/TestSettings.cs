using EaFramework.Driver;
using static EaFramework.Driver.DriverFixture;

namespace EaFramework.Config
{
    public class TestSettings
    {
        public BrowserType BrowserType { get; set; }

        public required Uri ApplicationUrl { get; init; }

        public float? TimeoutInternal { get; set; }





    }
}
