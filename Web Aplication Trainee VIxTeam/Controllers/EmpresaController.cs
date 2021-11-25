using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Web_Aplication_Trainee_VIxTeam.Models;


namespace Web_Aplication_Trainee_VIxTeam.Controllers
{
    public class EmpresaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Create([Bind("CodigoEmpresa,NomeEmpresa,NomeFantasiaEmpresa,CNPJ")] EmpresaModel empresaModel){

            return View("~/Views/Home/Index.cshtml");
        }
    }
}
