using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuartzManagementCore;

namespace QuartzManagement.Web.Pages.MovieMgt
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public bool Loading { get; set; }

        [BindProperty]
        public Movie Movie { get; set; }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostSaveAsync(CancellationToken cancellationToken)
        {
            Loading = true;
            await Task.Delay(TimeSpan.FromSeconds(10), cancellationToken);
            return Page();
        }
    }
}
