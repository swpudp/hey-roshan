using Quartz;
using Quartz.Logging;
using QuartzManagement.Core;

namespace QuartzManagement.Worker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            //∆Ù”√≈‰÷√
            builder.Services.AddOptions<QuartzOptions>().Bind(builder.Configuration.GetSection(nameof(QuartzOptions)));
            // Add services to the container.
            LogProvider.SetCurrentLogProvider(new MicrosoftLoggingProvider(new LoggerFactory()));
            builder.Services.AddQuartz();
            builder.Services.AddQuartzHostedService(x => x.WaitForJobsToComplete = true);
            //builder.Services.AddOptions();
            builder.Services.AddOptions<QuartzOptions>().Bind(builder.Configuration.GetSection("QuartzOptions"));
            // if you are using persistent job store, you might want to alter some options
            builder.Services.Configure<QuartzOptions>(options =>
            {
                options.Scheduling.IgnoreDuplicates = true; // default: false
                options.Scheduling.OverWriteExistingData = true; // default: true
            });
            builder.Services.AddSingleton<ScheduleService>();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            WebApplication app = builder.Build();
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