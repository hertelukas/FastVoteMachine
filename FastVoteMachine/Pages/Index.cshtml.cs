using System.ComponentModel.DataAnnotations;
using FastVoteMachine.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FastVoteMachine.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IVoteHandler _voteHandler;

    [Required]
    [StringLength(100)]
    [Display(Name = "Vote Name")]
    [BindProperty]
    public string Name { get; set; }
    public IndexModel(ILogger<IndexModel> logger, IVoteHandler voteHandler)
    {
        _logger = logger;
        _voteHandler = voteHandler;
    }

    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        return RedirectToPage("Vote", new { id = _voteHandler.CreateVoting(Name)}); 
    }
    
    
}