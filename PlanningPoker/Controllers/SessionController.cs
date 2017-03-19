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

        /// <summary>
        /// Create a new session.
        /// </summary>
        /// <param name="application">The name of the session to create.</param>
        /// <returns>The The created session.</returns>
        [HttpPost]
        [Route("sessions")]
        public Session CreateSession([FromBody] SessionApplication application)
        {
            var session = _sessionsService.CreateSession(application.SessionName, application.MasterName);
            return session;
        }

        /// <summary>
        /// Join an existing session.
        /// </summary>
        /// <param name="sessionName">Name of the session to join.</param>
        /// <param name="participant">Information about the person joining the session.</param>
        [HttpPost]
        [Route("sessions/{sessionName}/participants")]
        public void JoinSession([FromRoute]string sessionName, [FromBody]ParticipantApplication participant)
        {
            _sessionsService.JoinSession(sessionName, participant.Name);
        }

        /// <summary>
        /// Get information about an existing session.
        /// </summary>
        /// <param name="sessionId">The Id of the session</param>
        /// <returns>The Session</returns>
        [HttpGet]
        [Route("sessions/{sessionId}")]
        public Session GetSession([FromRoute] Guid sessionId)
        {
            return _sessionsService.GetSession(sessionId);
        }

        /// <summary>
        /// Start a new round.
        /// </summary>
        /// <param name="sessionId">The id of the session where the round will be started</param>
        /// <returns>The id of the new round.</returns>
        [HttpPost]
        [Route("sessions/{sessionId}/rounds")]
        public int StartRound([FromRoute] Guid sessionId)
        {
            var round = _sessionsService.StartRound(sessionId);
            return round.Id;
        }

        /// <summary>
        /// Submit a vote for the current round
        /// </summary>
        /// <param name="sessionName">The name of the session.</param>
        /// <param name="roundId">The id of the round</param>
        /// <param name="ballot">The vote ballot.</param>
        [HttpPost]
        [Route("sessions/{sessionName}/rounds/{roundId}/votes")]
        public void Vote([FromRoute]string sessionName, [FromRoute] int roundId, [FromBody] VoteBallot ballot)
        {
            _sessionsService.Vote(sessionName, ballot.ParticipantName,  roundId, ballot.Value);
        }

        /// <summary>
        /// End a round
        /// </summary>
        /// <param name="sessionId">The session id</param>
        /// <param name="roundId">The round id</param>
        [HttpDelete]
        [Route("sessions/{sessionId}/rounds/{roundId}")]
        public void EndRound([FromRoute] Guid sessionId, [FromRoute] int roundId)
        {
            _sessionsService.EndRound(sessionId, roundId);
        }

        /// <summary>
        /// End the session.
        /// </summary>
        /// <param name="sessionId">The session id</param>
        [HttpDelete]
        [Route("sessions/{sessionId}")]
        public void EndSession([FromRoute] Guid sessionId)
        {
            _sessionsService.EndSession(sessionId);
        }

    }
}
