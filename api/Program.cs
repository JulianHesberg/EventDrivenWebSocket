using System.Reflection;
using api;
using api.Models.Services;
using api.State;
using Fleck;
using lib;

var app = await ApiStartUp.StartApi();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.Run();

namespace api
{
    public static class ApiStartUp
    {
        public static async Task<WebApplication> StartApi()
        { 
            var builder = WebApplication.CreateBuilder();
            builder.Services.AddSingleton<ClientConnections>();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            var services =  builder.FindAndInjectClientEventHandlers(Assembly.GetExecutingAssembly());

            var app = builder.Build();
            
            var server = new WebSocketServer("ws://0.0.0.0:8181");
            
            server.Start(socket =>
            {
                socket.OnOpen = () =>
                {
                    StateService.AddConnection(socket);
                    socket.Send("Welcome to the server, a friendly chatty place.");
                };
                socket.OnClose = () =>
                {
                    app.Services.GetService<ClientConnections>().RemoveConnectionFromPool(socket);
                };
                socket.OnMessage = async message =>
                {
                    try
                    {
                        await app.InvokeClientEventHandler(services, socket, message);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        Console.WriteLine(e.InnerException);
                        Console.WriteLine(e.Message);
                    }
                };
            });

            return app;
        }
    }
    
}
