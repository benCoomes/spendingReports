using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Coomes.SpendingReports.Api.Transactions.Operations;
using Coomes.SpendingReports.Api.Transactions;
using Coomes.SpendingReports.Api.Categories.Operations;
using Coomes.SpendingReports.Api.Categories;
using Coomes.SpendingReports.CsvData;

namespace Coomes.SpendingReports.Web
{
    public class Startup
    {
        internal const string AllowLocalhost = "allowLocalhost";

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
                options.AddPolicy(name: AllowLocalhost, builder => 
                {
                    builder.WithOrigins("http://localhost:8081");
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                });
            });

            services.AddControllers();

            // operations
            services.AddTransient<GetTransactions>();
            services.AddTransient<AddClassifier>();
            services.AddTransient<GetClassifiers>();
            services.AddSingleton<ImportTransactions>();

            // data access
            var storageDir = Configuration.GetValue<string>("CsvStorage:Directory");
            services.AddSingleton<ITransactionData>(new TransactionData(storageDir));
            services.AddSingleton<IClassifierData>(new ClassifierData(storageDir));
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

            app.UseCors(AllowLocalhost);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
