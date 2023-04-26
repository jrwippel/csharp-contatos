using System.ComponentModel.DataAnnotations;

namespace ControleDeContatos.Models
{
    public class ContatoModel
    {        
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome do contato é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Email do contato é obrigatório")]
        [EmailAddress(ErrorMessage = "Email do contato é obrigatório")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Telefone do contato é obrigatório")]
        [Phone(ErrorMessage = "Telefone do contato Inválido")]
        public string Telefone { get; set; }
    }
}
