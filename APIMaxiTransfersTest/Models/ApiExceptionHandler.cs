using System.Collections.Generic;

namespace APIMaxiTransfersTest.Models
{
    public class ApiExceptionHandler
    {
        public ApiExceptionHandler(string errorMessage)
        {
            this.Title = errorMessage;
            this.Errors = new Dictionary<string, string[]>();
        }

        public string Title { get; private set; }
        public int Status { get; set; }
        public IDictionary<string, string[]> Errors { get; set; }
    }
}