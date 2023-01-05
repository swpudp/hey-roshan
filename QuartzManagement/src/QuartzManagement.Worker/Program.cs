using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.Impl;
using Quartz.Logging;
using Quartz.Simpl;
using Quartz.Spi;
using QuartzManagement.Core;
using System.Collections.Specialized;
using System.Globalization;

namespace QuartzManagement.Worker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //∆Ù”√≈‰÷√
            //builder.Services.Configure<QuartzOptions>(builder.Configuration.GetSection(nameof(QuartzOptions)));

            // Add services to the container.
            //builder.Services.AddQuartz(cfg =>
            //{
            //    //cfg.UsePersistentStore(c =>
            //    //{
            //    //    c.UseClustering();
            //    //    c.UseMySql(builder.Configuration.GetConnectionString("Quartz"));
            //    //    c.UseJsonSerializer();
            //    //});
            //    //cfg.UseDefaultThreadPool();
            //});
            //builder.Services.AddQuartz(q =>
            //{
            //    q.AddCalendar<GregorianCalendar>();
            //});
            //builder.Services.AddQuartzHostedService();
            //builder.Services.AddOptions();
            builder.Services.AddOptions<QuartzOptions>().Bind(builder.Configuration.GetSection("QuartzOptions"));
            builder.Services.AddSingleton<ScheduleService>();
            builder.Services.AddHostedService<ScheduleHostedService>();


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            LogProvider.SetCurrentLogProvider(new MicrosoftLoggingProvider(app.Services.GetService<ILoggerFactory>()));
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}