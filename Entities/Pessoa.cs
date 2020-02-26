using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace SelecaoStefanini.Entities
{
    public class Pessoa
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public string Id { get; set; }

        [BsonElement("nome")]
        [Required]
        public string Nome { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("sexo")]
        public string Sexo { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [BsonElement("datanascimento")]
        [Required]
        public DateTime DataNascimento { get; set; }

        [BsonElement("naturalidade")]
        public string Naturalidade { get; set; }

        [BsonElement("nacionalidade")]
        public string Nacionalidade { get; set; }

        [Required]
        [BsonElement("cpf")]
        public string CPF { get; set; }

    }
}
