﻿using System.ComponentModel.DataAnnotations;

namespace WebApplication8.ViewModels;

public class LoginVM
{
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [DataType(DataType.Password)]
    public string Password { get; set; }


    public bool RememberMe { get; set; }
}
