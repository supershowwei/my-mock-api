namespace MyMockAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            app.UseRouting();

            // 捨棄常規路由，只使用屬性路由。
            app.MapControllers();

            app.Run();
        }
    }
}
