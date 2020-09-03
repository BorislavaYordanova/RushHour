using RushHour.Mappings;

namespace RushHour.App_Start
{
    public class Bootstraper
    {
        public static void Run()
        {
            AutoMapperConfiguration.Configure();
        }
    }
}