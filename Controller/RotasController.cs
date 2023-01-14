using Microsoft.AspNetCore.Mvc;

namespace RepositoryGeneric.Controller
{

    [ApiController]
    public class RotasController : ControllerBase
    {
        [HttpGet("/Rotas/Get")]
        public async Task<IActionResult> get()
        {
            return Ok();
        }

        [HttpGet("/Rotas/List")]
        public async Task<IActionResult> getLista()
        {
            return Ok();
        }

        [HttpPost("/Rotas/Insert")]
        public async Task<IActionResult> insert()
        {
            context.Acompanhamentoclientes.Add(add);
            await context.SaveChangesAsync();
            return Ok(add);
        }

        [HttpPut("/Rotas/Update")]
        public async Task<IActionResult> update()
        {
            context.Acompanhamentoclientes.Update(add);
            await context.SaveChangesAsync();
            return Ok(add);
        }


    }
}
