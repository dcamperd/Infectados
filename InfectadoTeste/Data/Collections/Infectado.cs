using System;
using MongoDB.Driver.GeoJsonObjectModel;

namespace InfectadoTeste.Data.Collections
{
    public class Infectado
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sexo { get; set; }
        public DateTime DataNascimento { get; set; }
        public GeoJson2DGeographicCoordinates Localizacao { get; set; }

        public Infectado(int id, string nome, string sexo, DateTime dataNasc, double latitude, double longitude) {
            this.Id = id;
            this.Nome = nome;
            this.Sexo = sexo;
            this.DataNascimento = dataNasc;
            this.Localizacao = new GeoJson2DGeographicCoordinates(longitude, latitude);
        }
    }
}