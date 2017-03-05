using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PlanningPoker.Model;
using PlanningPoker.Services;

namespace PlanningPoker.Controllers
{
    [Route("api/startup")]
    public class SetupController : Controller
    {
        private readonly ISessionsService _sessionsService;

        public SetupController(
            ISessionsService sessionsService
            )
        {
            _sessionsService = sessionsService;
        }

        [HttpPost]
        [Route("sessions/{sessionName}/{masterName}")]
        public SessionId CreateSession([FromRoute]string sessionName, [FromRoute]string masterName)
        {
            var session = _sessionsService.CreateSession(sessionName, masterName);
            var result = new SessionId()
            {
                Id = session.Id,
                Name = session.Name
            };
            return result;
        }
    }
}
