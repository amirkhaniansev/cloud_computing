using InformationCenterUI.HttpClients;
using InformationCenterUI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace InformationCenterUI.Controllers
{
    public class FilmsController : Controller
    {
        readonly FilmClient client;

        public FilmsController(FilmClient client)
        {
            this.client = client;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddFilm()
        {
            return View(new Film());
        }

        [HttpPost]
        public async Task<IActionResult> Add(Film film)
        {
            await this.client.PostFilm(film);
            return View("Success");
        }

        [HttpGet]
        public async Task<IActionResult> GetFilms()
        {
            List<Film> films = await this.client.GetFilms();
            return View("GetFilms", films);
        }
        
        public IActionResult GetFilmByIdI()
        {
            return View(new Film());
        }

        [HttpGet]
        public async Task<IActionResult> GetFilmById(Film film)
        {
            film = await this.client.GetFilmById(film.Id);
            return View("ViewFilmById", film);
        }

        public IActionResult GetFilmsByCinemaI()
        {
            return View(new Film());
        }

        [HttpGet]
        public async Task<IActionResult> GetFilmsByCinema(Film film)
        {
            List<Film> films = await this.client.GetFilmsByCinema(film.Cinema);
            return View("GetFilms", films);        
        }

        public IActionResult DeleteFilmById()
        {
            return View(new Film());
        }

        [HttpPost]
        public async Task<IActionResult> DeleteFilmByIdI(Film film)
        {
            await this.client.DeleteFilmById(film.Id);
            return View("Success");
        }

        [HttpGet]
        public IActionResult AddTrailer(int id)
        {
            ViewBag.Id = id;
            return View("Trailer");
        }

        [HttpPost]
        public async Task<IActionResult> AddTrailer(int filmId, IFormFile trailerFile)
        {
            byte[] trailerFileContent;
            using (MemoryStream ms = new MemoryStream())
            {
                trailerFile.CopyTo(ms);
                trailerFileContent = ms.ToArray();
            }
            await this.client.AddTrailer(filmId, trailerFileContent);
            return View("Trailer");
        }
    }
}
