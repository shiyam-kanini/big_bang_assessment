using System.IdentityModel.Tokens.Jwt;

namespace BigBang_Assessment_26_5_23_.Model_Request_Response_
{
    public class Commonresponse
    {
        public bool status { get; set; }
        public string? message { get; set; }
        public string? token { get; set; }
    }
}
