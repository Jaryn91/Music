using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Musiction.API.Entities;
using Musiction.API.Models;
using Musiction.API.Services;
using NLog.Extensions.Logging;

namespace Musiction.API
{
    public class Startup
    {
        public static IConfiguration Configuration { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddTransient<IMailService, LocalMailService>();

            var connectionString = Startup.Configuration["connectionStrings:songDBConnectionString"];
            services.AddDbContext<SongContext>(o => o.UseSqlServer(connectionString));

            services.AddScoped<ISongRepository, SongRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory,
            SongContext songContext)
        {
            loggerFactory.AddConsole();

            loggerFactory.AddDebug();

            loggerFactory.AddNLog();

            songContext.EnsureSeedDataForContext();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStatusCodePages();

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Song, SongDto>();
                cfg.CreateMap<SongForCreationDto, Song>();
                cfg.CreateMap<Song, SongForUpdateDto>();
                cfg.CreateMap<SongForUpdateDto, Song>();

            });
            app.UseMvc();

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
