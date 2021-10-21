namespace Baby_Tracker.Web
{
    using System;

    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;

    using Baby_Tracker.Web.Models;

    public class Program
    {
        public static void Main(string[] args)
        {
            DB_Access.DBUpgrade(typeof(Baby));
            DB_Access.DBUpgrade(typeof(BabyEventable));
            CreateHostBuilder(args).Build().Run();
        }



        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
