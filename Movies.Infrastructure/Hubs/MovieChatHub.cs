using Microsoft.AspNetCore.SignalR;
using Movies.Infrastructure.Models.Messages;
using System.Threading.Tasks;

namespace Movies.Infrastructure.Hubs
{
    public class MovieChatHub: Hub
    {
        public async Task SendMessageAsync(ChatMessageRequest request, string groupName)
        {
            await Clients.Group(groupName).SendAsync("ReceiveMessageAsync", request);
        }

        public async Task AddUserToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }
        
        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }
    }
}
