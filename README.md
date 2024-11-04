GrowerTech - Sistema de Recomendação de Insumos Agrícolas


GrowerTech é um sistema inteligente de recomendação de insumos agrícolas, desenvolvido para ajudar produtores rurais a tomar decisões informadas sobre quais produtos utilizar em suas plantações. O sistema analisa dados climáticos, tipos de solo, histórico de cultivos e outras variáveis para recomendar os insumos mais adequados para cada situação.

//Funcionalidades//

1-Registro de Propriedades Rurais
Permite que produtores cadastrem detalhes sobre suas propriedades, incluindo localização, área cultivada e tipo de cultura.

2-Análise de Dados Agronômicos
O sistema coleta e analisa dados sobre clima, tipos de solo, umidade, histórico de pragas e doenças, entre outras variáveis.

3-Recomendação de Insumos
Com base na análise dos dados, o sistema recomenda insumos agrícolas que podem otimizar a produtividade.

4-Autenticação via Google
Integração com API de autenticação do Google para login seguro.

//Estrutura do Projeto//

O projeto segue uma organização modular, com as principais pastas para Controllers, Models, Services, e Views, além de configurações e arquivos de inicialização como Startup.cs e Program.cs. A pasta Views contém as interfaces de usuário para páginas como Home, Login e Cadastro.

//Configuração do Projeto//

1-Banco de Dados
O sistema utiliza Oracle como banco de dados. Configure a string de conexão em appsettings.json.

2-Autenticação com Google
Configure as credenciais do Google na seção Authentication:Google em appsettings.json ou com dotnet user-secrets.


//Testes//
O projeto inclui testes de unidade e de integração com xUnit, cobrindo os principais métodos e funcionalidades:

UsersControllerTests.cs - Testa funcionalidades do controlador de usuários.
UserServiceTests.cs - Testa os métodos principais do serviço de usuários.



//Práticas de Clean Code e SOLID//
Para garantir um código limpo e modular, foram aplicados princípios de Clean Code e SOLID:

SRP (Single Responsibility Principle): Cada classe tem uma responsabilidade específica, facilitando manutenção e entendimento.
DIP (Dependency Inversion Principle): A injeção de dependência permite a flexibilidade e facilita a criação de testes para cada serviço.
OCP (Open/Closed Principle): As classes podem ser estendidas sem alterar o código original, permitindo fácil evolução do sistema.

//Como Executar o Projeto//


1-Clone o repositório:
git clone https://github.com/luigi138/GrowerTech.git
cd GrowerTech

2-Instale as dependências:
dotnet restore

3-Configure as credenciais do Google e a string de conexão no appsettings.json.

4-Execute as migrações:
dotnet ef database update


5-Inicie a aplicação:
dotnet run

Acesse a aplicação em http://localhost:5000.

Observações
Este projeto é voltado para otimizar o processo de escolha de insumos na agricultura, utilizando análise de dados para oferecer recomendações que visam aumentar a produtividade e a eficiência nas propriedades rurais.