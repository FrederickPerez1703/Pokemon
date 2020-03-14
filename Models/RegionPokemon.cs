using System;
using System.Collections.Generic;

namespace ULTIMO_INTENTO_POKEMONES.Models
{
    public partial class RegionPokemon
    {
        public RegionPokemon()
        {
            Pokemon = new HashSet<Pokemon>();
            TipoPokemon = new HashSet<TipoPokemon>();
        }

        public int IdRegion { get; set; }
        public string NombreRegion { get; set; }
        public string ColorRegion { get; set; }

        public virtual ICollection<Pokemon> Pokemon { get; set; }
        public virtual ICollection<TipoPokemon> TipoPokemon { get; set; }
    }
}
