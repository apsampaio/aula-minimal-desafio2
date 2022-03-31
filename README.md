<p align="center">
  <a href="http://nestjs.com/" target="blank"><img src="./.github/LogoPackage.svg" width="320" alt="Nest Logo" /></a>
</p>

<h3 align="center">
    DeliveryFIT - Controle de Pacotes
</h3>

<p align="center">
    <img alt=".NET Core 6" src="https://img.shields.io/badge/-Core 6-512BD4?style=for-the-badge&logo=dotnet&logoColor=white">
    <img alt="C Sharp" src="https://img.shields.io/badge/-CSharp-3178C6?style=for-the-badge&logo=csharp&logoColor=white">
    <img alt="JWT" src="https://img.shields.io/badge/-JWT-d63aff?style=for-the-badge&logo=jsonwebtokens&logoColor=white">
    <img alt="SQLite" src="https://img.shields.io/badge/-SQLite-003B57?style=for-the-badge&logo=sqlite&logoColor=white">
</p>

## Description

Neste desafio você deve desenvolver uma API para o controle de envio de pacotes utilizando o .NET Core 6 Minimal APIs.

## 🚀 Tecnologias

- [Core 6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- [JWT](https://jwt.io/)
- [SQLite](https://www.sqlite.org/index.html)

## 🔰 Instalação

```bash
$ dotnet restore
```

## 🚀 Rodando a API

```bash
$ dotnet run
```

## 🧠 Desafios

### GET `/package/{id}`

Essa rota deve retornar uma lista com todos os pacotes armazenados no Banco de Dados.

- [ ] Rota deve retornar apenas o pacote com o mesmo `id`.
- [ ] Caso não exista pacotes com o mesmo `id`, rota deve retornar status 404.
- [ ] Retorno de Erro na rota deve ser padronizado.
- [ ] Rota deve ser autenticada utilizando JWT.
- [ ] Apenas usuários autenticados pode utilizar esta rota.
- [ ] Usuário deve ter privilégio de `User` ou superior para utilizar esta rota.

---

### GET `/package/details/{id}`

Essa rota deve receber um id referente a um pacote pelos Parâmetros de Rota e retornar os detalhes de um pacote do Banco de Dados que contenha o mesmo id. Os detalhes retornados devem conter o pacote relacionado.

- [ ] Rota deve retornar apenas os detalhes com o mesmo `id`.
- [ ] Detalhes devem conter o pacote no objeto de saída.
- [ ] Caso não exista detalhes com o mesmo `id`, rota deve retornar status 404.
- [ ] Retorno de Erro na rota deve ser padronizado.
- [ ] Rota deve ser autenticada utilizando JWT.
- [ ] Apenas usuários autenticados pode utilizar esta rota.
- [ ] Usuário deve ter privilégio de `User` ou superior para utilizar esta rota.

---

### POST `/package`

Essa rota deve receber os parâmetros `recipient`, `houseNumber` e `zipcode` no corpo da requisição em formato JSON para realizar a criação de um Pacote no Banco de Dados e seus Detalhes.

```json
{
  "recipient": "Andre Sampaio",
  "zipcode": "18080320",
  "houseNumber": 200
}
```

- [ ] Rota deve receber os parâmetros `recipient`, `houseNumber` e `zipcode` no corpo da requisição.
- [ ] Parâmetros recebidos devem ser validados.
  - [ ] `recipient` deve ter no mínimo 3 caracteres.
  - [ ] `houseNumber` não pode ser nulo.
  - [ ] `zipcode` não pode ser vazio ou nulo.
  - [ ] `zipcode` deve ser um CEP válido sem caracteres especiais.
- [ ] Parâmetros incorretos devem retornar status 400.
- [ ] Retorno de Erro na rota deve ser padronizado.
- [ ] Devem ser criados um Pacote e Detalhes utilizando os dados recebidos.
  - [ ] Pacote e Detalhes devem estar relacionados no Banco de Dados.
  - [ ] Status de Pacote deve iniciar como `waiting`
  - [ ] Detalhes `deliveredAt` e `withdrawAt` devem iniciar como `null`
- [ ] Rota deve ser autenticada utilizando JWT.
- [ ] Apenas usuários autenticados pode utilizar esta rota.
- [ ] Usuário deve ter privilégio de `User` ou superior para utilizar esta rota.
- [ ] `createdBy` deve ter o nome armazenado no Token do usuário.
- [ ] `userId` deve ter o nome armazenado no Token do usuário.

---

### PATCH `/package/status/{id}`

Essa rota deve receber um `id` nos Parâmetros de Rota da requisição e realizar a atualização do Status de um Pacote com o mesmo `id`.

- [ ] Rota deve retornar apenas o Pacote após atualização.
- [ ] O Status deve ser atualizado para o próximo valor de `enum`.
  - [ ] A Rota não pode atualizar o Status para `misplaced`
- [ ] Detalhes `deliveredAt` e `withdrawAt` devem ser atualizados conforme a atualização de Status.
- [ ] Caso não exista pacote com o mesmo `id`, rota deve retornar status 404.
- [ ] Retorno de Erro na rota deve ser padronizado.
- [ ] Rota deve ser autenticada utilizando JWT.
- [ ] Apenas usuários autenticados pode utilizar esta rota.
- [ ] Apenas o criador do pacote pode atualiza-lo.
- [ ] Usuários com privilégio `Admin` podem atualizar qualquer pacote.

---

### PATCH `/package/misplaced/{id}`

Essa rota deve receber um `id` nos Parâmetros de Rota da requisição e realizar a atualização do Status de um Pacote com o mesmo `id` para perdido.

- [ ] Rota deve retornar apenas o Pacote após atualização.
- [ ] O Status deve ser atualizado para o valor de `misplaced`.
- [ ] Caso não exista pacote com o mesmo `id`, rota deve retornar status 404.
- [ ] Retorno de Erro na rota deve ser padronizado.
- [ ] Rota deve ser autenticada utilizando JWT.
- [ ] Apenas usuários autenticados pode utilizar esta rota.
- [ ] Apenas o criador do pacote pode atualiza-lo.
- [ ] Usuários com privilégio `Admin` podem atualizar qualquer pacote.

---

### DELETE `/package/{id}`

Essa rota deve receber um `id` nos Parâmetros de Rota da requisição e realizar a exclusão de um Pacote com o mesmo `id`.

- [ ] Rota deve retornar apenas status 204 na resposta, sem corpo.
- [ ] Pacote e Detalhes relacionados devem ser excluídos do Banco de Dados.
- [ ] Caso não exista pacote com o mesmo `id`, rota deve retornar status 404.
- [ ] Retorno de Erro na rota deve ser padronizado.
- [ ] Rota deve ser autenticada utilizando JWT.
- [ ] Apenas usuários autenticados pode utilizar esta rota.
- [ ] Apenas o criador do pacote pode excluí-lo.
- [ ] Usuários com privilégio `Admin` podem atualizar qualquer pacote.

---

### 🎲 Modelos de Dados

**Modelo de Pacote no Banco de dados**

```csharp
public record Package
{
    public Guid id { get; init; }

    public string createdBy { get; init; } = default!;
    public Guid userId { get; init; }

    public DateTime updatedAt { get; set; }
    public Status status { get; set; }

    public Details Details { get; init; } = default!;
}
```

**Modelo de Detalhes no Banco de dados**

```csharp
public record Details
{
    public Guid id { get; init; }
    public string recipient { get; init; } = default!;
    public string zipcode { get; init; } = default!;
    public Int32 houseNumber { get; init; }

    public Nullable<DateTime> postedAt { get; set; }
    public Nullable<DateTime> withdrawnAt { get; set; }
    public Nullable<DateTime> deliveredAt { get; set; }

    public Guid packageId { get; init; }
    public Package Package { get; init; } = default!;
}
```

**Modelo de Status**

```csharp
public enum Status
{
    waiting = 1,
    transporting = 2,
    delivered = 3,
    misplaced = 4,
}
```

## 🚀 Bônus

Os exercícios bônus são feitos para se desafiar em relação aos aprendizados do curso, a entrega não é obrigatório.

### GET `/package/statistics`

Essa rota deve retornar as estatísticas em relação aos status de pacotes do Banco de Dados.

- [ ] Rota deve retornar uma lista com a somatória de cada status no banco de dados.
- [ ] Rota deve ser autenticada utilizando JWT.
- [ ] Apenas usuários autenticados pode utilizar esta rota.
- [ ] Usuários com privilégio `User` ou superior podem utilizar esta rota.

```json
[
  {
    "status": 3,
    "count": 1
  },
  {
    "status": 2,
    "count": 3
  }
]
```

---

## 📅 Entrega

Esse desafio deve ser entregue a partir da plataforma Edx, enviado em formato de repositório de código, plataforma de edição de código online ou zip. **Lembre-se de manter o arquivo público antes de compartilhar com o instrutor.**

Feito com 💜 por [Andre Sampaio](https://github.com/apsampaio) <img src="https://media.giphy.com/media/hvRJCLFzcasrR4ia7z/giphy.gif" width="25px">
