using FastVoteMachine.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FastVoteMachine.Pages;

public class Vote : PageModel
{
    private readonly IVoteHandler _voteHandler;

    public int Id { get; private set; }
    public string Name { get; private set; }

    public Vote(IVoteHandler voteHandler)
    {
        _voteHandler = voteHandler;
    }

    public IActionResult OnGet(int id)
    {
        Id = id;
        try
        {
            Name = _voteHandler.GetName(id);
        }
        catch (Exception)
        {
            return RedirectToPage("/Index");
        }

        return Page();
    }
}