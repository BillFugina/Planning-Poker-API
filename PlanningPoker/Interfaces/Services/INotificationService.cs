﻿using PlanningPoker.Model;

namespace PlanningPoker.Interfaces.Services
{
    public interface INotificationService
    {
        void StartCountdown(string sessionName, Round round);
        void RegisterVote(string sessionName, Vote vote);
        void RegisterParticipant(string sessionName, Participant participant);
        void StartSession(string sessionName);
        void PrepareRound(string sessionName, Round round);
    }
}