namespace ProductsAPI.Controllers;

using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models;

[ApiController]
[Route("[controller]")]
public class ProductsController(
    IProductService productStore, 
    IProductValidator validator) : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Product>> GetAll() => Ok(productStore.GetAll());

    [HttpGet("{id}")]
    public ActionResult<Product> GetById(int id)
    {
        var product = productStore.GetById(id);

        return product == null
            ? NotFound()
            : Ok(product);
    }

    [HttpPost]
    public ActionResult<Product> Create(Product product)
    {
        var validationError = validator.Validate(product);
        if (validationError is not null)
            return BadRequest(validationError);

        var created = productStore.Add(product);

        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Product product)
    {
        var validationError = validator.Validate(product);
        if (validationError is not null)
            return BadRequest(validationError);

        var updated = productStore.Update(id, product);

        return updated
            ? NoContent()
            : NotFound();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var deleted = productStore.Delete(id);

        return deleted
            ? NoContent()
            : NotFound();
    }
}