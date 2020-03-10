using Microsoft.AspNetCore.SignalR;
using ServicesStateMonitor.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServicesStateMonitor.Hubs
{
    public class ServicesHub : Hub
    {
        //https://docs.microsoft.com/ru-ru/aspnet/core/tutorials/signalr?view=aspnetcore-3.1&tabs=visual-studio

        public async Task UpdateMap(List<Service> services)
        {
            await Clients.All.SendAsync("UpdateTrigger", services);
        }
    }
}