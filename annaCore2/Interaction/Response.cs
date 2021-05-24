using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANNA.Interaction
{
    public class Response
    {
        public Guid ExtensionID
        {
            get;
        }

        private DateTime Time
        {
            get;
        }
        
        public string responseID
        {
            get;
        }
        
        public string response;

        // Public API Response Calls
        public Response(Extension extension, string aiResponse)
        {
            response = aiResponse;
            ExtensionID = extension.UUID;
            Time = DateTime.Now;
            responseID = ExtensionID.ToString().Substring(0, 3) + DateTime.UnixEpoch.Ticks.ToString().Substring(0, 7) + ExtensionID.ToString().Substring(4, 6) + '_' + extension.ExtName;
        }
        
        // Used for ANNA Internal Response Calls ONLY
        internal Response(string aiResponse)
        {
            response = aiResponse;
            ExtensionID = Program.guid();
            Time = DateTime.Now;
            responseID = "0000" + DateTime.UnixEpoch.Ticks.ToString().Substring(0, 7) + "000" + '_' + "AnnaBase";
        }
    }
}
