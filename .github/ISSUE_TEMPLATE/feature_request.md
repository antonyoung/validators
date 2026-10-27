---
name: Feature request
about: Suggest improvements to validation, formatting, country support, or the .NET packages.
title: '[Feature]: '
labels: enhancement
assignees: ''
---

## Summary

Describe the feature you would like in a few sentences.

## Affected component

Select all that apply:

- [ ] Postal code validation
- [ ] IBAN validation
- [ ] Formatting or normalization
- [ ] Additional country support
- [ ] Shared abstractions or dependency injection
- [ ] NuGet packages or framework compatibility
- [ ] Documentation or examples
- [ ] Other (describe below)

## Problem or use case

What are you trying to achieve, and what limitation do you encounter today?
Explain who would benefit and why. Include the package name and version if relevant.

## Proposed behavior

Describe how the feature should work from a user's perspective.

For validation or formatting changes, include:

- **Country or countries:**
- **Example input:**
- **Expected validation result:**
- **Expected formatted output, if applicable:**
- **Relevant options or edge cases:**

Distinguish required validation rules from optional presentation preferences.
For example, accepting an input format and producing a display format may be separate requirements.

## Rules and references (if applicable)

For new country support or changes to validation rules, link to an authoritative specification or reference.

Describe valid and invalid examples, including relevant length, character, prefix, separator, or checksum rules. State whether the rules apply to a particular region or have an effective date.

Use synthetic or public test examples. Changing IBAN characters can change its checksum, so ensure examples still demonstrate the intended behavior.

## Suggested API or usage (optional)

Show how you would like to use the feature in C#.
Use a fenced `csharp` code block and clearly identify any proposed methods or types that do not exist yet.

An implementation proposal is welcome, but not required.

## Alternatives considered

Describe existing APIs, workarounds, or other approaches you considered and why they are insufficient.

## Compatibility considerations (optional)

Could this change affect existing validation results, formatted output, interfaces, or default behavior?
State whether the feature should be opt-in and identify any framework or dependency requirements.

## Acceptance criteria

List observable outcomes that would make this feature complete, including important edge cases.

## Additional context (optional)

Include related issues, example projects, or other supporting information.