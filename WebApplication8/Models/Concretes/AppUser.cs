﻿

using Microsoft.AspNetCore.Identity;

namespace WebApplication8.Models.Concretes;

public class AppUser : IdentityUser
{
    public string? Name { get; set; }

    public string? Surname { get; set; }


}
