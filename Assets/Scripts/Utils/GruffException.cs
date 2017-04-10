using System;

namespace Utils
{
    public class GruffException : Exception
    {
        public GruffException()
        {
        }

        public GruffException(string message): base(message)
        {
        }

        public GruffException(string message, Exception inner): base(message, inner)
        {
        }
    }
}