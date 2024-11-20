📒 Agenda de Contatos API
Uma aplicação API desenvolvida em ASP.NET Core para gerenciar uma agenda de contatos, 
armazenando informações como nome, telefone, e-mail e endereço. (Sendo aprimorado a cada dia)


🚀 Funcionalidades
Adicionar contatos: Insira informações de novos contatos na agenda.
Listar contatos: Consulte todos os contatos cadastrados.
Buscar contato por ID: Obtenha detalhes de um contato específico.
Atualizar contato: Edite as informações de um contato existente.
Excluir contato: Remova um contato da agenda.


🛠️ Tecnologias Utilizadas
ASP.NET Core: Framework para construção da API.
Entity Framework Core: ORM para manipulação de banco de dados.
SQLite: Banco de dados leve e embutido.
Swagger: Interface de documentação e teste da API.


📂 Estrutura do Projeto
📦 AgendaContatoApi
├── 📂 Controllers
│   └── ContatoController.cs     # Controlador da API
├── 📂 Data
│   ├── AgendaContext.cs         # Contexto do banco de dados
│   └── ContatoRepository.cs     # Repositório de contatos
├── 📂 Models
│   └── ContatoModel.cs          # Modelo da entidade Contato
├── Program.cs                   # Configuração principal da API
├── appsettings.json             # Configurações da aplicação
└── README.md                    # Documentação do projeto


🔧 Configuração e Execução
Pré-requisitos: 
.NET 6 SDK ou superior
IDE: Visual Studio 2022 / VS Code
SQLite instalado (opcional, já configurado para funcionar embutido)

Passos
Clone o repositório:

git clone https://github.com/GoddessPersephone/AgendaContato.git

bash
cd AgendaContatoApi

Restaurar pacotes:
bash
dotnet restore

Executar a aplicação:
bash
dotnet 

Acesse o Swagger para testar a API:

https://localhost:7280/swagger

🛠️ Endpoints
Método	Endpoint	Descrição	Exemplo JSON
GET	/api/contato	Lista todos os contatos	-
GET	/api/contato/{id}	Busca um contato pelo ID	-
POST	/api/contato	Adiciona um novo contato	{ "nome": "John", ... }
PUT	/api/contato/{id}	Atualiza as informações do contato	{ "nome": "Jane", ... }
DELETE	/api/contato/{id}	Remove um contato pelo ID	-

🔍 Exemplos de JSON

POST - Adicionar Contato
{
  "nome": "John Doe",
  "telefone": "123456789",
  "email": "johndoe@example.com",
  "endereco": "Rua Exemplo, 123"
}

PUT - Atualizar Contato
{
  "id": 1,
  "nome": "Jane Doe",
  "telefone": "987654321",
  "email": "janedoe@example.com",
  "endereco": "Avenida Principal, 456"
}

📜 Licença
Este projeto é distribuído sob a licença MIT. Consulte o arquivo LICENSE para mais detalhes.

🤝 Contribuições
Contribuições são bem-vindas! 
Para contribuir:

Faça um fork do repositório.
Crie uma nova branch: git checkout -b feature/nova-feature.
Faça suas alterações e commit: git commit -m 'Adiciona nova feature'.
Envie as alterações: git push origin feature/nova-feature.
Abra um Pull Request.

👩‍💻 Desenvolvedora
Criado com 💙 por Lara Veronica.

Entre em contato:

🌐 GitHub https://github.com/GoddessPersephone
✉️ leal3work@gmail.com
😊 https://api.whatsapp.com/send?phone=5541991640451