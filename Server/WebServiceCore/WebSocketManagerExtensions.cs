using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Server.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Server
{
    public static class WebSocketManagerExtensions
    {
        /// <summary>
        /// Realiza a injeção de dependencia dos serviço de cliente e cria um Singleton dos clientes logados
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddWebSocketService(this IServiceCollection services)
        {
            services.AddTransient<ClientService>();

            foreach (var type in Assembly.GetEntryAssembly().ExportedTypes)
            {
                if (type.GetTypeInfo().BaseType == typeof(WebSocketHandler))
                {
                    services.AddSingleton(type);
                }
            }
            return services;
        }
        
        public static IApplicationBuilder MapWebSocketManager(this IApplicationBuilder app, PathString path, WebSocketHandler handler)
        {
            return app.Map(path, (_app) => {
                _app.UseMiddleware<WebSocketManagerMiddleware>(handler);
            });
        }
    }
}
