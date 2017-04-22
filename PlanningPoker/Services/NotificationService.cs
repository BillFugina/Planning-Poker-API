using Microsoft.Extensions.Options;
using PlanningPoker.Interfaces.Services;
using PlanningPoker.Model;
using PusherServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanningPoker.Services
{
    public class NotificationService : INotificationService
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

        public async void StartSession(string sessionName)
        {
            var result = await _pusher.TriggerAsync(sessionName, "BeginSession", "");
        }

        public async void RegisterVote(string sessionName, Vote vote)
        {
            var result = await _pusher.TriggerAsync(sessionName, "RegisterVote", vote);
        }

        public async void RegisterParticipant(string sessionName, Participant participant)
        {
            var result = await _pusher.TriggerAsync(sessionName, "RegisterParticipant", participant);
        }

        public async void PrepareRound(string sessionName, Round round)
        {
            var result = await _pusher.TriggerAsync(sessionName, "PrepareRound", round);
        }

        public async void StartCountdown(string sessionName, Round round)
        {
            var result = await _pusher.TriggerAsync(sessionName, "StartCountdown", round);
        }

    }
}
