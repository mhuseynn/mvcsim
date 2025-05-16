using WebApplication8.Models.Abstracts;

namespace WebApplication8.Models.Concretes;

public class Chef : BaseEntity
{
    public string? FullName { get; set; }

    public string? Designation { get; set; }

    public string? ImgUrl { get; set; }

}
