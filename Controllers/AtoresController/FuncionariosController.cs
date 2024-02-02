using Microsoft.AspNetCore.Mvc;

namespace TRABALHO_VOLVO
{
    [Route("[controller]")]
    [ApiController]
    public class FuncionariosController : Controller
    {
        //Cadastra um novo Funcionario via Form
        [HttpPost]
        public IActionResult CadastrarFuncionario([FromForm] Funcionario mfuncionario)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                _context.Funcionarios.Add(mfuncionario);
                _context.SaveChanges();
                return Ok("Funcionario Cadastrado Com Sucesso.");
            }
        }

        //Joga todos os Funcionarios em uma lista e formata em uma table na view "ListaFuncionarios.cshtml"
        [HttpGet("TodosFuncionarios/byConcID/{id}")]
        public IActionResult GetTodosFuncionarios(int id)
        {
            List<Funcionario> ListaFuncionarios;
            using (var _context = new TrabalhoVolvoContext())
            {
                ListaFuncionarios = _context.Funcionarios.ToList();
            }
            return View("ListaFuncionarios", ListaFuncionarios);
        }

        //Retorna o Funcionario cujo id é = id fornecido
        [HttpGet("Get/byID/{id}")]
        public IActionResult Get(int id)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.Funcionarios.FirstOrDefault(t => t.CodFuncionario == id);
                if(item == null)
                {
                    return NotFound();
                }
                return new ObjectResult(item);
            }
        }

        [HttpGet("Get/byDoc/{doc}")]
        public IActionResult GetByCpf(string doc)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.Funcionarios.FirstOrDefault(t => t.CpfFuncionario == doc);
                if(item == null)
                {
                    return NotFound();
                }
                return new ObjectResult(item);
            }
        }

        //Atualiza os dados do Funcionario cujo CPJ/CNPJ é = Doc fornecido
        [HttpPut("Update/byDoc/{Doc}")]
        public void Put(string Doc, [FromForm] Funcionario mfuncionario)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.Funcionarios.FirstOrDefault(t => t.CpfFuncionario == Doc);
                if(item == null)
                {
                    return;
                }
                // _context.Entry(item).CurrentValues.SetValues(mFuncionario);
                item.NomeFuncionario = mfuncionario.NomeFuncionario;
                item.NumeroContatoFuncionario = mfuncionario.NumeroContatoFuncionario;
                _context.SaveChanges();
            }
        }
    }
}