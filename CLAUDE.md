# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Commands

```bash
dotnet build                  # Build the project
dotnet test                   # Run all tests
dotnet test --filter "FullyQualifiedName~Day01Tests"  # Run tests for a specific day
dotnet test --filter "FullyQualifiedName=advent_of_code_csharp_2024.Day01Tests.RealData_Part1"  # Run a single test
```

## Architecture

This is an Advent of Code 2024 solution project using .NET 10.0 and xUnit. All code lives flat in the project root — no subdirectories for source files.

**Per-day structure:** Each day consists of three files:
- `DayNN.cs` — solution logic (static parse/solve methods)
- `DayNNTests.cs` — xUnit tests with both example and real data
- `DayNN_input.txt` — puzzle input

**Test naming convention:** `ExampleData_PartI`, `RealData_PartI`, `Example_partII`, `RealData_PartII` with hardcoded expected answers.

**Code style:**
- C# records for data structures parsed from input
- Heavy LINQ/functional style for transformations
- Static methods for parsing and solving
- Enums + switch expressions for state machines (e.g. grid direction)
- `Combinations.cs` provides a generic utility for generating all operator combinations (used in Day 7)
