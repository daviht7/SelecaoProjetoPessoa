using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using SelecaoStefanini.Entities;

namespace SelecaoStefanini.DAO
{
    public class PessoaDAO
    {

        private MongoClient mongoClient;
        private IMongoCollection<Pessoa> pessoaCollection;

        public PessoaDAO()
        {
            mongoClient = new MongoClient("mongodb://localhost:27017");
            var database = mongoClient.GetDatabase("selecaostefanini");
            pessoaCollection = database.GetCollection<Pessoa>("pessoa");
        }

        public async Task<List<Pessoa>> FindAll()
        {
            var pessoas = await pessoaCollection.AsQueryable<Pessoa>().ToListAsync();
            return pessoas;
        }

        public async Task<Pessoa> FindById(string id)
        {
            var pessoa = await pessoaCollection.Find(p => p.Id ==id).FirstOrDefaultAsync();
            return pessoa;
        }

        public async Task<Pessoa> FindByCpf(string cpf)
        {
            var pessoa = await pessoaCollection.Find(p => p.CPF == cpf).FirstOrDefaultAsync();
            return pessoa;
        }

        public async Task<Pessoa> Create(Pessoa p)
        {
            await pessoaCollection.InsertOneAsync(p);
            return p;
        }

        public async Task<Pessoa> Update(Pessoa p)
        {
            var filter = Builders<Pessoa>.Filter.Eq(s => s.Id, p.Id);
            var result = await pessoaCollection.ReplaceOneAsync(filter, p);
            return p;
        }

        public async void delete(string id)
        {
            var deleteFilter = Builders<Pessoa>.Filter.Eq("_id", ObjectId.Parse(id));
            await pessoaCollection.DeleteOneAsync(deleteFilter);
        }

    }
}
