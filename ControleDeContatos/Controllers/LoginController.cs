﻿using ControleDeContatos.Helper;
using ControleDeContatos.Models;
using ControleDeContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeContatos.Controllers
{
    public class LoginController : Controller
    {

        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISessao _sessao;
        private readonly IEmail _email;

        public LoginController(IUsuarioRepositorio usuarioRepositorio, 
                               ISessao sessao,
                               IEmail email)
        {

            _usuarioRepositorio = usuarioRepositorio;
            _sessao = sessao;
            _email = email;
        }


        public IActionResult Index()
        {
            //Se o usuario estiver logado, direcionar para a Home
            if (_sessao.BuscarSessaoDoUsuario() != null) return RedirectToAction("Index", "Home");
            return View();
        }
        public IActionResult RedefinirSenha()
        {
            return View();
        }

        public IActionResult Sair()         
        { 
            _sessao.RemoverSessaoDoUsuario();
            return RedirectToAction("Index", "Login"); 
        }

        [HttpPost]
        public IActionResult Entrar(LoginModel loginModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuarioModel = _usuarioRepositorio.BuscarPorLogin(loginModel.Login);

                    if (usuarioModel != null)
                    {
                        if (usuarioModel.ValidaSenha(loginModel.Senha))
                        {
                            _sessao.CriarSessaoDoUsuario(usuarioModel);
                            return RedirectToAction("Index", "Home");
                        }
                        TempData["MensagemErro"] = $"Senha do Usuário é Inválida";
                    }
                    TempData["MensagemErro"] = $"Usuário e/ou Senha Inválido(s)";

                }
               
                return View("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos apagar seu Usuário apagado, mais detalhes no erro: {erro.Message}";
                return RedirectToAction("Index");
            }            
        }

        [HttpPost]
        public IActionResult EnviarLinkParaRedefinirSenha(RedefinirSenhaModel redefinirSenhaModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuarioModel = _usuarioRepositorio.BuscarPorEmailLogin(redefinirSenhaModel.Email, redefinirSenhaModel.Login);

                    if (usuarioModel != null)
                    {
                        string novaSenha = usuarioModel.GerarNovaSenha();
                        string mensagem = $"Sua nova senha é: {novaSenha}";
                        bool emailEnviado = _email.Enviar(usuarioModel.Email, "Sistema de Controle Jurídico - Nova Senha", mensagem);
                        if (emailEnviado)
                        {
                            _usuarioRepositorio.Atualizar(usuarioModel);
                            TempData["MensagemSucesso"] = $"Enviamos para o seu email cadastrado uma nova senha.";
                        }
                        else
                        {
                            TempData["MensagemErro"] = $"Não conseguimos enviar email. Tente novamente";
                        }                       
                        return RedirectToAction("Index", "Login");
                    }
                    TempData["MensagemErro"] = $"Não conseguimos redefinir sua senha. Dados informados inválidos";
                }

                return View("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos redefinir sua senha, tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
