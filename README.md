# CV GraphQL API

A deliberately small .NET 10 API using [GraphQL.NET](https://github.com/graphql-dotnet/graphql-dotnet). The web project is organized into lightweight Domain, Application, Infrastructure, and GraphQL areas; sample data is kept in memory.

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/10.0)

## Run

From the repository root:

```powershell
dotnet restore .\Cv.slnx
dotnet run --project .\Cv.Api --urls https://localhost:7135
```

If your browser does not trust the local HTTPS certificate, run:

```powershell
dotnet dev-certs https --trust
```

## Run with Docker

From the repository root:

```powershell
docker build -t cv-api .
docker run --rm -p 8080:8080 cv-api
```

The container exposes:

- Root: [http://localhost:8080/](http://localhost:8080/) redirects to GraphiQL
- GraphQL: [http://localhost:8080/graphql](http://localhost:8080/graphql)
- GraphiQL: [http://localhost:8080/ui/graphiql](http://localhost:8080/ui/graphiql)
- Health: [http://localhost:8080/health](http://localhost:8080/health)

Press `Ctrl+C` to stop and remove the container.

## Endpoints

| Method | Route | Purpose |
|---|---|---|
| `GET` | `/` | Redirect to GraphiQL |
| `POST` | `/graphql` | Execute GraphQL queries |
| `GET` | `/ui/graphiql` | Explore the schema and execute queries in GraphiQL |
| `GET` | `/health` | Check application health |

The examples below assume the API is running at `https://localhost:7135`.

## Use GraphiQL

1. Open [https://localhost:7135/ui/graphiql](https://localhost:7135/ui/graphiql).
2. Enter a query in the query editor.
3. Add JSON values in the **Variables** panel when the query declares variables.
4. Select **Execute** or press `Ctrl+Enter`.

For example:

```graphql
query Profile($id: ID!) {
  profile(id: $id) {
    id
    name
    companies { id name }
    projects { id name }
    education { id institution }
    skills { id name }
  }
}
```

Use these variables:

```json
{
  "id": "profile-1"
}
```

## Root queries

| Query | Result |
|---|---|
| `profiles` | All profiles |
| `profile(id: ID!)` | The matching profile, or `null` |
| `company(id: ID!)` | The matching company, or `null` |
| `project(id: ID!)` | The matching project, or `null` |
| `education(id: ID!)` | The matching education item, or `null` |
| `skill(id: ID!)` | The matching skill, or `null` |

Profile results also expose profile-scoped `companies`, `projects`, `education`, and `skills` collections.

## Project structure

| Path | Responsibility |
|---|---|
| `Cv.Api/Domain` | Public domain records returned by the API |
| `Cv.Api/Application` | Application service contracts |
| `Cv.Api/Infrastructure` | In-memory query implementation and sample data |
| `Cv.Api/GraphQL` | Schema, root queries, and graph types |
| `Cv.Api.Tests/Builders` | Fluent builders for isolated test data |
| `Cv.Api.Tests/Infrastructure` | Test host, service replacement, and GraphQL client setup |
| `Cv.Api.Tests/*GraphQlTests.cs` | HTTP-level integration tests grouped by domain type |

## Test

Run the complete solution test suite from the repository root:

```powershell
dotnet test .\Cv.slnx
```
