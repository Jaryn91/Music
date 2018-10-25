using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Musiction.API.BusinessLogic;
using Musiction.API.Entities;
using Musiction.API.IBusinessLogic;
using Musiction.API.Models;
using Musiction.API.Services;
using NLog.Extensions.Logging;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Musiction.API
{
    public class Startup
    {
        public static IConfiguration Configuration { get; private set; }

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            CreateAuthentication(services);

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                    });
            });

            services.AddMvc();

            var propName = Startup.Configuration["env"] + ":ConnectionString";
            var connectionString = Startup.Configuration[propName];

            services.AddDbContext<SongContext>(o => o.UseMySql(connectionString));

            RegisterContainers(services);
        }

        private void CreateAuthentication(IServiceCollection services)
        {
            string domain = $"https://{Configuration[Configuration["env"] + ":Auth0:Domain"]}/";
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.Authority = domain;
                options.Audience = Configuration[Configuration["env"] + ":Auth0:ApiIdentifier"];
            });
        }


        private void RegisterContainers(IServiceCollection services)
        {
            services.AddTransient<IMailService, LocalMailService>();
            services.AddScoped<ISongRepository, SongRepository>();
            services.AddSingleton<IFileAndFolderPathsCreator, FileAndFolderPathsCreator>();
            services.AddSingleton<IFileSaver, FileSaver>();
            services.AddSingleton<IOutcomeTextCreator, OutcomeTextCreator>();
            services.AddSingleton<IGoogleSlides, GoogleSlides>();
        }

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


            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            app.UseStatusCodePages();

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Song, SongDto>();
                cfg.CreateMap<SongForCreationDto, Song>();
                cfg.CreateMap<Song, SongForUpdateDto>();
                cfg.CreateMap<SongForUpdateDto, Song>();
            });

            DefaultFilesOptions options = new DefaultFilesOptions();
            options.DefaultFileNames.Clear();
            options.DefaultFileNames.Add("login.html");
            app.UseDefaultFiles(options);
            app.UseStaticFiles();


            app.UseAuthentication();
            app.UseCors("AllowAll");
            app.UseMvc();


            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
