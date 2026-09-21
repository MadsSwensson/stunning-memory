# CV GraphQL API

A deliberately small .NET 10 API using [GraphQL.NET](https://github.com/graphql-dotnet/graphql-dotnet). The web project is organized into lightweight Domain, Application, Infrastructure, and GraphQL areas; sample data is kept in memory.

## Prerequisites

- [.NET10](https://dotnet.microsoft.com/en-us/download/dotnet/10.0)
- 

## Run

```powershell
dotnet restore .\Cv.slnx
dotnet run --project .\Cv.Api --urls https://localhost:7135
```

Send GraphQL requests to `POST https://localhost:7135/graphql`, or open GraphiQL at `https://localhost:7135/ui/graphiql`.

## Sample query

```graphql
query {
  profile(id: "profile-1") {
    id
    name
    companies { id name }
    projects { id name }
    education { id institution }
    skills { id name }
  }
  company(id: "company-1") { id name role period description }
  project(id: "project-1") { id name summary description }
  education(id: "education-1") { id institution program period description }
  skill(id: "skill-1") { id name level description }
}
```

PowerShell request:

```powershell
$body = @{ query = 'query { project(id: "project-1") { id name summary description } }' } | ConvertTo-Json
Invoke-RestMethod https://localhost:7135/graphql -Method Post -ContentType 'application/json' -Body $body
```

Run the focused endpoint test with:

```powershell
dotnet test .\Cv.slnx
```
