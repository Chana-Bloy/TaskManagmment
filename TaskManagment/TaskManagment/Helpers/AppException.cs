using System.Globalization;

namespace TaskManagement.Helpers
{
    public class AppException : Exception
    {
        public AppException() : base() { }

        public AppException(string message) : base(message) { }        
    }
}
