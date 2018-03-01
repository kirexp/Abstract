using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace WebApi.Hub
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class LoopyHub : Microsoft.AspNetCore.SignalR.Hub
    {
        public Task Send(string data)
        {
            return this.Clients.All.InvokeAsync("Send", data);
        }
    }
}
