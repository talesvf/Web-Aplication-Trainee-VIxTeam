using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web_Aplication_Trainee_VIxTeam.Data;
using Web_Aplication_Trainee_VIxTeam.Models;
using Web_Aplication_Trainee_VIxTeam.Business;

namespace Web_Aplication_Trainee_VIxTeam.Controllers
{
    public class PessoaModelsController : Controller
    {
        private readonly Web_Aplication_Trainee_VIxTeamContext _context;

        public PessoaModelsController(Web_Aplication_Trainee_VIxTeamContext context)
        {
            _context = context;
        }

        // GET: PessoaModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.PessoaModel.ToListAsync());
        }

        // GET: PessoaModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoaModel = await _context.PessoaModel
                .FirstOrDefaultAsync(m => m.CodigoPessoa == id);
            if (pessoaModel == null)
            {
                return NotFound();
            }

            return View(pessoaModel);
        }

        // GET: PessoaModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PessoaModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodigoPessoa,NomePessoa,Email,DataNascimento,QtdFilhos,Salario")] PessoaModel pessoaModel)
        {
            pessoaModel.Situacao = true;
            if(!PessoaBusiness.VerificaEmailAoCriar(_context.PessoaModel.Any(x => x.Email == pessoaModel.Email)))
            {
                ModelState.AddModelError("Regra de Negócio", "O Email inserido já se encontra cadastrado por outra Pessoa.");
                return View();
            }
            if (!PessoaBusiness.VerificaDataNacimento(pessoaModel))
            {
                ModelState.AddModelError("Regra de Negócio", "A Data de Nascimento deve ser superior a 01/01/1990.");
                return View();
            }
            if (!PessoaBusiness.VerificaQuantidadeDeFilhos(pessoaModel))
            {
                ModelState.AddModelError("Regra de Negócio", "A Quantidade de Filhos deve ser maior ou igual a zero");
                return View();
            }
            if(PessoaBusiness.VerificaSalario(pessoaModel) == -1)
            {
                ModelState.AddModelError("Regra de Negócio", "O salário deve ser superior a R$1200,00.");
                return View();
            }
            if (PessoaBusiness.VerificaSalario(pessoaModel) == 1)
            {
                ModelState.AddModelError("Regra de Negócio", "O salário deve ser inferior a R$13000,00.");
                return View();
            }
            _context.Add(pessoaModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        // GET: PessoaModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoaModel = await _context.PessoaModel.FindAsync(id);
            if (pessoaModel == null)
            {
                return NotFound();
            }
            return View(pessoaModel);
        }

        // POST: PessoaModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CodigoPessoa,NomePessoa,Email,DataNascimento,QtdFilhos,Salario,Situacao")] PessoaModel pessoaModel)
        {
            PessoaModel pessoaBD = _context.PessoaModel.Single(x => x.CodigoPessoa == id);

            if (id != pessoaModel.CodigoPessoa)
            {
                return NotFound();
            }
            if (PessoaBusiness.VerificaSituacaoPessoaEditar(pessoaModel, pessoaBD) == 1)
            {
                ModelState.AddModelError("Regra de Negócio", "Não é possível editar uma Pessoa com a Situação 'Inativa'.");
                return View();
            }
            if (PessoaBusiness.VerificaSituacaoPessoaEditar(pessoaModel, pessoaBD) == 2)
            {
                ModelState.AddModelError("Regra de Negócio", "Esta Pessoa possui a situação 'Inativa', mude-a antes de alterar qualuer outra informação.");
                return View();
            }
            if (!PessoaBusiness.VerificaEmailAoEditar(pessoaModel, pessoaBD, _context.PessoaModel.Any(x => x.Email == pessoaModel.Email)))
            {
                ModelState.AddModelError("Regra de Negócio", "O Email inserido já se encontra cadastrado por outra pessoa");
                return View();
            }
            if (!PessoaBusiness.VerificaDataNacimento(pessoaModel))
            {
                ModelState.AddModelError("Regra de Negócio", "A Data de Nascimento deve ser superior a 01/01/1990.");
                return View();
            }
            if (!PessoaBusiness.VerificaQuantidadeDeFilhos(pessoaModel))
            {
                ModelState.AddModelError("Regra de Negócio", "A Quantidade de Filhos deve ser maior ou igual a zero");
                return View();
            }
            if (PessoaBusiness.VerificaSalario(pessoaModel) == -1)
            {
                ModelState.AddModelError("Regra de Negócio", "O salário não pode ser inferior a R$1200,00.");
                return View();
            }
            if (PessoaBusiness.VerificaSalario(pessoaModel) == 1)
            {
                ModelState.AddModelError("Regra de Negócio", "O salário não pode ser superior a R$13000,00.");
                return View();
            }
            pessoaBD.NomePessoa = pessoaModel.NomePessoa;
            pessoaBD.Email = pessoaModel.Email;
            pessoaBD.DataNascimento = pessoaModel.DataNascimento;
            pessoaBD.QtdFilhos = pessoaModel.QtdFilhos;
            pessoaBD.Salario = pessoaModel.Salario;
            pessoaBD.Situacao = pessoaModel.Situacao;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pessoaBD);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PessoaModelExists(pessoaModel.CodigoPessoa))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(pessoaModel);
        }

        // GET: PessoaModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoaModel = await _context.PessoaModel
                .FirstOrDefaultAsync(m => m.CodigoPessoa == id);
            if (pessoaModel == null)
            {
                return NotFound();
            }

            return View(pessoaModel);
        }

        // POST: PessoaModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pessoaModel = await _context.PessoaModel.FindAsync(id);
            //Regra de Negócio: Não é possível deletar uma Pessoa com a Situação 'Ativa'.
            if (PessoaBusiness.VerificaSituacaoPessoaDeletar(pessoaModel))
            {
                ModelState.AddModelError("Regra de Negócio", "Não é possível deletar uma Pessoa com a Situação 'Ativa'.");
                return View(pessoaModel);
            }
            _context.PessoaModel.Remove(pessoaModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> MudaSituacao(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var pessoaModel = await _context.PessoaModel.FindAsync(id);

            if (pessoaModel == null)
            {
                return NotFound();
            }
            if (pessoaModel.Situacao == true)
            {
                pessoaModel.Situacao = false;
            }
            else if (pessoaModel.Situacao == false)
            {
                pessoaModel.Situacao = true;
            }
            return await ConfirmaSituacao(pessoaModel);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmaSituacao(PessoaModel pessoaModel)
        {
            _context.Update(pessoaModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PessoaModelExists(int id)
        {
            return _context.PessoaModel.Any(e => e.CodigoPessoa == id);
        }
    }
}
