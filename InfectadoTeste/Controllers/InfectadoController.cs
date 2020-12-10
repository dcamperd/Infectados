using InfectadoTeste.Data.Collections;
using InfectadoTeste.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace InfectadoTeste.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InfectadoController : ControllerBase
    {
        Data.MongoDb _mongoDb;
        IMongoCollection<Infectado> _infectadosCollection;

        public InfectadoController(Data.MongoDb mongoDb) {
            _mongoDb = mongoDb;
            _infectadosCollection = _mongoDb.Db.GetCollection<Infectado>(typeof(Infectado).Name.ToLower());
        }

        [HttpPost]
        public ActionResult SalvarInfectado([FromBody] InfectadoDto dto) {
            var infectado = new Infectado(dto.Id, dto.Nome, dto.Sexo, dto.DataNascimento, dto.Latitude, dto.Longitude);
            _infectadosCollection.InsertOne(infectado);

            return StatusCode(201, "Infectado adicionado com sucesso");
        }

        [HttpGet]
        public ActionResult ObterInfectado() {
            var infectados = _infectadosCollection.Find(Builders<Infectado>.Filter.Empty).ToList();
            
            return Ok(infectados);
        }

        [HttpPut]
        public ActionResult AtualizarInfectado([FromBody] InfectadoDto dto) {

            var filter = Builders<Infectado>.Filter.Where(_ => _.Id == dto.Id);
            

            var combine = Builders<Infectado>.Update.Set("nome", dto.Nome).Set("sexo", dto.Sexo).Set("dataNascimento", dto.DataNascimento).Set("latitude", dto.Latitude).Set("longitude", dto.Longitude);

            _infectadosCollection.UpdateMany(filter, combine);

            return Ok("Infectado atualizado com sucesso");
        }

        [HttpDelete("{id}")]
        public ActionResult DeletarInfectado(int id) {
            
            _infectadosCollection.DeleteOne(Builders<Infectado>.Filter.Where(_ => _.Id == id));

            return Ok("Infectado excluido com sucesso");
        }
    }
}