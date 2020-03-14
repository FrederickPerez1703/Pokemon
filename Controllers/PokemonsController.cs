using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ULTIMO_INTENTO_POKEMONES.DTO;
using ULTIMO_INTENTO_POKEMONES.Models;

namespace ULTIMO_INTENTO_POKEMONES.Controllers
{
    public class PokemonsController : Controller
    {
        private readonly pokemon_itlaContext _context;
        public IHostingEnvironment HostingEnvironment;
        public PokemonsController(pokemon_itlaContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            this.HostingEnvironment = hostingEnvironment;
        }

        // GET: Pokemons
        public async Task<IActionResult> Index()
        {
            var pokemon_itlaContext = _context.Pokemon.Include(p => p.RegionPokemonNavigation).Include(p => p.TipoPokemonNavigation);
            return View(await pokemon_itlaContext.ToListAsync());
        }

        // GET: Pokemons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pokemon = await _context.Pokemon
                .Include(p => p.RegionPokemonNavigation)
                .Include(p => p.TipoPokemonNavigation)
                .FirstOrDefaultAsync(m => m.IdPokemon == id);
            if (pokemon == null)
            {
                return NotFound();
            }

            return View(pokemon);
        }

        // GET: Pokemons/Create
        public IActionResult Create()
        {
            ViewData["RegionPokemon"] = new SelectList(_context.RegionPokemon, "IdRegion", "ColorRegion");
            ViewData["TipoPokemon"] = new SelectList(_context.TipoPokemon, "IdTipo", "NombreTipo");
            return View();
        }

        // POST: Pokemons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PokemonDto model)
        {
            var det = new Pokemon();
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (model.Foto != null)
                {
                    string folderPath = Path.Combine(HostingEnvironment.WebRootPath, "img");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Foto.FileName;
                    string filePath = Path.Combine(folderPath, uniqueFileName);

                    if (filePath != null) model.Foto.CopyTo(new FileStream(filePath, mode: FileMode.Create));
                }
                Pokemon detalle = new Pokemon { 
                
                    NombrePokemon = model.NombrePokemon,
                    TipoPokemon = model.TipoPokemon,
                    Foto = uniqueFileName,
                    Ataque = model.Ataque,
                    RegionPokemon = model.RegionPokemon

                };
                _context.Add(detalle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
           
            return View(model);
        }

        // GET: Pokemons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pokemon = await _context.Pokemon.FindAsync(id);
            if (pokemon == null)
            {
                return NotFound();
            }
            ViewData["RegionPokemon"] = new SelectList(_context.RegionPokemon, "IdRegion", "ColorRegion", pokemon.RegionPokemon);
            ViewData["TipoPokemon"] = new SelectList(_context.TipoPokemon, "IdTipo", "NombreTipo", pokemon.TipoPokemon);
            return View(pokemon);
        }

        // POST: Pokemons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPokemon,NombrePokemon,TipoPokemon,Ataque,RegionPokemon,Foto")] Pokemon pokemon )
        {
            if (id != pokemon.IdPokemon)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pokemon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PokemonExists(pokemon.IdPokemon))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["RegionPokemon"] = new SelectList(_context.RegionPokemon, "IdRegion", "ColorRegion", pokemon.RegionPokemon);
            ViewData["TipoPokemon"] = new SelectList(_context.TipoPokemon, "IdTipo", "NombreTipo", pokemon.TipoPokemon);
            return View(pokemon);
        }

        // GET: Pokemons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pokemon = await _context.Pokemon
                .Include(p => p.RegionPokemonNavigation)
                .Include(p => p.TipoPokemonNavigation)
                .FirstOrDefaultAsync(m => m.IdPokemon == id);
            if (pokemon == null)
            {
                return NotFound();
            }

            return View(pokemon);
        }

        // POST: Pokemons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pokemon = await _context.Pokemon.FindAsync(id);
            _context.Pokemon.Remove(pokemon);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PokemonExists(int id)
        {
            return _context.Pokemon.Any(e => e.IdPokemon == id);
        }
    }
}
