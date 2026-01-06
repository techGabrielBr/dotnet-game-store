# GamesStore API

## Descrição do Projeto

A **GamesStore API** é uma aplicação backend desenvolvida em **.NET 10** com o objetivo de servir como MVP de uma plataforma de venda de jogos digitais educacionais.  
Este projeto corresponde à **Fase 1 do Tech Challenge**, focando na **gestão de usuários, jogos, promoções e biblioteca de jogos adquiridos**, servindo de base para evoluções futuras como matchmaking e gerenciamento de servidores.

A aplicação foi construída seguindo boas práticas de engenharia de software, princípios de **Domain-Driven Design (DDD)** e arquitetura monolítica, conforme solicitado.

---

## Objetivos da Fase 1

- Criar uma API REST para:
  - Cadastro e autenticação de usuários
  - Gestão de jogos
  - Gestão de promoções
  - Associação de jogos à biblioteca do usuário (compra simulada)
- Implementar autenticação e autorização via JWT
- Separar responsabilidades entre usuários comuns e administradores
- Garantir persistência de dados
- Criar uma base sólida para futuras fases do projeto

---

## Tecnologias Utilizadas

- .NET 10
- ASP.NET Core Web API (Controllers MVC)
- Entity Framework Core
- SQLite
- JWT (JSON Web Token)
- Swagger / OpenAPI
- xUnit
- FluentAssertions
- EF Core InMemory
- PasswordHasher (.NET)

---

## Arquitetura

A aplicação segue uma **arquitetura monolítica**, organizada em camadas:

- **API**
  - Controllers
  - DTOs (Request / Response Models)
  - Middlewares
- **Domain**
  - Entidades
  - Enums
  - Regras de negócio
- **Infrastructure**
  - DbContext
  - Repositórios
  - Seed de dados
  - Jwt
  - Validators
- **Tests**
  - Testes unitários

O projeto aplica conceitos de **DDD**, como:
- Aggregates
- Repositories
- Bounded Contexts
- Separação entre domínio e infraestrutura

---

## Funcionalidades Implementadas

### Usuários

- Cadastro de usuário comum (role padrão)
- Cadastro de usuário administrador (rota protegida)
- Login com autenticação JWT
- CRUD completo de usuários (admin)
- Validações:
  - Nome obrigatório
  - E-mail válido
  - Senha segura (mínimo de 8 caracteres, letras, números e caractere especial)

### Autenticação e Autorização

- Autenticação baseada em JWT
- Dois níveis de acesso:
  - **User**
  - **Admin**
- Proteção de rotas via policies e Authorize

### Jogos

- Cadastro de jogos (admin)
- Listagem de jogos
- Atualização de preço
- Associação com promoções

### Promoções

- Criação de promoções
- Definição de preço promocional
- Manutenção do preço original do jogo
- Ativação e desativação de promoções

### Biblioteca de Jogos

- Associação entre usuário e jogo
- Endpoint de compra simulada
- Listagem dos jogos adquiridos pelo usuário autenticado

---

## Persistência de Dados

- Banco de dados SQLite
- Migrations com Entity Framework Core
- Seed automático de usuário administrador na inicialização

---

## Usuário Administrador (Seed)

Ao iniciar a aplicação, um usuário administrador é criado automaticamente, caso não exista.

Exemplo:
- Email: `admin@gamesstore.com`
- Senha: definida no seed da aplicação

---

## Documentação da API

A API possui documentação automática via **Swagger**, disponível em: `http://localhost:5122/swagger/index.html`