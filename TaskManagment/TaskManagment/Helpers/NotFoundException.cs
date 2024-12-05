using System.Globalization;

namespace TaskManagement.Helpers
{
    public class NotFoundException : Exception
    {
        public NotFoundException() : base() { }

        public NotFoundException(string message) : base(message) { }        
    }
}
