# AntonYoung.Validators

[![Build status](https://dev.azure.com/antonyoung/c05894b0-db29-4558-b7c4-af27777e447d/_apis/build/status/4?branchName=master)](https://dev.azure.com/antonyoung/c05894b0-db29-4558-b7c4-af27777e447d/_build?definitionId=4)
[![Tests](https://img.shields.io/azure-devops/tests/antonyoung/c05894b0-db29-4558-b7c4-af27777e447d/4/master.svg)](https://dev.azure.com/antonyoung/c05894b0-db29-4558-b7c4-af27777e447d/_build?definitionId=4)
[![License: GPL v3](https://img.shields.io/badge/License-GPLv3-blue.svg)](LICENSE)

C# libraries for validating and formatting European postal codes and International Bank Account Numbers (IBANs), using country-specific rules.

The validator packages target **.NET Standard 2.1**. Repository development and tests use **.NET 10**.

## Features

- Country-specific postal code validation and formatting.
- IBAN structure validation and a modulo-97 checksum check.
- IBAN field extraction, including country, check digits, account number, and available bank or branch codes.
- Optional removal or replacement of spaces and hyphens in formatted output.
- Interfaces and constructor injection for integration with dependency injection containers.
- Separate packages for postal code and IBAN functionality.

**Scope:** A successful result means the input satisfies the implemented rules. It does not confirm that a postal code is assigned, that a bank account exists, or who owns that account.

## Packages and requirements

| Package | Purpose |
| --- | --- |
| `AntonYoung.Validators.Postalcode` | Postal code validation and formatting |
| `AntonYoung.Validators.Iban` | IBAN validation, formatting, and field extraction |
| `AntonYoung.Validators.Abstractions` | Shared country and formatter definitions |

To consume the validator packages, use an application framework compatible with .NET Standard 2.1. **.NET Framework does not support .NET Standard 2.1.** You do not need .NET 10 merely to consume a compatible library package.

To build and test this repository, install the .NET 10 SDK.

## Installation

Run the relevant command from your application project directory:

```bash
dotnet add package AntonYoung.Validators.Postalcode
dotnet add package AntonYoung.Validators.Iban
```

Install only the validator packages you need. Package availability and versions depend on your configured NuGet feed.

If the version you want is a prerelease, use `--prerelease` or specify its exact version with `--version`. In the NuGet package manager UI, enable **Include prerelease**.

The dependency injection example below also uses `Microsoft.Extensions.DependencyInjection`. Add it if your application does not already reference it:

```bash
dotnet add package Microsoft.Extensions.DependencyInjection
```

## Quick start

This example registers both validators and resolves them from a service provider. In an existing hosted application, add the registrations to its service collection instead of creating a separate provider.

```csharp
using System;
using AntonYoung.Validators.Abstractions.Enums;
using AntonYoung.Validators.Iban;
using AntonYoung.Validators.Iban.Infrastructure;
using AntonYoung.Validators.Iban.Models;
using AntonYoung.Validators.Postalcode;
using AntonYoung.Validators.Postalcode.Infrastructure;
using AntonYoung.Validators.Postalcode.Models;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

services.AddTransient<IPostalcodeModel, PostalcodeModel>();
services.AddTransient<IPostalcodeValidator, PostalcodeValidator>();

services.AddTransient<IIbanModel, IbanModel>();
services.AddTransient<IIbanValidator, IbanValidator>();

using var provider = services.BuildServiceProvider();

var postalcodeValidator = provider.GetRequiredService<IPostalcodeValidator>();
var ibanValidator = provider.GetRequiredService<IIbanValidator>();

bool postalcodeIsValid = postalcodeValidator.TryValidate(
    "1062GD",
    Countries.Netherlands,
    out string postalcode);

Console.WriteLine(postalcodeIsValid); // True
Console.WriteLine(postalcode);        // 1062 GD

// Example input from this repository's existing documentation.
bool ibanIsValid = ibanValidator.TryValidate(
    "NL71INGB1320949010",
    out string iban);

Console.WriteLine(ibanIsValid); // True
Console.WriteLine(iban);        // NL71 INGB 1320 9490 10
```

Validators receive their rule models through constructor injection. A DI container is convenient but not required; models and validators can also be constructed directly.

**Instance lifetime:** Validators retain state from validation calls. Keep the transient registrations shown above, and do not share a validator instance between concurrent operations. Registering it as transient does not itself make a resolved instance thread-safe.

## Postal code validation

The following examples reuse `postalcodeValidator` from the quick start.

### Default country

The overload without a country uses the Netherlands:

```csharp
bool isValid = postalcodeValidator.TryValidate("1062GD", out string result);

Console.WriteLine(isValid); // True
Console.WriteLine(result);  // 1062 GD
```

For other countries, pass a supported `Countries` value explicitly.

### Invalid input

`TryValidate` returns a Boolean. Read validation details from the validator instance, not from that Boolean:

```csharp
bool isValid = postalcodeValidator.TryValidate(
    "0162GD",
    Countries.Netherlands,
    out string result);

Console.WriteLine(isValid); // False
Console.WriteLine(result);  // 0162GD

if (!isValid)
{
    Console.WriteLine(postalcodeValidator.ErrorMessage);
    Console.WriteLine(postalcodeValidator.Example);
}
```

A Dutch postal code beginning with zero is rejected by the implemented rules.

## Formatting

Successful validation first produces the configured country-specific display format. The selected formatter then removes or replaces separators in that output.

| Formatter | Effect |
| --- | --- |
| `Formatters.None` | Keep the configured display format without additional separator replacement |
| `Formatters.WhiteSpaces` | Remove or replace whitespace |
| `Formatters.Hyphens` | Remove or replace hyphens |
| `Formatters.HyphensAndWhiteSpaces` | Remove or replace both |

The default replacement value is an empty string.

### Remove postal code whitespace

```csharp
bool isValid = postalcodeValidator.TryValidate(
    "1062 GD",
    Countries.Netherlands,
    Formatters.WhiteSpaces,
    out string result);

Console.WriteLine(isValid); // True
Console.WriteLine(result);  // 1062GD
```

### Replace postal code whitespace

```csharp
bool isValid = postalcodeValidator.TryValidate(
    "1062GD",
    Countries.Netherlands,
    Formatters.WhiteSpaces,
    "-",
    out string result);

Console.WriteLine(isValid); // True
Console.WriteLine(result);  // 1062-GD
```

Custom separators are presentation options. The returned Boolean describes validation before that output transformation; it does not guarantee that the customized output will be accepted when validated again.

## IBAN validation and field extraction

The IBAN country is determined from its country prefix. The following example reuses `ibanValidator` from the quick start:

```csharp
bool isValid = ibanValidator.TryValidate(
    "NL71INGB1320949010",
    out string result);

if (isValid)
{
    Console.WriteLine(result);                         // NL71 INGB 1320 9490 10
    Console.WriteLine(ibanValidator.Country);          // Netherlands
    Console.WriteLine(ibanValidator.CheckDigits);      // 71
    Console.WriteLine(ibanValidator.NationalBankCode); // INGB
    Console.WriteLine(ibanValidator.AccountNumber);    // 1320949010
}
else
{
    Console.WriteLine(ibanValidator.ErrorMessage);
}
```

Additional properties include `AccountType`, `NationalBranchCode`, and `NationalCheckDigit`. Availability depends on the country and its implemented rule set.

Read parsed fields after successful validation and before the next call on the same instance.

### Replace IBAN whitespace

```csharp
bool isValid = ibanValidator.TryValidate(
    "NL71INGB1320949010",
    Formatters.WhiteSpaces,
    ".",
    out string result);

Console.WriteLine(isValid); // True
Console.WriteLine(result);  // NL71.INGB.1320.9490.10
```

Use synthetic or publicly documented examples when reporting issues. Do not publish real account details.

## Supported countries and limitations

Support is defined by the implemented rule sets, not solely by membership in the `Countries` enum:

- [Postal code rules](src/AntonYoung.Validators.Postalcode/Models/PostalcodeModel.cs)
- [IBAN rules](src/AntonYoung.Validators.Iban/Models/IbanModel.cs)
- [Country definitions](src/AntonYoung.Validators.Abstractions/Enums/Countries.cs)

Important considerations:

- Country rules can change. Report discrepancies with an authoritative reference and a reproducible test case.
- UK and Irish alphanumeric postal code rules were identified in the original documentation as needing further review; do not assume exhaustive coverage without checking the relevant rules and tests.
- Finnish postal code formatting supports `FI-` and `AX-` prefixes. The configured default is `FI-` when the input omits the prefix.
- `TryValidate` does not guarantee exception-free handling of every input. Unsupported postal code countries can throw `NotSupportedException`; unknown IBAN prefixes and some malformed inputs can also throw.
- Check the Boolean returned by each call. Read `ErrorMessage` on failure rather than assuming it is cleared after a later successful call.

## Build and test

Run these commands from the repository root:

```bash
dotnet restore src/AntonYoung.Validators.sln
dotnet build src/AntonYoung.Validators.sln --configuration Release
dotnet test src/AntonYoung.Validators.sln --configuration Release
```

The repository uses xUnit tests. See the CI badges for reported test results. Execution time depends on the machine, configuration, and test runner; it is not a validator performance benchmark.

To create the validator packages locally:

```bash
dotnet pack src/AntonYoung.Validators.Postalcode/AntonYoung.Validators.Postalcode.csproj --configuration Release --output ./artifacts
dotnet pack src/AntonYoung.Validators.Iban/AntonYoung.Validators.Iban.csproj --configuration Release --output ./artifacts
```

## Contributing

Bug reports, country-rule corrections, documentation improvements, and feature requests are welcome.

- [Report a bug or request a feature](https://github.com/antonyoung/validators/issues/new/choose)
- [Browse existing issues](https://github.com/antonyoung/validators/issues)

For validation issues, include the package version, selected country, exact test input, expected result, and a minimal reproduction. For new rules, include an authoritative reference and tests for valid and invalid inputs.

## Background

Anton Young started this project in August 2019 after encountering inconsistent Dutch postal code data in an application database. The project began with postal code validation and later expanded to IBAN validation and formatting.

## References

- [EU Interinstitutional Style Guide: postal addressing](https://publications.europa.eu/code/en/en-390105.htm)
- [International Bank Account Number overview](https://en.wikipedia.org/wiki/International_Bank_Account_Number)

These are background references used in the original project documentation, not a guarantee that every implemented rule matches the latest specification.

## Author

Anton Young — initial author and maintainer.

## License

This project is licensed under the GNU General Public License v3.0. See [LICENSE](LICENSE) for the license text.
