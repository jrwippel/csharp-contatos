using ControleDeContatos.Enums;
using System.ComponentModel.DataAnnotations;

namespace ControleDeContatos.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Nome do usuário é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Login do usuário é obrigatório")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Email do contato é obrigatório")]
        [EmailAddress(ErrorMessage = "Email do contato é obrigatório")]
        public string Email { get; set; }
        public PerfilEnum Perfil { get; set; }

        [Required(ErrorMessage = "Senha do usuário é obrigatório")]
        public string Senha { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAtualizacao { get; set; }
    }
}
