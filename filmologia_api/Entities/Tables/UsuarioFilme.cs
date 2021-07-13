using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace filmologia_api.Entities.Tables
{
    [Table("UsuarioFilme")]
    public class UsuarioFilme
    {
        [Key]
        public int IdFilme { get; set; }
        public int IdUsuario { get; set; }
        public int IdFilmeAPI { get; set; }
        public string Nome { get; set; }
        public string Sinopse { get; set; }
        public string Poster { get; set; }
        public DateTime Lancamento { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
