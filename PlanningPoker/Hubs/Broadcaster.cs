using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace PlanningPoker.Hubs
{
    public class Broadcaster : Hub<IBroadcaster>
    {
        public override Task OnConnected()
        {
            Clients.Client(Context.ConnectionId).SetConnectionId(Context.ConnectionId);
            return base.OnConnected();
        }

        public Task Subscribe(int groupId)
        {
            return Groups.Add(Context.ConnectionId, groupId.ToString());
        }

        public Task Unsubscribe(int groupId)
        {
            return Groups.Remove(Context.ConnectionId, groupId.ToString());
        }
    }

    public interface IBroadcaster
    {
        Task SetConnectionId(string connectionId);
    }
}
