using Quartz;
using Quartz.Impl;
using Quartz.Simpl;
using Quartz.Spi;
using QuartzManagementCore;
using System.Collections.Specialized;

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
            builder.Services.AddQuartz();
            //builder.Services.AddQuartzHostedService();
            builder.Services.AddOptions();
            builder.Services.Configure<QuartzOptions>(c =>
            {

                foreach(var key in c.Keys)
                {
                    c[key] = c
                }


            });
            builder.Services.AddScoped<ScheduleService>();
           // builder.Services.AddHostedService<SchedulerService>();

            builder.Services.AddControllers();
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

    public static class TestExt
    {
        /// <summary>
        /// Configures Quartz services to underlying service collection. This API maybe change!
        /// </summary>
        public static IServiceCollection AddQuartz1(this IServiceCollection services)
        {
            return AddQuartz(services, new NameValueCollection());
        }

        /// <summary>
        /// Configures Quartz services to underlying service collection. This API maybe change!
        /// </summary>
        public static IServiceCollection AddQuartz(this IServiceCollection services, NameValueCollection properties)
        {
            services.AddOptions();
            services.Configure<QuartzOptions>(options =>
            {
                foreach (var key in options.Keys)
                {
                    properties[key] = options[key];
                }
                //foreach (var key in schedulerBuilder.Properties.AllKeys)
                //{
                //    options[key] = schedulerBuilder.Properties[key];
                //}
            });


            //services.TryAddSingleton<MicrosoftLoggingProvider?>(serviceProvider =>
            //{
            //    var loggerFactory = serviceProvider.GetService<ILoggerFactory>();

            //    if (loggerFactory is null)
            //    {
            //        throw new InvalidOperationException($"{nameof(ILoggerFactory)} service is required");
            //    }

            //    LogContext.SetCurrentLogProvider(loggerFactory);

            //    return LogProvider.CurrentLogProvider as MicrosoftLoggingProvider;
            //});

            var schedulerBuilder = SchedulerBuilder.Create(properties);
            //if (configure != null)
            {
                //var target = new ServiceCollectionQuartzConfigurator(services, schedulerBuilder);
                //configure(target);
            }

            // try to add services if not present with defaults, without overriding other configuration
            if (string.IsNullOrWhiteSpace(properties[StdSchedulerFactory.PropertySchedulerTypeLoadHelperType]))
            {
               // services.TryAddSingleton(typeof(ITypeLoadHelper), typeof(SimpleTypeLoadHelper));
            }

            if (string.IsNullOrWhiteSpace(properties[StdSchedulerFactory.PropertySchedulerJobFactoryType]))
            {
                // there's no explicit job factory defined, use MS version
              //  services.TryAddSingleton(typeof(IJobFactory), typeof(PropertySettingJobFactory));
            }



            //services.TryAddSingleton<ContainerConfigurationProcessor>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>(x => new StdSchedulerFactory(properties));

            // Note: TryAddEnumerable() is used here to ensure the initializers are registered only once.
            //services.TryAddEnumerable(new[]
            //{
            //    ServiceDescriptor.Singleton<IPostConfigureOptions<QuartzOptions>, QuartzConfiguration>()
            //});

            return services;
        }
    }
}