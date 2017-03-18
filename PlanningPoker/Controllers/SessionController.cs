using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PlanningPoker.Model;
using PlanningPoker.Services;

namespace PlanningPoker.Controllers
{
    [Route("api/")]
    public class SessionController : Controller
    {
        private readonly ISessionsService _sessionsService;

        public SessionController(
            ISessionsService sessionsService
            )
        {
            _sessionsService = sessionsService;
        }

        [HttpPost]
        [Route("sessions")]
        public SessionId CreateSession([FromBody] SessionApplication application)
        {
            var session = _sessionsService.CreateSession(application.SessionName, application.MasterName);
            var result = new SessionId()
            {
                Id = session.Id,
                Name = session.Name
            };
            return result;
        }

        [HttpPost]
        [Route("sessions/{sessionName}/participants")]
        public void JoinSession([FromRoute]string sessionName, [FromBody]ParticipantApplication application)
        {
            _sessionsService.JoinSession(sessionName, application.Name);
        }

        [HttpGet]
        [Route("sessions/{sessionId}")]
        public Session GetSession([FromRoute] Guid sessionId)
        {
            return _sessionsService.GetSession(sessionId);
        }

        [HttpPost]
        [Route("sessions/{sessionId}/rounds")]
        public int StartRound([FromRoute] Guid sessionId)
        {
            var round = _sessionsService.StartRound(sessionId);
            return round.Id;
        }

        [HttpPost]
        [Route("sessions/{sessionName}/rounds/{roundId}/votes")]
        public void Vote([FromRoute]string sessionName, [FromRoute] int roundId, [FromBody] VoteBallot ballot)
        {
            _sessionsService.Vote(sessionName, ballot.ParticipantName,  roundId, ballot.Value);
        }

        [HttpDelete]
        [Route("sessions/{sessionId}/rounds/{roundId}")]
        public void EndRound([FromRoute] Guid sessionId, [FromRoute] int roundId)
        {
            _sessionsService.EndRound(sessionId, roundId);
        }

        [HttpDelete]
        [Route("sessions/{sessionId}")]
        public void EndSession([FromRoute] Guid sessionId)
        {
            _sessionsService.EndSession(sessionId);
        }

    }
}
