using System.ComponentModel.DataAnnotations;

namespace EmployeesManagement.Models;

public class Instrument
{
    public int Id { get; set; }

    [Required]
    [Display(Name = "Tipo de Instrumento")]
    public string TipoInstrumento { get; set; } = "";

    [Required]
    public string Instrumento { get; set; } = "";

    [Display(Name = "Usa Cordas")]
    public bool UsaCordas { get; set; }
}
