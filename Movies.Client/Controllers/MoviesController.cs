using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Movies.Client.Models;
using Movies.Client.Services;

namespace Movies.Client.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieService _movieSvc;

        public MoviesController(IMovieService movieSvc)
        {
            _movieSvc = movieSvc;
        }

        // GET: Movies
        public async Task<IActionResult> Index()
        {
            return View(await _movieSvc.GetMovies());
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _movieSvc.GetMovie(id);

            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Genre,Rating,ReleaseDate,ImageUrl,Owner")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                await _movieSvc.CreateMovie(movie);

                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _movieSvc.GetMovie(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Genre,Rating,ReleaseDate,ImageUrl,Owner")] Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _movieSvc.UpdateMovie(movie);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (! await MovieExists(movie.Id))
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
            return View(movie);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _movieSvc.GetMovie(id);

            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _movieSvc.DeleteMovie(id);

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> MovieExists(int id)
        {
            return (await _movieSvc.GetMovies()).Any(e => e.Id == id);
        }
    }
}
