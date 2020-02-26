# SelecaoProjetoPessoaBack
Um projeto de CRUD de Pessoa com C# e mongodb no back


Para utilizar , basta modificar no arquivo cs da pasta DAO/PessoaDAO.cs o server mongo.

EXEMPLO : "mongoClient = new MongoClient("mongodb://localhost:27017");
var database = mongoClient.GetDatabase("selecaostefanini");
pessoaCollection = database.GetCollection<Pessoa>("pessoa"); "
