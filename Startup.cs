using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AspnCore2Empty_NetClit.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace AspnCore2Empty_NetClit
{
    public class Startup
    {
        
        public IConfiguration _config { get; set; }
        public Startup()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            _config = builder.Build();
            
        }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(provider => _config);
            services.AddSingleton<IMensagem,MensagemConfiguration>();
            services.AddTransient<IMensagem,MensagemClasse>();
            IFileProvider physicalProvider = new PhysicalFileProvider(Directory.GetCurrentDirectory());
            services.AddSingleton<IFileProvider>(physicalProvider);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IMensagem msg, IFileProvider phf )
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions(){
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"TesteAcessarArquivo")),
                RequestPath = new PathString("/TesteAcessarArquivo")
            });

            app.Run(async (context) =>
            {
                msg.Getmensagem();
                // var mensagem = _config["Mensagem"];
                phf.GetDirectoryContents("ad");
                var mensagem = msg.Getmensagem();
                 //  await context.Response.WriteAsync(j.Getmensagem());
                await context.Response.WriteAsync(mensagem);
            });
        }
    }
}
