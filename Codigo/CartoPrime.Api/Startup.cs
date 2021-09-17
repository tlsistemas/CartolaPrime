using CartoPrime.Application;
using CartoPrime.Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Text;
using TM.Utils.Authentication;
using TM.Utils.Cryptography;
using TM.Utils.Email;
using TM.Utils.Filter;
using TM.Utils.Swagger.Filters;

namespace CartoPrime.Api
{
    public class Startup
    {
        TM.Utils.Options.Swagger.SwaggerOptions swaggerOptions = new TM.Utils.Options.Swagger.SwaggerOptions();
        JwtAuthenticationOptions jwtAuthenticationOptions = new JwtAuthenticationOptions();

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Configuration.GetSection("Swagger").Bind(swaggerOptions);
            Configuration.GetSection("Authentication:Jwt").Bind(jwtAuthenticationOptions);
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            #region Infrastructure
            services.AddTransient<IPasswordCipher, PasswordCipher>();
            services.AddHttpContextAccessor();
            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.Providers.Add<GzipCompressionProvider>();
            });
            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Fastest;
            });

            services.AddScoped<IEnviarEmail, SmtpEnviarEmail>();
            #endregion

            #region Context
            var conectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<DSContext>(o => o.UseMySql(conectionString, ServerVersion.AutoDetect(conectionString)));

            #endregion

            services.AddMvc();

            #region Configuration
            services.Configure<JwtAuthenticationOptions>(Configuration.GetSection("Authentication:Jwt"));
            #endregion

            DependenciesInjector.Register(services);

            services.Configure<IISServerOptions>(options =>
            {
                options.AutomaticAuthentication = false;
            });

            var corsBuilder = new CorsPolicyBuilder();
            corsBuilder.AllowAnyHeader();
            corsBuilder.AllowAnyMethod();
            //corsBuilder.WithOrigins("http://localhost/");
            corsBuilder.AllowAnyOrigin();
            corsBuilder.AllowCredentials();

            services.AddCors(options =>
            {
                options.AddPolicy("SiteCorsPolicy", corsBuilder.Build());
            });

            ConfigureSwagger(services);
            ConfigureAuthentication(services);
            ConfigureLocalization(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseDeveloperExceptionPage();


            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CartoPrime");
                c.DocExpansion(DocExpansion.None);
            });


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }


        #region Custom Configuration
        private void ConfigureSwagger(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddMvc(config =>
            {
                config.Filters.Add(typeof(CustomExceptionFilter));
            })
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            }
            ).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddSwaggerGenNewtonsoftSupport();
            services.AddMvcCore().AddApiExplorer();
            services.AddSwaggerGen(options =>
            {
                services.ConfigureSwaggerGen(options =>
                {
                    options.CustomSchemaIds(x => x.FullName);
                });
                options.EnableAnnotations();
                options.SwaggerDoc(swaggerOptions.APIVersion, new OpenApiInfo
                {
                    Version = swaggerOptions.APIVersion,
                    Title = swaggerOptions.APIName,
                    Description = swaggerOptions.Description,
                    TermsOfService = new Uri("https://www.tlsolucoes.com.br/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "TL Solucoes",
                        Email = String.Empty,
                        Url = new Uri("https://www.tlsolucoes.com.br"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://www.tlsolucoes.com.br/license"),
                    }
                });


                var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Bearer",
                    BearerFormat = "JWT",
                    Scheme = "bearer",
                    Description = "Specify the authorization token.",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement {
               {
                 new OpenApiSecurityScheme
                 {
                   Reference = new OpenApiReference
                   {
                     Type = ReferenceType.SecurityScheme,
                     Id = "Bearer"
                   }
                  },
                  new String[] { }
                }
              });

                //options.DescribeAllEnumsAsStrings();
                options.OperationFilter<SecurityRequirementsOperationFilter>();
                options.OperationFilter<ReplaceFileParamOperationFilter>();
                options.OperationFilter<FormDataOperationFilter>();
            });
        }
        private void ConfigureAuthentication(IServiceCollection services)
        {
            services
                .AddAuthentication()
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = jwtAuthenticationOptions.Issuer,
                        ValidateAudience = true,
                        ValidAudience = jwtAuthenticationOptions.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtAuthenticationOptions.SecretKey)),
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                    };
                });
        }
        private void ConfigureLocalization(IServiceCollection services)
        {
            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("pt-BR");
                options.SupportedCultures = new List<CultureInfo> { new CultureInfo("pt-BR") };
                options.SupportedUICultures = new List<CultureInfo> { new CultureInfo("pt-BR") };
                options.RequestCultureProviders.Clear();
            });
        }
        #endregion
    }
}
