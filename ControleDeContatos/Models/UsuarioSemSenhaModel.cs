using ControleDeContatos.Enums;
using System.ComponentModel.DataAnnotations;

namespace ControleDeContatos.Models
{
    public class UsuarioSemSenhaModel
    {

            public int Id { get; set; }
            [Required(ErrorMessage = "Nome do usuário é obrigatório")]
            public string Nome { get; set; }

            [Required(ErrorMessage = "Login do usuário é obrigatório")]
            public string Login { get; set; }

            [Required(ErrorMessage = "Email do usuário é obrigatório")]
            [EmailAddress(ErrorMessage = "Email do usuário é obrigatório")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Perfil do usuário é obrigatório")]
            public PerfilEnum? Perfil { get; set; }


    }
}
