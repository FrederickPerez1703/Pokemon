using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ULTIMO_INTENTO_POKEMONES.Models;

namespace ULTIMO_INTENTO_POKEMONES.DTO
{
    public class PokemonDto
    {
        public int IdPokemon { get; set; }
        public string NombrePokemon { get; set; }
        public int TipoPokemon { get; set; }
        public string Ataque { get; set; }
        public int RegionPokemon { get; set; }
        [Display (Name = "Foto")]
        public IFormFile Foto { get; set; }
        [Display(Name = "Region")]
        public virtual RegionPokemon RegionPokemonNavigation { get; set; }
        [Display(Name = "Tipo")]
        public virtual TipoPokemon TipoPokemonNavigation { get; set; }
    }
}
