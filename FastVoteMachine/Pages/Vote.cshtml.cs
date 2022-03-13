using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FastVoteMachine.Pages;

public class Vote : PageModel
{
    [BindProperty] public int Id { get; set; }

    public void OnGet(int id)
    {
        Id = id;
    }
}