using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace William_Santisteban_02_08_2016.ViewModels
{
    public class ReglamentoViewModel
    {
        [Required]
        [Display(Name = "Nombre del Reglamento")]
        public string NombreReglamento { get; set; }
        [Required]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }
        [Required]
        [Display(Name = "Estado")]
        public string Estado { get; set; }
        [Required]
        [Display(Name = "Fecha de Confección")]
        public DateTime FechaConfeccion { get; set; }
        [Required]
        [Display(Name = "Fecha de Vigencia")]
        public DateTime FechaVigencia { get; set; }
    }

    public enum Estado
    {
        Vigente,
        Anulado
    }
}