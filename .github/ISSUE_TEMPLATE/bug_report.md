---
name: Bug report
about: Report incorrect validation, formatting, exceptions, or package/build issues.
title: '[Bug]: '
labels: bug
assignees: ''
---

## Summary

Describe the problem and its impact in a few sentences.

## Affected component

Select all that apply:

- [ ] Postal code validation
- [ ] IBAN validation
- [ ] Formatting or normalization
- [ ] Shared abstractions or dependency injection
- [ ] NuGet package installation or metadata
- [ ] Build or runtime behavior

## Package and environment

- **NuGet package name(s) and exact version(s):**
- **Application target framework:** (for example, `net10.0`)
- **.NET SDK/runtime version:** (include relevant output from `dotnet --info`)
- **Operating system and version:**
- **Culture, if relevant:** (for example, `nl-NL` or `en-GB`)
- **Using published packages or repository source:** (if source, include branch and commit)

## Steps to reproduce

Provide the smallest reproducible sequence, including package installation
and dependency injection setup where relevant.

1.
2.
3.

### Minimal reproduction

Include a small C# example, failing unit test, or link to a minimal public
reproduction project.

Include:
- Validator construction or dependency injection registrations.
- The exact method overload being called.
- Any formatter options.

Use a fenced `csharp` code block when pasting code.

### Input and options

For validation or formatting issues, provide:

- **Exact input:** (use backticks or a code block to preserve spacing)
- **Selected country:** (state explicitly if using the default)
- **Formatter and replacement value, if any:**
- **Special characters:** (identify tabs, newlines, nonbreaking spaces,
  or other invisible characters)

Use test data that still reproduces the issue. Masking or changing IBAN
characters can change its checksum and invalidate the reproduction.

## Expected behavior

State the expected validation result and formatted output.

For country-specific rules, link to an authoritative specification or
reference if available.

## Actual behavior

State the actual validation result, formatted output, and error message.

For IBAN issues, include any relevant parsed fields returned by the validator.

If an exception occurs, include its type, message, and full stack trace in
a fenced `text` code block, with sensitive information removed.

## Regression information (optional)

- **Did this work in an earlier version?**
- **Last known working version:**
- **First known affected version:**
- **Workaround, if any:**

## Additional context (optional)

Include related issues, relevant logs, or other details.

Screenshots are optional; prefer copyable code and text for validation
results and errors.