using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserService.Models.Entities
{
<<<<<<<< HEAD:ManagementSystem/Models/Entities/AddressEntity.cs
    public class AddressEntity
========
    public class RefreshToken
>>>>>>>> master:ManagementSystem/Models/Entities/RefreshToken.cs
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Token { get; set; }

        [Required]
        public DateTime CreatedDateTime { get; set; }

        public Guid UserId { get; set; }

        [Required]
<<<<<<<< HEAD:ManagementSystem/Models/Entities/AddressEntity.cs
        [MaxLength(35)]
        public string Street { get; set; }

        [Required]
        [MaxLength(10)]
        public string HouseNumber { get; set; }
========
        public UserEntity User { get; set; }
>>>>>>>> master:ManagementSystem/Models/Entities/RefreshToken.cs
    }
}
