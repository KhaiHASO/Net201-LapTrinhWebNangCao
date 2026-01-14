using demo03.Models;
using Microsoft.AspNetCore.Mvc;

namespace demo03.Controllers;

public class ProductController : Controller
{
    private readonly CompanyContext _context;

    public ProductController(CompanyContext context)
    {
        _context = context;
    }

    // GET: /Product
    public IActionResult Index()
    {
        var products = _context.Products.ToList();
        return View(products);
    }

    // GET: /Product/Add
    public IActionResult Add()
    {
        return View();
    }

    // POST: /Product/Add
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Add(Product product)
    {
        if (!ModelState.IsValid)
        {
            return View(product);
        }

        _context.Products.Add(product);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    // GET: /Product/Edit/5
    public IActionResult Edit(int id)
    {
        var product = _context.Products.Find(id);
        if (product == null)
        {
            return NotFound();
        }
        return View(product);
    }

    // POST: /Product/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Product product)
    {
        if (!ModelState.IsValid)
        {
            return View(product);
        }

        _context.Products.Update(product);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    // GET: /Product/Delete/5
    public IActionResult Delete(int id)
    {
        var product = _context.Products.Find(id);
        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }

    // POST: /Product/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var product = _context.Products.Find(id);
        if (product == null)
        {
            return NotFound();
        }

        _context.Products.Remove(product);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
}

