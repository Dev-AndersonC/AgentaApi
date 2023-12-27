using AgentaApi.Context;
using AgentaApi.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AgentaApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ContatoController : ControllerBase
	{
		private readonly AgendaContext _context;
        public ContatoController(AgendaContext context)
        {
			_context = context;
        }
		[HttpGet("{id}")]
		public IActionResult GetById(int id)
		{
			var contato = _context.Contatos.Find(id);
			var msgError = $"O id:{id}, requisitado não existe ou não foi encontrado no banco de dados!";
			if (contato == null)
			{
				return NotFound(new
				{
					msgError
				} );
			}
			return Ok(contato);
		}
		[HttpGet("ObterContatoPorNome")]
		public IActionResult GetByName(string nome)
		{
			var contatos = _context.Contatos.Where(x => x.Nome.Contains(nome));
			return Ok(contatos);
		}

		[HttpPost]
        public IActionResult Create(Contato contato)
		{
			_context.Add(contato);
			_context.SaveChanges();
			return CreatedAtAction(nameof(GetById), new
			{id = contato.Id}, contato );
		}

		[HttpPut("{id}")]
		public IActionResult Update(int id, Contato contato)
		{
			var contatoBanco = _context.Contatos.Find(id);
			var msgError = $"O id:{id}, requisitado não existe ou não foi encontrado no banco de dados!";
			if (contatoBanco == null)
			{
				return NotFound(new
				{
					msgError
				} );
			}
			contatoBanco.Nome = contato.Nome;
			contatoBanco.Telefone = contato.Telefone;
			contatoBanco.Ativo = contato.Ativo;

			_context.Contatos.Update(contatoBanco);
			_context.SaveChanges();

			return Ok(contatoBanco);
		}
		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			var msgError = $"O id:{id}, requisitado não existe ou não foi encontrado no banco de dados!";
			var msgDelete = $"O id:{id}, requisitado foi deletado!";


			var contatoDeletar = _context.Contatos.Find(id);
			if (contatoDeletar == null)
			{
				return NotFound(msgError);
			}
			_context.Contatos.Remove(contatoDeletar);
			_context.SaveChanges();

			return NoContent(); 
		}
	}
	
}
