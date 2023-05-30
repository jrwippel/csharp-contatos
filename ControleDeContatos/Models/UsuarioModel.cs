using ControleDeContatos.Enums;
using ControleDeContatos.Helper;
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

        [Required(ErrorMessage = "Email do usuário é obrigatório")]
        [EmailAddress(ErrorMessage = "Email do usuário é obrigatório")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Perfil do usuário é obrigatório")]
        public PerfilEnum Perfil { get; set; }
        [Required(ErrorMessage = "Senha do usuário é obrigatório")]
        public string Senha { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAtualizacao { get; set; }

        public bool ValidaSenha(string senha)
        {
            return Senha == senha.GerarHash();
        }
        public void SetSenhaHash()
        {
            Senha = Senha.GerarHash();
        }

        public string GerarNovaSenha()
        {
            string novaSenha = Guid.NewGuid().ToString().Substring(0,8);
            Senha = novaSenha.GerarHash();
            return novaSenha;
        }
    }
}
