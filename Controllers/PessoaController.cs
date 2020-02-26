using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using SelecaoStefanini.DAO;
using SelecaoStefanini.Entities;

namespace SelecaoStefanini.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaController : ControllerBase
    {
        PessoaDAO pessoaDao = new PessoaDAO();

        // GET: api/Pessoa
        [HttpGet]
        public async Task<ActionResult<List<Pessoa>>> GetAll()
        {
            var pessoas = await pessoaDao.FindAll();
            Console.WriteLine(pessoas);
            return Ok(pessoas);
        }

        // GET: api/Pessoa/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pessoa>> Get(string id)
        {
            var pessoa = await pessoaDao.FindById(id);
            return Ok(pessoa);
        }

        // GET: api/Pessoa/5
        [HttpPost("{cpf}")]
        public async Task<ActionResult<Pessoa>> FindByCpf(string cpf)
        {
            return Ok(await pessoaDao.FindByCpf(cpf));
        }

        // POST: api/Pessoa
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Pessoa p)
        {

            if (!Validator.IsCpf(p.CPF))
                return BadRequest("o CPF informado não é válido.");

            if (!string.IsNullOrEmpty(p.Email))
            {
                if (!Validator.IsValidEmail(p.Email))
                    return BadRequest("o E-mail informado não é válido.");
            }

            Pessoa pessoaExiste = await pessoaDao.FindByCpf(p.CPF);

            if (pessoaExiste != null)
                return BadRequest(new { message = "CPF já cadastrado" });

            Pessoa pessoa = await pessoaDao.Create(p);

            return Ok(pessoa);

        }

        // PUT: api/Pessoa/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Pessoa>> Put(string id, [FromBody] Pessoa p)
        {
            try
            {
                if (!string.IsNullOrEmpty(p.Email))
                {
                    if (!Validator.IsValidEmail(p.Email))
                        return BadRequest("o E-mail informado não é válido.");
                }

                var cpfPessoa = await pessoaDao.FindById(id);
                p.Id = id;
                p.CPF = cpfPessoa.CPF;
                Pessoa pessoa = await pessoaDao.Update(p);
                return Ok(pessoa);
            }
            catch
            {
                return BadRequest("Ocorreu um erro ao tentar atualizar, verifique os dados informados.");
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            pessoaDao.delete(id);
        }
    }
}
