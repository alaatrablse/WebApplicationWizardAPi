using WebApplicationWizardAPi.Models.DataManager;
using WebApplicationWizardAPi.Models.DTO;
using WebApplicationWizardAPi.Models.Repository;
using WebApplicationWizardAPi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.FileProviders;

namespace WebApplicationWizardAPi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        readonly string _specificOrigin = "_specificOrigin";
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("_specificOrigin",
                    p => p.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });
            services.AddDbContext<WizardDBContext>(opts => opts.UseSqlServer(Configuration["ConnectionString:WizardDB"]));
            services.AddScoped<IDataRepository<User, UserDTO>, UserDataManager>();
            services.AddScoped<IDataRepository<Wizard, WizardDTO>, WizardDataManager>();
            services.AddScoped<IDataRepository<Page, PageDTO>, PageDataManager>();
            services.AddScoped<IDataRepository<WizardDatum, WizardDataDTO>, WizardDataDataManager>();
            services.AddScoped<IDataRepository<Answer, AnswerDTO>, AnswerDataManager>();


            services.AddControllers()
                .AddNewtonsoftJson(
                    options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseDefaultFiles();
            //app.UseStaticFiles();

            _ = app.UseFileServer(new FileServerOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(),"StaticFile")),
                RequestPath = "",
                EnableDefaultFiles = true
            });

            app.UseCors(_specificOrigin);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
