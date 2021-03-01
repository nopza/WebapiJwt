using System.ComponentModel.DataAnnotations;


namespace WebapiJwt.Models
{
    public class User2
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public string Name { get; set; }
    }
}
