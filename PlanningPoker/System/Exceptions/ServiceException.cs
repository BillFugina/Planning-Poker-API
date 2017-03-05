using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanningPoker.System.Exceptions
{
    public class ServiceException : Exception
    {
        public ServiceException()
        {
            
        }

        public ServiceException(string message) : base(message)
        {
        }

        public ServiceException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

    public class SessionClashException : ServiceException
    {
        public SessionClashException() : base("Session already exists.")
        {
            
        }
    }

    public class SessionMissingException : ServiceException
    {
        public SessionMissingException() : base("Session could not be found.")
        {
            
        }
    }

    public class ParticipantClashException : ServiceException
    {
        public ParticipantClashException() : base("Participant already in session.")
        {
            
        }
    }
}
