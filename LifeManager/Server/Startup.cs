using System.Text.Json.Serialization;
using LifeManager.Server.Database;
using LifeManager.Server.Database.Implementation;
using LifeManager.Server.Model.Mapper;
using LifeManager.Server.Model.Mapper.Implementation;
using LifeManager.Server.Service;
using LifeManager.Server.Service.Implementation;
using LifeManager.Server.Service.Implementation.Tool;
using LifeManager.Server.User;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LifeManager.Server {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddDbContext<LifeManagerDatabaseContext>(options => options
                .UseLazyLoadingProxies()
                .UseNpgsql(Configuration.GetConnectionString("MyWebApiConnection"))
            );

            services.AddControllersWithViews()
                .AddJsonOptions(options => {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumMemberConverter());
                });

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "ClientApp/build"; });
            
            services.AddScoped<ILifeManagerRepository, LifeManagerRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IModelServiceTools, ModelServiceTools>();
            
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<IChoreService, ChoreService>();
            services.AddScoped<ILeisureActivityService, LeisureActivityService>();
            services.AddScoped<IPrincipleService, PrincipleService>();
            services.AddScoped<IRecurringTaskService, RecurringTaskService>();
            services.AddScoped<IToDoTaskService, ToDoTaskService>();
            
            services.AddScoped<IAppointmentMapper, AppointmentMapper>();
            services.AddScoped<IChoreMapper, ChoreMapper>();
            services.AddScoped<IItemMapper, ItemMapper>();
            services.AddScoped<ILeisureActivityMapper, LeisureActivityMapper>();
            services.AddScoped<IPrincipleMapper, PrincipleMapper>();
            services.AddScoped<IRecurringTaskMapper, RecurringTaskMapper>();
            services.AddScoped<ITaskMapper, TaskMapper>();
            services.AddScoped<IToDoTaskMapper, ToDoTaskMapper>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            else {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });

            app.UseSpa(spa => {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment()) {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}