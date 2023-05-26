namespace MVCIntroDemo.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using MVCIntroDemo.Core.Models.Product;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;

public class ProductController : Controller
{
    private readonly IConfiguration config;

    public ProductController(IConfiguration config)
    {
        this.config = config;
    }

    [ActionName("My-Products")]
    public IActionResult All(string keyword)
    {
        var desData = GetAll();

        if (keyword != null) 
        {
            var foundProduct = desData
                .Where(p => p.Name.ToLower()
                .Contains(keyword.ToLower()));

            return View(foundProduct);
        }


        return View(desData);
    }

    public IActionResult ById(int Id) 
    {
        var desData = GetAll();
        var product = desData.FirstOrDefault(x => x.Id == Id);

        if (product == null) 
        {
            return NotFound();
        }

        return View(product);
    }

    public IActionResult AllAsJSON() 
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        return Json(GetAll(), options);
    }

    public IActionResult AllAsText() 
    {
        StringBuilder sb = new StringBuilder();

        var products = GetAll();
        foreach (var product in products) 
        {
            sb
                .AppendLine($"Product {product.Id}: {product.Name} - {product.Price} lv.");
        }

        return Content(sb.ToString());
    }

    public IActionResult AllAsTextFile()
    {
        var products = GetAll();
        StringBuilder sb = new();

        foreach (var p in products)
        {
            sb.AppendLine($"Product {p.Id}: {p.Name} - {p.Price:f2}lv.");
        }
        Response.Headers.Add(HeaderNames.ContentDisposition, @"attachment; filename=products.txt");
        byte[] fileContents = Encoding.UTF8.GetBytes(sb.ToString().TrimEnd());
        string contentType = "text/plain";
        return File(fileContents, contentType);
    }

    private IEnumerable<ProductViewModel> GetAll() 
    {
        string dataPath = config.GetValue<string>("DataFiles:Products");
        string data = System.IO.File.ReadAllText(dataPath);
        var desData = JsonConvert.DeserializeObject<IEnumerable<ProductViewModel>>(data);

        return desData;
    }

}
