using Pomelo.EntityFrameworkCore.MySql.Metadata.Internal;

namespace RotasWebAPI.Models
{
    public class SearchRotasDTO
    {

        public List<RotasDTO> ListaConexoes { get; set; } = new List<RotasDTO>();
        public List<RotasDTO> ListaRotas { get; set; } = new List<RotasDTO>();

        public RotasDTO RotaDestino { get; set; }

    }
}
