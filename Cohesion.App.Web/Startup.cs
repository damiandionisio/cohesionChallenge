using Cohesion.Core;
using Cohesion.Core.ServiceRequest.Commands.Create;
using Cohesion.Core.ServiceRequest.Commands.Delete;
using Cohesion.Core.ServiceRequest.Commands.Update;
using Cohesion.Core.ServiceRequest.Models;
using Cohesion.Core.ServiceRequest.Queries.GetServiceRequestById;
using Cohesion.Core.ServiceRequest.Queries.GetServiceRequests;
using Cohesion.Core.ServiceRequest.Utilities;
using Cohesion.Infrastructure.ServiceRequest.Readers;
using Cohesion.Infrastructure.ServiceRequest.Writers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Cohesion.App.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Cohesion.App.Web", Version = "v1" });
            });

            services.AddTransient<IQueryHandler<ServiceRequestQueryHandler.Arguments, ServiceRequestQueryHandler.Result>, ServiceRequestQueryHandler.Handler>();
            services.AddTransient<IQueryHandler<ServiceRequestByIdQueryHandler.Arguments, ServiceRequestByIdQueryHandler.Result>, ServiceRequestByIdQueryHandler.Handler>();
            services.AddTransient<ICommandHandler<CreateServiceRequestCommandHandler.Arguments>, CreateServiceRequestCommandHandler.Handler>();
            services.AddTransient<ICommandHandler<UpdateServiceRequestCommandHandler.UpdateArguments>, UpdateServiceRequestCommandHandler.Handler>();
            services.AddTransient<ICommandHandler<DeleteServiceRequestCommandHandler.DeleteArguments>, DeleteServiceRequestCommandHandler.Handler>();

            services.AddTransient<IWriterHandler<CreateServiceRequestCommandHandler.Arguments>, CreateServiceRequestWriter>();
            services.AddTransient<IWriterHandler<UpdateServiceRequestCommandHandler.UpdateArguments>, UpdateServiceRequestWriter>();
            services.AddTransient<IWriterHandler<DeleteServiceRequestCommandHandler.DeleteArguments>, DeleteServiceRequestWriter>();
            services.AddTransient<IReaderHandler<ServiceRequestQueryHandler.Arguments, ServiceRequestQueryHandler.Result>, ServiceRequestReaderHandler>();
            services.AddTransient<IReaderHandler<ServiceRequestByIdQueryHandler.Arguments, ServiceRequestByIdQueryHandler.Result>, ServiceRequestByIdReaderHandler>();

            services.AddTransient<IServiceRequestStatus, CompleteServiceRequestStatus>();
            services.AddTransient<IEmailUtility, EmailUtility>();

            services.Configure<SMTPConfig>(Configuration.GetSection("SMTPConfig"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cohesion.App.Web v1"));
            }

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
