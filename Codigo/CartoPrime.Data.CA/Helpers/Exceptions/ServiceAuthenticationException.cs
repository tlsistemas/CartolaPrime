using System;
using System.Collections.Generic;
using System.Text;

namespace CartoPrime.Data.CA.Helpers.Exceptions
{
    public class ServiceAuthenticationException : Exception
    {
        public string Content { get; }

        public ServiceAuthenticationException()
        {
        }

        public ServiceAuthenticationException(string content)
        {
            Content = content;
        }
    }
}