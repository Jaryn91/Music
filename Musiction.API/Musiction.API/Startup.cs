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
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Musiction.API
{
    public class Startup
    {
        private IGetValue _valueRetrieval;
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

            RegisterContainers(services);
            var sp = services.BuildServiceProvider();
            _valueRetrieval = sp.GetService<IGetValue>();

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


            var connectionString = _valueRetrieval.Get("ConnectionString");

            services.AddDbContext<SongContext>(o => o.UseMySql(connectionString));

        }

        private void CreateAuthentication(IServiceCollection services)
        {
            string domain = $"https://{_valueRetrieval.Get("Auth0:Domain")}/";
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.Authority = domain;
                options.Audience = _valueRetrieval.Get("Auth0:ApiIdentifier");
                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        if (context.SecurityToken is JwtSecurityToken token)
                        {
                            if (context.Principal.Identity is ClaimsIdentity identity)
                            {
                                identity.AddClaim(new Claim("access_token", token.RawData));
                            }
                        }

                        return Task.FromResult(0);
                    }
                };
            });
        }


        private void RegisterContainers(IServiceCollection services)
        {
            services.AddTransient<IPresentationRepository, PresentationRepository>();
            services.AddTransient<ISongRepository, SongRepository>();
            services.AddTransient<IFileAndFolderPathsCreator, FileAndFolderPathsCreator>();
            services.AddTransient<IGetValue, ValueRetrieval>();
            services.AddTransient<IOutcomeTextCreator, OutcomeTextCreator>();
            services.AddTransient<IGoogleSlides, GoogleSlides>();
            services.AddTransient<IConvertPresentation, PptxToZipConverter>();
            services.AddTransient<IMerge, PowerPointMerger>();
            services.AddTransient<IAccount, Account>();
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
                await context.Response.WriteAsync("No cześć! A Ty co tutaj robisz :)?");
            });
        }
    }
}
