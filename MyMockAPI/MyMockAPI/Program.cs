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

            // �˱�`�W���ѡA�u�ϥ��ݩʸ��ѡC
            app.MapControllers();

            app.Run();
        }
    }
}
