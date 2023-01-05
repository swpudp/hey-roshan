using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuartzManagement.Core;

namespace QuartzManagement.Web.Pages.MovieMgt
{
    public class ListModel : PageModel
    {
        [BindProperty]
        public List<Movie> Movie { get; set; }

        private readonly ILogger<ListModel> _logger;

        public ListModel(ILogger<ListModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            _logger.LogInformation("moviemgt list");
            Movie = Enumerable.Range(0, 10).Select((x, idx) => new Movie { Genre = "M", Id = idx + 1, Price = idx * 10m / 3m, ReleaseDate = DateTime.Now.AddDays(idx), Title = "test" + idx }).ToList();
        }
    }
}
