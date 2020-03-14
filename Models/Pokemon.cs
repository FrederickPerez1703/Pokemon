using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ULTIMO_INTENTO_POKEMONES.Models
{
    public partial class Pokemon
    {
        public int IdPokemon { get; set; }
        public string NombrePokemon { get; set; }
        public int TipoPokemon { get; set; }

        public string Ataque { get; set; }

        public int RegionPokemon { get; set; }
        [Display(Name = "Foto")]
        public string Foto { get; set; }
        [Display(Name = "Region")]
        public virtual RegionPokemon RegionPokemonNavigation { get; set; }
        [Display(Name = "Tipo")]
        public virtual TipoPokemon TipoPokemonNavigation { get; set; }
    }
}
