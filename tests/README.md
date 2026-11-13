# Lab 5, Part 1: Backend unit tests

`Emerald.Lobster.Domain.Tests` is an MSTest project targeting .NET 8. It references
only the Domain project, so no database or web server is needed.

Run from the repository root:

```sh
dotnet build
dotnet test
dotnet test /p:CollectCoverage=true
```

Both `coverlet.collector` and `coverlet.msbuild` are included. The final command
writes `tests/Emerald.Lobster.Domain.Tests/coverage.json`, which is ignored by Git.

## Tests

- `Can_Create_New_Rating`: verifies stars, user name, and review.
- `Cannot_Create_Rating_With_Invalid_Stars`: verifies zero stars throws
  `ArgumentException`. The exception assertion scopes the check to the constructor.
- `Can_Create_New_Item`: verifies name, description, brand, and price.
- `Can_Create_Add_Rating`: verifies adding a rating stores the same rating in the
  item's collection.

## Verified coverage progression

Measured with .NET SDK 8.0.428 and the Debug configuration:

| Tests run | Passed | Domain line coverage | Branch coverage | Method coverage |
| --- | --- | --- | --- | --- |
| Rating tests only | 2 | 26.08% | 35.71% | 19.04% |
| Rating tests + Item constructor | 3 | 60.86% | 64.28% | 47.61% |
| All Part 1 tests | 4 | 67.39% | 64.28% | 52.38% |

To reproduce the intermediate runs, append these filters to the coverage command:

```sh
--filter FullyQualifiedName~RatingTests
--filter "FullyQualifiedName~RatingTests|Name=Can_Create_New_Item"
```

These percentages cover the entire Domain assembly, including Orders classes
that this lab does not test. Build succeeded with six existing warnings: nullable
properties in Order/OrderItem and lowercase migration class names.

In the restricted verification environment, MSBuild required `-m:1` and
`-p:UseSharedCompilation=false`. These flags were used for build/test runs and do
not change the test assertions or coverage scope.
