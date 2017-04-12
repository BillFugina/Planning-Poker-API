using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using PlanningPoker.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanningPoker.Services
{
    public class SnsService : ISnsService
    {

        private readonly IAmazonSimpleNotificationService _snsService;

        public SnsService(IAmazonSimpleNotificationService snsService)
        {
            _snsService = snsService;
        }

        public async Task<CreateTopicResponse> CreateSession(string sessionName)
        {
            var createTopicRequest = new CreateTopicRequest(sessionName);
            var response = await _snsService.CreateTopicAsync(createTopicRequest);
            return response;
        }


        public async Task<DeleteTopicResponse> DeleteSession(string sessionName)
        {
            var deleteTopicRequest = new DeleteTopicRequest(sessionName);
            var response = await _snsService.DeleteTopicAsync(deleteTopicRequest);
            return response;
        }

        public async Task<PublishResponse> RegisterVote(string sessionArn, string message)
        {
            var response = await _snsService.PublishAsync(sessionArn, message);
            return response;
        }



    }
}
