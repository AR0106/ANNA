using System;

namespace ANNA.Interaction
{
    public class Response
    {
        public string ExtensionID
        {
            get;
        }

        private DateTime Time
        {
            get => DateTime.Now;
        }

        public string responseID
        {
            get;
        }

        public dynamic response;

        // Public API Response Calls
        public Response(Extension extension, dynamic aiResponse)
        {
            response = aiResponse;
            ExtensionID = extension.ANEID;
            responseID = ExtensionID.ToString().Substring(0, 3) + DateTime.UnixEpoch.Ticks.ToString().Substring(0, 7) + ExtensionID.ToString().Substring(4, 6) + '_' + extension.ExtName;
        }

        // Used for ANNA Internal Response Calls ONLY
        internal Response(dynamic aiResponse)
        {
            response = aiResponse;
            ExtensionID = Program.baseANEID();
            responseID = "0000" + DateTime.UnixEpoch.Ticks.ToString().Substring(0, 7) + "000" + '_' + "AnnaBase";
        }
    }
}
