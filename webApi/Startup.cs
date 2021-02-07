using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Coomes.SpendingReports.Api.Transactions.Operations;
using Coomes.SpendingReports.Api.Transactions;
using Coomes.SpendingReports.CsvData;

namespace Coomes.SpendingReports.Web
{
    public class Startup
    {
        internal const string AllowAll = "allowAnyOrigin";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // TODO: remove allow any CORS once web UI and API are run in the same container
            services.AddCors(options => 
            {
                options.AddPolicy(name: AllowAll, builder => 
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                });
            });

            services.AddControllers();

            // operations
            services.AddTransient<GetTransactions>();
            services.AddSingleton<ImportTransactions>();

            // data access
            var storageDir = Configuration.GetValue<string>("CsvStorage:Directory");
            services.AddSingleton<ITransactionData>(new TransactionData(storageDir));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(AllowAll);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
