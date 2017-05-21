﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PlanningPoker.Model;
using PlanningPoker.Model.Configuration;
using PlanningPoker.Services;
using PlanningPoker.Interfaces.Services;
using QRCoder;
using Microsoft.Extensions.Options;

namespace PlanningPoker.Controllers
{
    [Route("api/")]
    public class SessionController : Controller
    {
        private readonly ISessionsService _sessionsService;
        private readonly Client _clientConfiguration;

        public SessionController(
            ISessionsService sessionsService,
            IOptions<Model.Configuration.Client> clientOptions

            )
        {
            _sessionsService = sessionsService;
            _clientConfiguration = clientOptions.Value;
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
        public Session JoinSession([FromRoute]string sessionName, [FromBody]ParticipantApplication participant)
        {
            return _sessionsService.JoinSession(sessionName, participant.Name);
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
        /// Prepare a new round.
        /// </summary>
        /// <param name="sessionId">The id of the session where the round will be started</param>
        /// <returns>The id of the new round.</returns>
        [HttpGet]
        [Route("sessions/{sessionId}/rounds")]
        public Round PrepareRound([FromRoute] Guid sessionId)
        {
            var round = _sessionsService.PrepareRound(sessionId);
            return round;
        }

        [HttpGet]
        [Route("sessions/{sessionId}/rounds/{roundId}")]
        public Round BeginCountdown([FromRoute] Guid sessionId, [FromRoute] int roundId)
        {
            var round = _sessionsService.StartCountdown(sessionId, roundId);
            return round;
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
            _sessionsService.Vote(sessionName, ballot.ParticipantName, roundId, ballot.Value, true);
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

        [HttpGet]
        [Route("sessions/{sessionId}/qrCode")]
        public IActionResult GetQrCode([FromRoute] Guid sessionId)
        {
            var gen = new QRCodeGenerator();
            var data = gen.CreateQrCode($"{_clientConfiguration.Url}/", QRCodeGenerator.ECCLevel.Q);
            var qrcode = new BitmapByteQRCode(data);
            var byteArray = qrcode.GetGraphic(20);
            return File(byteArray, "image/bmp");
        }

    }
}
