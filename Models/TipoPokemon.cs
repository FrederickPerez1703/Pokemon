using System;
using System.Collections.Generic;

namespace ULTIMO_INTENTO_POKEMONES.Models
{
    public partial class TipoPokemon
    {
        public TipoPokemon()
        {
            Pokemon = new HashSet<Pokemon>();
        }

        public int IdTipo { get; set; }
        public string NombreTipo { get; set; }
        public int RegionPokemon { get; set; }

        public virtual RegionPokemon RegionPokemonNavigation { get; set; }
        public virtual ICollection<Pokemon> Pokemon { get; set; }
    }
}
