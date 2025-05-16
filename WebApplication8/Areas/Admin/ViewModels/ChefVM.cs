using System.ComponentModel.DataAnnotations;

namespace WebApplication8.Areas.Admin.ViewModels;

public class ChefVM
{
    public int Id { get; set; }

    [Required, MinLength(3)]
    public string? FullName { get; set; }

    [Required, MinLength(5)]
    public string? Designation { get; set; }

    public string? ImgUrl { get; set; }

    public IFormFile? formFile { get; set; }
}
