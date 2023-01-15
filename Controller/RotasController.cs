using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RotasWebAPI.Data;
using RotasWebAPI.Models;

namespace RepositoryGeneric.Controller
{

    [ApiController]
    public class RotasController : ControllerBase
    {
        private readonly DataContext context;
        public RotasController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet("/Rotas/Get")]
        public async Task<IActionResult> get()
        {
            var list = await context.Rotas.ToListAsync();
            return Ok(list);
        }

        [HttpPost("/Rotas/Insert")]
        public async Task<IActionResult> insert(RotasDTO add)
        {
            context.Rotas.Add(add);
            await context.SaveChangesAsync();
            return Ok(add);
        }

        [HttpPut("/Rotas/Update")]
        public async Task<IActionResult> update(RotasDTO add)
        {
            context.Rotas.Update(add);
            await context.SaveChangesAsync();
            return Ok(add);
        }

        [HttpDelete("/Rotas/Remove")]
        public async Task<IActionResult> delete(RotasDTO add)
        {
            context.Rotas.Remove(add);
            await context.SaveChangesAsync();
            return Ok(add);
        }

        [HttpPost("/Rotas/Search")]
        public async Task<IActionResult> search(string origem, string destino)
        {
            List<RotasDTO> listaConexoes = new List<RotasDTO>();
            IList<RotasDTO> listaRotas = await context.Rotas.ToListAsync();
            string rotadestino = "";

            for (int i = 0; i < listaRotas.Count; i++)
            {                
                if (listaRotas[i].Origem == origem && listaRotas[i].Destino == destino)
                {
                    listaConexoes.Add(listaRotas[i]);
                }
                else
                {
                    SearchRotasDTO search = new SearchRotasDTO();
                    search.ListaConexoes.Add(listaRotas[i]);
                    rotadestino = listaRotas[i].Origem + " - " + listaRotas[i].Destino;
                    search.ListaRotas = listaRotas.Where(p => p.Origem == listaRotas[i].Destino).ToList();

                    bool continuar = true;
                    while (continuar)
                    {
                        for (int a = 0; a < search.ListaRotas.Count; a++)
                        {
                            search.ListaConexoes.Add(search.ListaRotas[a]);
                            rotadestino = rotadestino + " - " + search.ListaRotas[a].Destino;
                            var pesquisa = listaRotas.Where(A => A.Origem == search.ListaRotas[a].Destino).ToList();
                            if (pesquisa.Count > 0)
                            {
                                for (int e = 0; e < pesquisa.Count; e++)
                                {
                                    rotadestino = rotadestino + " - " + pesquisa[e].Destino;
                                    search.ListaRotas.Add(pesquisa[e]);
                                }
                            }
                            else
                            {
                                continuar = false;
                            }
                        }
                    }

                    RotasDTO nova = new RotasDTO();
                    nova.Origem = origem;     
                    nova.Destino = rotadestino;
                    nova.Valor = search.ListaConexoes.Sum(q => q.Valor);
                    listaConexoes.Add(nova);
                }
            }


            return Ok(listaConexoes);
        }


    }
}
