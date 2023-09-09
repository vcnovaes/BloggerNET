using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BloggerNET.Models;
using BloggerNET.Services;

namespace BloggerNET.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IContentService _contentProvider;

    public HomeController(ILogger<HomeController> logger, IContentService contentProvider)
    {
        _logger = logger;
        _contentProvider = contentProvider;
    }

    private async Task<IActionResult> GenerateSampleView( string viewDataLabel, ViewResult view, CancellationToken token)
    {
        var content = await _contentProvider.GetContent(token);
        view.ViewData[viewDataLabel] = content.FirstOrDefault();
        return view;
    }
    public async Task<IActionResult>  Index(CancellationToken token)
    {
        var view = View();
        var sampleViews = new List<Task<IActionResult>>()
        {
            GenerateSampleView("index0", view, token),
            GenerateSampleView( "index1", view, token),
            GenerateSampleView("index2", view, token)
        };

        await Task.WhenAll(sampleViews);
        return view;
    }

    
    public async Task<IActionResult> AboutMe(CancellationToken token)
    {
        var view = View();
        var rawContent = await _contentProvider.GetContent(token, 4);
        view.ViewData["Content"] = BaconIpsumService.ConvertListOfStringsToParagraph(rawContent);
        return view;
    }
    
    public async Task<ViewResult> Projects(CancellationToken token)
    {
        var view = View();
        var rawContent = await _contentProvider.GetContent(token, 4);
        view.ViewData["Content"] = BaconIpsumService.ConvertListOfStringsToParagraph(rawContent);
        return view;
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    
}