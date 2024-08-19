using Microsoft.OpenApi.Models;
using System.Reflection;
using TestTaskVmarmysh.Filters;
using TestTaskVmarmysh.Services;

namespace TestTaskVmarmysh
{
    /// <summary>
    /// Main programm class.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main method.
        /// </summary>
        /// <param name="args">Arguments.</param>
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers((options) => {
                options.Filters.Add<AppExceptionFilter>();
            });
            builder.Services.AddServices();
            builder.Services.AddSwaggerGen(setupAction =>
            {
                var xmlDocumentationFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlDocumentationFullPath = Path.Combine(AppContext.BaseDirectory, xmlDocumentationFile);
                setupAction.IncludeXmlComments(xmlDocumentationFullPath, true);
            });


            var app = builder.Build();
            app.Services.SetupServices();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.Run();
        }
    }
}
