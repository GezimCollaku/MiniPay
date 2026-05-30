using Microsoft.AspNetCore.Mvc;
using MiniPay.API.Data;
using MiniPay.API.Domain;
using System.Linq;
using System.Collections.Generic;
namespace MiniPay.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentProviderController : ControllerBase
{
    private readonly MiniPayDbContext _context;

    public PaymentProviderController(MiniPayDbContext context)
    {
        _context = context;
    }

    
    [HttpGet]
    public ActionResult<IEnumerable<PaymentProvider>> Get()
    {
        return Ok(_context.PaymentProviders.ToList());
    }

    
    [HttpGet("{id}")]
    public ActionResult<PaymentProvider> GetById(int id)
    {
        var provider = _context.PaymentProviders.FirstOrDefault(x => x.Id == id);

        if (provider == null)
            return NotFound(new { message = "Ofruesi nuk u gjet." });

        return Ok(provider);
    }

    
    [HttpPost]
    public ActionResult Create(PaymentProvider provider)
    {
         _context.PaymentProviders.Add(provider);
        _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = provider.Id }, provider);
    }

    
    [HttpPut("{id}")]
    public ActionResult Update(int id, PaymentProvider provider)
    {
        var existingProvider = _context.PaymentProviders.FirstOrDefault(x => x.Id == id);
        if (existingProvider == null)
             return NotFound(new { message = "Ofruesi që po tentoni të ndryshoni nuk ekziston." });

        
        existingProvider.Name = provider.Name;
        existingProvider.EndpointUrl = provider.EndpointUrl;
        existingProvider.Currency = provider.Currency;
        existingProvider.Description = provider.Description;
        existingProvider.IsActive = provider.IsActive; 

        _context.SaveChanges();
        return Ok(existingProvider);
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var provider = _context.PaymentProviders.FirstOrDefault(x => x.Id == id);

        if (provider == null)
            return NotFound(new { message = "Ofruesi nuk ekziston." });

        _context.PaymentProviders.Remove(provider);
         _context.SaveChanges();

        return Ok(new { message = "Ofruesi u fshi me sukses." });
    }
}