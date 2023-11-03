
using Microsoft.AspNetCore.Mvc;
using system_rh_csharp.Context;
using system_rh_csharp.Models;

namespace system_rh_csharp.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class FuncionarioController : ControllerBase
    {
        private readonly RHContext _context;

        public FuncionarioController(RHContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            var funcionario = _context.Funcionarios.Find(id);

            if (funcionario == null)
                return NotFound();

            return Ok(funcionario);
        }

        [HttpPost]
        public IActionResult Criar(Funcionario funcionario)
        {
            _context.Funcionarios.Add(funcionario);
            var funcionarioLog = new FuncionarioLog(funcionario, TipoAcao.Inclusao, funcionario.Departamento, Guid.NewGuid().ToString());
            return CreatedAtAction(nameof(ObterPorId), new { id = funcionario.Id }, funcionario);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Funcionario funcionario)
        {
            var funcionarioBanco = _context.Funcionarios.Find(id);

            if (funcionarioBanco == null)
                return NotFound();

            funcionarioBanco.Nome = funcionario.Nome;
            funcionarioBanco.Endereco = funcionario.Endereco;
            _context.SaveChanges();

            var funcionarioLog = new FuncionarioLog(funcionarioBanco, TipoAcao.Atualizacao, funcionarioBanco.Departamento, Guid.NewGuid().ToString());

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var funcionarioBanco = _context.Funcionarios.Find(id);

            if (funcionarioBanco == null)
                return NotFound();
            _context.SaveChanges();

            var funcionarioLog = new FuncionarioLog(funcionarioBanco, TipoAcao.Remocao, funcionarioBanco.Departamento, Guid.NewGuid().ToString());
            return NoContent();
        }
    }
}