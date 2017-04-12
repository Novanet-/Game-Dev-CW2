using System;

namespace Utils
{
    public class GruffException : Exception
    {
        #region Public Constructors

        public GruffException()
        {
        }

        public GruffException(string message): base(message)
        {
        }

        public GruffException(string message, Exception inner): base(message, inner)
        {
        }

        #endregion Public Constructors
    }
}