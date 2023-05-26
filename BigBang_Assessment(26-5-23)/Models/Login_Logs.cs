using System.ComponentModel.DataAnnotations;

namespace BigBang_Assessment_26_5_23_.Models
{
    public class Login_Logs
    {
        [Key]
        public string? SessionId { get; set; }
        public string? LoginId { get; set; }
    }
}
