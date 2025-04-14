namespace MVCProject.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

public class AppUser : IdentityUser
{
    [PersonalData]
    public string FullName { get; set; } = null!;

}
