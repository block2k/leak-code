using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Exceptions
{
    public class AzureUploadException : Exception
    {
        public AzureUploadException(): base() { }
        public AzureUploadException(string messages) : base(messages)
        {
        }
    }
}
