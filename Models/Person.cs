using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelloApi4.Models;

public class Person
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Nombre { get; set; } = null!;

    [Required, MaxLength(100)]
    public string Apellido { get; set; } = null!;

    [Range(0, 150)]
    public int Edad { get; set; }
}
