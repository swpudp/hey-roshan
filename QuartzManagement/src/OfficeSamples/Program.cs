using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Serialization;
using Quartz;

namespace OfficeSamples
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            // base configuration for DI, read from appSettings.json
            builder.Services.AddOptions<QuartzOptions>().Bind(builder.Configuration.GetSection("QuartzOptions"));

            // if you are using persistent job store, you might want to alter some options
            builder.Services.Configure<QuartzOptions>(options =>
            {
                options.Scheduling.IgnoreDuplicates = true; // default: false
                options.Scheduling.OverWriteExistingData = true; // default: true
            });

            builder.Services.AddQuartz();
            builder.Services.AddQuartzHostedService();

            builder.Services.AddControllers().AddNewtonsoftJson(x =>
            {
                x.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                x.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

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