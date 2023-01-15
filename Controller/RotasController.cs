using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
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

            // Passa por todas as rotas.
            for (int i = 0; i < listaRotas.Count; i++)
            {               
                // Verifica se existe uma rota com a origem e o destino solicitado, se existir adiciona.
                if (listaRotas[i].Origem == origem && listaRotas[i].Destino == destino)
                {
                    listaConexoes.Add(listaRotas[i]);
                }
                else
                {
                    // Caso não exista, vamos montar as conexões da Rota (Exemplo: Rota inicial, rota  2 3 até a Rota Final).
                    if (listaRotas[i].Origem == origem)
                    {
                        SearchRotasDTO search = new SearchRotasDTO();
                        search.ListaConexoes.Add(listaRotas[i]);

                        // Primeira montagem da Rota.
                        rotadestino = listaRotas[i].Destino;
                        search.ListaRotas = listaRotas.Where(p => p.Origem == listaRotas[i].Destino).ToList();

                        bool continuar = true;
                        while (search.ListaRotas.Count > 0)
                        {
                            // Passa por todas as Rotas da Conexão (Há casos onde o destino pode ter 2 conexões diferente).
                            for (int a = 0; a < search.ListaRotas.Count; a++)
                            {
                                // Adiciona a Rota da Conexão a lista de conexões.
                                search.ListaConexoes.Add(search.ListaRotas[a]);                                   
                                rotadestino = rotadestino + " - " + search.ListaRotas[a].Destino;

                                // Verifica se há conexão de destino da conexão anterior.
                                var pesquisa = listaRotas.Where(A => A.Origem == search.ListaRotas[a].Destino).ToList();
                                if (pesquisa.Count > 0)
                                {
                                    // Há casos em que a conexão anterior tenha destinos diferentes.
                                    for (int e = 0; e < pesquisa.Count; e++)
                                    {
                                        // Como existe, vamos realimentar novamente a Lista de Rotas de Conexões.
                                        search.ListaRotas.Add(pesquisa[e]);
                                    }
                                }
                                else
                                {
                                    search.ListaRotas.Clear();
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
            }                  

            return Ok(listaConexoes);
        }


    }
}
