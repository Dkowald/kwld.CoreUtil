using System.IO;
using kwd.CoreUtil.FileSystem;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace kwd.CoreUtil.Doc
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {}

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    var file = new DirectoryInfo(env.WebRootPath)
                        .GetFile("Index.html");
                    
                    await context.Response.WriteAsync(await file.ReadAllTextAsync());
                });
            });
        }
    }
}
