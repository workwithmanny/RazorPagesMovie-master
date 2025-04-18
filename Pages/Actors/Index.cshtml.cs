using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Data;
using RazorPagesMovie.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPagesMovie.Pages.Actors
{
    public class IndexModel : PageModel
    {
        private readonly RazorPagesMovieContext _context;

        public IndexModel(RazorPagesMovieContext context)
        {
            _context = context;
        }

        public IList<Actor> Actors { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public async Task OnGetAsync()
        {
            var actors = from a in _context.Actor
                         select a;

            if (!string.IsNullOrEmpty(SearchString))
            {
                actors = actors.Where(a => a.Name.Contains(SearchString));
            }

            Actors = await actors
                .Include(a => a.MovieActors)
                .ThenInclude(ma => ma.Movie)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}