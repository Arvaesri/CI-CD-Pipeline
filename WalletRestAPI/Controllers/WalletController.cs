using WalletRestAPI.Models;
using WalletRestAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace WalletRestAPI.Controllers;

[ApiController]
[Route("[controller]")] // Or just [controller]
public class WalletController : ControllerBase
{
    private readonly ILogger<WalletController> _logger;

    public WalletController(ILogger<WalletController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public ActionResult<List<Wallet<Dollar>>> GetAll() =>
        WalletService.GetAll();


    [HttpGet("{id}")]
    public ActionResult<Wallet<Dollar>> Get(int id)
    {
        var wallet = WalletService.Get(id);

        if(wallet == null)
        return NotFound();

        return wallet;
    }

    [HttpPost]
    public IActionResult Create(Wallet<Dollar> wallet)
    {            
        WalletService.Add(wallet);
        return CreatedAtAction(nameof(Get), new { id = wallet.Id }, wallet);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Wallet<Dollar> wallet)
    {
        if (id != wallet.Id)
            return BadRequest();
            
        var existingWallet = WalletService.Get(id);
        if(existingWallet is null)
            return NotFound();
    
        WalletService.Update(wallet);           
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var wallet = WalletService.Get(id);
    
        if (wallet is null)
            return NotFound();
        
        WalletService.Delete(id);
    
        return NoContent();
}
}
