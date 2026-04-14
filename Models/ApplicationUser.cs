using Microsoft.AspNetCore.Identity;

namespace ExamenLinguaMVC.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string Direccion { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string NombreCompleto { get; set; } = string.Empty;
        public string IdiomaPreferido { get; set; } = string.Empty;
    }
}
