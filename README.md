# Emerald Lobster API

Lab 4, Part 1: Building an API (steps 1-7).

## Run

Install the .NET 8 SDK, then run from the repository root:

```sh
dotnet restore
dotnet build
dotnet dev-certs https --trust
dotnet run --project Emerald.Lobster.Api
```

Open https://localhost:7250/swagger for Swagger UI. In VS Code, install the REST Client extension and open `catalog.http`; click **Send Request** above each request. The launch profile also listens at http://localhost:5243 and redirects to HTTPS.

## Structure

- `Emerald.Lobster.sln`: API and Domain projects.
- `Emerald.Lobster.Domain/Catalog`: Item and Rating models, constructor validation, and AddRating.
- `Emerald.Lobster.Api`: App entry point, Startup configuration, and CatalogController.
- `catalog.http`: requests covering all six endpoints plus the integer route constraint.

`App.cs` and `Startup.cs` are equivalent implementations of the startup structure described by the lab; the Carmen-provided files were not available for comparison.

## Part 1 behavior

| Method | Route | Expected response |
| --- | --- | --- |
| GET | /catalog | 200 with hard-coded Shirt and Shorts |
| GET | /catalog/{id} | 200 with hard-coded Shirt using the requested ID |
| POST | /catalog | 201 with the submitted item; Location is /catalog/42 |
| POST | /catalog/{id}/ratings | 200 with a hard-coded Shirt and the submitted rating |
| PUT | /catalog/{id} | 204, empty body |
| DELETE | /catalog/{id} | 204, empty body |

These are intentionally the lab's Part 1 stubs. Writes do not persist, and GET by ID does not perform a database lookup. EF Core, SQLite, migrations, and Orders are Part 2 and are not included.

Item constructors reject missing/empty name, description, or brand, and negative prices (zero is allowed by the lab's condition). Ratings require 1-5 stars and a nonempty user name; reviews may be blank. As in the lab, constructor exceptions during JSON binding are not translated into a custom HTTP 400 response.

## Team workflow

Review the Part 1 feature pull request and have a teammate merge it into `main`. It links API Project Structure (#1) and Catalog API (#2).

## Automated HTTP check

With the API running, run `python3 scripts/smoke_test.py` (or `python scripts/smoke_test.py` on Windows). This checks status codes, response bodies, the Created Location header, route constraints, Swagger, and Part 1's stateless behavior. The script accepts the local development certificate for localhost only.
