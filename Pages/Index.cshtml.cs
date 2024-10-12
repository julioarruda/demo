using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Xml;
using System.Xml.Linq;
using System.Linq;

namespace demo_octocat.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public string octoImage { get; private set; } 

    public void OnGet()
    {
        string url = "https://octodex.github.com/atom.xml";
        XDocument doc = XDocument.Load(url);
        var octoImages = doc.Descendants("entry")
                            .Select(entry => entry.Element("content")?.Value)
                            .Where(content => content != null)
                            .Select(content => content.Substring(content.IndexOf("<img src=") + 10, content.IndexOf("/>") - content.IndexOf("<img src=") - 11).Replace("\"", ""))
                            .ToList();

        Random rnd = new Random();
        octoImage = octoImages[rnd.Next(octoImages.Count)];
    }
}
