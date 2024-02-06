using Microsoft.AspNetCore.Mvc;

namespace TRABALHO_VOLVO
{
    [Route("[controller]")]
    [ApiController]
    public class ModelosCaminhoesController : Controller
    {
        [HttpPost("Cadastrar")]
        public IActionResult PostModelosCaminhao([FromForm] ModelosCaminhao modelosCaminhao)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                try
                {
                    modelosCaminhao.CodModelo = 0;
                    modelosCaminhao.ModelosAtivo  = true;
                    ValidationHelper.IsValidDouble($"{modelosCaminhao.ValorModeloCaminhao}", "Valor do modelo invalido.");
                    _context.ModelosCaminhoes.Add(modelosCaminhao);
                    _context.SaveChanges();
                    return Ok("O modelo foi registrado com sucesso.");
                }
                catch(Exception)
                {
                    throw;
                }
            }
        }

        [HttpGet("Listar")]
        public List<ModelosCaminhao> GetTodosModelosCaminhoes()
        {
            try
            {
                using (var _context = new TrabalhoVolvoContext())
                {
                    return _context.ModelosCaminhoes.ToList();
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        [HttpGet("Buscar/{Codigo}")]
        public IActionResult GetModelosCaminhao(int Codigo)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.ModelosCaminhoes.FirstOrDefault(t => t.CodModelo == Codigo);
                if (item == null)
                {
                    return NotFound();
                }
                return new ObjectResult(item);
            }
        }

        [HttpPut("Atualizar/{Codigo}")]
        public IActionResult PutModelosCaminhao(int Codigo, [FromForm] ModelosCaminhao modelosCaminhao)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.ModelosCaminhoes.FirstOrDefault(t => t.CodModelo == Codigo);
                if (item == null)
                {
                    throw new FKNotFoundException("Nenhum modelo registrado possui esse codigo.");
                }
                try
                {
                    ValidationHelper.IsValidDouble($"{modelosCaminhao.ValorModeloCaminhao}", "Valor do modelo invalido.");
                    item.NomeModelo = modelosCaminhao.NomeModelo;
                    item.ValorModeloCaminhao = modelosCaminhao.ValorModeloCaminhao;
                    _context.SaveChanges();
                    return Ok();
                }
                catch(Exception)
                {
                    throw;
                }
            }
        }
        
        [HttpPut("Desativar/{Codigo}")]
        public IActionResult PutDeleteModeloCaminhao(int Codigo)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.ModelosCaminhoes.FirstOrDefault(t => t.CodModelo == Codigo);

                if (item == null)
                {
                    throw new FKNotFoundException("Nenhum cargo registrado possui esse Codigo.");
                }

                try
                {
                    var EstoqueModelo = _context.EstoqueCaminhao.Where(f => f.FkModelosCaminhoesCodModelo == Codigo);

                    foreach (var estoqueModelo in EstoqueModelo)
                    {
                        estoqueModelo.CaminhaoEstoqueAtivo = false;
                    }
                    
                    item.ModelosAtivo = false;
                    _context.SaveChanges();
                    return Ok();
                }
                catch(Exception)
                {
                    throw;
                }
            }
        }

        [HttpDelete("Deletar/{Codigo}")]
        public IActionResult DeleteModeloCaminhao(int Codigo)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.ModelosCaminhoes.FirstOrDefault(c => c.CodModelo == Codigo);

                if (item == null)
                {
                    return NotFound();
                }

                try
                {
                     var EstoqueModelo = _context.EstoqueCaminhao.Where(f => f.FkModelosCaminhoesCodModelo == Codigo);

                    foreach (var estoqueModelo in EstoqueModelo)
                    {
                        _context.EstoqueCaminhao.Remove(estoqueModelo);
                    }
                    _context.ModelosCaminhoes.Remove(item);
                    _context.SaveChanges();
                    return Ok();
                }
                
                catch(Exception)
                {
                    throw;
                }
            }
        }
    }
}