using System.ComponentModel.DataAnnotations;

namespace ManagementSystem.Entities
{
    public class ChangePassword
    {
        [Required, DataType(DataType.Password), Display(Name = "Current Password")]
        public string CurrentPassword { get; set; }

        [Required, DataType(DataType.Password), Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [Required, DataType(DataType.Password), Display(Name = "Confirm new Password")]
        [Compare("NewPassword")]
        public string ConfirmNewPassword { get; set; }
    }
}
