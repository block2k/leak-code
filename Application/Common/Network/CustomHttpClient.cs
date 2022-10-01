using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Network
{
    public class CustomHttpClient : HttpClient
    {
        private WebProxy? _proxy { get; set; }

        public CustomHttpClient(WebProxy? proxy = null) : base()
        {
            if (_proxy != null )
            {
                this._proxy = proxy;
            }
        }
    }
}
