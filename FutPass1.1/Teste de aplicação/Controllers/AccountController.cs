using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Teste_de_aplicação.Models;

namespace Teste_de_aplicação.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        /// <param name="returnURL"></param>
        /// <returns></returns>
        public ActionResult Login(string returnURL)
        {
            /*Recebee a url que o usuário tentou acessar*/
            ViewBag.ReturnUrl = returnURL;
            return View(new Users());
        }

        /// <param name="login"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Users login, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                using (UEFAEntities db = new UEFAEntities())
                {
                    var vLogin = db.Users.Where(p => p.Email.Equals(login.Email)).FirstOrDefault();
                    /*Verificar se a variavel vLogin está vazia. Isso pode ocorrer caso o usuário não existe. 
              Caso não exista ele vai cair na condição else.*/
                    if (vLogin != null)
                    {
                        /*Código abaixo verifica se o usuário que retornou na variavel tem está 
                         ativo. Caso não esteja cai direto no else*/
                        if (Equals(vLogin.UserStatus.StatusDesc, "Active"))
                        {
                            /*Código abaixo verifica se a senha digitada no site é igual a senha que está sendo retornada 
                             do banco. Caso não cai direto no else*/
                            if (Equals(vLogin.UserPassword, login.UserPassword))
                            {
                                FormsAuthentication.SetAuthCookie(vLogin.Email, false);
                                if (Url.IsLocalUrl(returnUrl)
                                && returnUrl.Length > 1
                                && returnUrl.StartsWith("/")
                                && !returnUrl.StartsWith("//")
                                && returnUrl.StartsWith("/\\"))
                                {
                                    return Redirect(returnUrl);
                                }
                                /*armazenar o nome do usuário na Sssion*/
                                Session["Name"] = vLogin.FirstName;
                                /*armazenar role do usuário*/
                                Session["Role"] = vLogin.RoleType.RoleName;
                                /*LastName*/
                                Session["LastName"] = vLogin.LastName;
                                /*retorna para a tela inicial do Home*/
                                return RedirectToAction("Index", "Home");
                            }
                            /*Else responsável da validação da senha*/
                            else
                            {
                                /*Escreve na tela a mensagem de erro informada*/
                                ModelState.AddModelError("", "Wrong Password!!!");
                                /*Retorna a tela de login*/
                                return View(new Users());
                            }
                        }
                        /*Else responsável por verificar se o usuário está ativo*/
                        else
                        {
                            /*Escreve na tela a mensagem de erro informada*/
                            ModelState.AddModelError("", "Access Deined!!!");
                            /*Retorna a tela de login*/
                            return View(new Users());
                        }
                    }
                    /*Else responsável por verificar se o usuário existe*/
                    else
                    {
                        /*Escreve na tela a mensagem de erro informada*/
                        ModelState.AddModelError("", "Wrong E-mail!!!");
                        /*Retorna a tela de login*/
                        return View(new Users());
                    }
                }
            }
            /*Caso os campos não estejam de acordo com a solicitação retorna a tela de login com as mensagem dos campos*/
            return View(login);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon(); // Limpar sessions ao sair da aplicação 
            return RedirectToAction("Login", "Account"); //retornar para a tela de login
        }
    }
}
