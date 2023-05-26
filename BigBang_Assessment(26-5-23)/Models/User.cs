using System.ComponentModel.DataAnnotations;

namespace BigBang_Assessment_26_5_23_.Models
{
    public class User
    {
        [Key]
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public string? UserDOB { get; set;}
        public string? UserPasswordHash { get; set; }
        public string? UserPasswordSalt { get; set; }
    }
}
