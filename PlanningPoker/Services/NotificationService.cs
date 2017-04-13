using Microsoft.Extensions.Options;
using PusherServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanningPoker.Services
{
    public class NotificationService
    {
        private readonly Pusher _pusher;
        private readonly Model.Configuration.Pusher _pusherOptions;

        public NotificationService(
            IOptions<Model.Configuration.Pusher> pusherOptions
            )
        {
            _pusherOptions = pusherOptions.Value;
            _pusher = new Pusher(_pusherOptions.AppID, _pusherOptions.Key, _pusherOptions.Secret);
        }

        public async void RegisterVote(string sessionName)
        {
            var result = await _pusher.TriggerAsync(sessionName, "RegisterVote", "");
        }

    }
}
