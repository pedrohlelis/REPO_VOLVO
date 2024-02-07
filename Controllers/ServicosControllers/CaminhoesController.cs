using Microsoft.AspNetCore.Mvc;

namespace TRABALHO_VOLVO
{
    [Route("[controller]")]
    [ApiController]
    public class CaminhoesController : Controller
    {
        [HttpPost("Cadastrar")]
        public IActionResult PostCaminhao([FromForm] Caminhao caminhao)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                //vai verificar a integridade das FKs fornecidas
                // if (!_context.Clientes.Any(c => c.CodCliente == caminhao.FkClientesCodCliente))
                // {
                //     throw new FKNotFoundException("Nenhum cliente registrada possui esse codigo.");
                // }
                // else if (!_context.ModelosCaminhoes.Any(c => c.CodModelo == caminhao.FkModelosCaminhoesCodModelo))
                // {
                //     throw new FKNotFoundException("Nenhum Modelo de caminhao registrado possui esse codigo.");
                // }
                //se chegou ate aqui significa que as FK inseridas estao ok, hora de tentar registrar no bd!!!
                try
                {
                    caminhao.CodCaminhao = 0;
                    caminhao.CaminhaoAtivo = true;
                    // validar os dados do caminhao
                    ValidationHelper.CheckUniqueChassi(_context, caminhao.CodChassiCaminhao);
                    ValidationHelper.ValidateAlphaNumericFormat(caminhao.CodChassiCaminhao, "Codigo chassi invalido.");
                    ValidationHelper.ValidateAlphaFormat(caminhao.CorCaminhao, "Cor de caminhao invalida.");
                    ValidationHelper.ValidateAlphaNumericFormat(caminhao.CodChassiCaminhao, "Placa invalida.");
                    // feita a validacao tentar adicionar o caminhao ao estoque
                    _context.Caminhoes.Add(caminhao);
                    // Hora de gerar o registro da aquisicao
                    _context.SaveChanges();
                    return Ok("O caminhao foi Registrado com sucesso.");
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        [HttpGet("Listar")]
        public List<Caminhao> GetTodosCaminhoes()
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                return _context.Caminhoes.ToList();
            }
        }

        [HttpGet("Buscar/{Codigo}")]
        public IActionResult GetCaminhao(int Codigo)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.Caminhoes.FirstOrDefault(t => t.CodCaminhao == Codigo);
                if (item == null)
                {
                    throw new FKNotFoundException("Nenhum caminhao registrado possui esse codigo.");
                }
                return new ObjectResult(item);
            }
        }

        [HttpPut("Deletar/{Codigo}")]
        public IActionResult PutDeleteCaminhao(int Codigo)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.Caminhoes.FirstOrDefault(t => t.CodCaminhao == Codigo);
                if (item == null)
                {
                    throw new FKNotFoundException("Nenhum caminhao registrado possui esse codigo.");
                }
                item.CaminhaoAtivo = false;
                _context.SaveChanges();
                return Ok();
            }
        }

        [HttpDelete("Deletar/{Codigo}")]
        public IActionResult DeleteCaminhao(int Codigo)
        {
            using (var _context = new TrabalhoVolvoContext())
            {
                var item = _context.Caminhoes.FirstOrDefault(c => c.CodCaminhao == Codigo);

                if (item == null)
                {
                    throw new FKNotFoundException("Nenhum caminhao registrado possui esse codigo.");
                }
                try
                {
                    ManipulacaoDadosHelper.RegistrarDelete("Caminhoes", "Caminhao", item);
                    _context.Caminhoes.Remove(item);
                    _context.SaveChanges();
                    return Ok();
                }

                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}