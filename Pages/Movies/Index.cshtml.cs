using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Data;
using RazorPagesMovie.Models;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPagesMovie.Pages.Movies
{
    public class IndexModel : PageModel
    {
        private readonly RazorPagesMovieContext _context;

        public IndexModel(RazorPagesMovieContext context)
        {
            _context = context;
        }

        public IList<Movie> Movie { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public async Task OnGetAsync()
        {
            var movies = from m in _context.Movie
                         select m;

            if (!string.IsNullOrEmpty(SearchString))
            {
                movies = movies.Where(m =>
                    m.Title.Contains(SearchString) ||
                    m.Director.Contains(SearchString) ||
                    m.Genre.Contains(SearchString) ||
                    m.ReleaseCountry.Contains(SearchString) ||
                    m.Cast.Any(c => c.Name.Contains(SearchString)));
            }

            Movie = await movies
                .Include(m => m.Cast) // Include Cast data
                .AsNoTracking()
                .ToListAsync();
        }
    }
}