# Advent of Code 2024 — C#

Solutions for [Advent of Code 2024](https://adventofcode.com/2024) in C# (.NET 10).

## Running

```bash
dotnet run          # Run all days with timing
dotnet test         # Run all tests
dotnet test --filter "FullyQualifiedName~Day13Tests"   # Run a specific day
```

## Solutions

| Day | Title | Part I | Part II | Approach |
|-----|-------|--------|---------|----------|
| 01 | Historian Hysteria | ✅ | ✅ | Sort + zip; frequency map |
| 02 | Red-Nosed Reports | ✅ | ✅ | Sequence validation; tolerance via level removal |
| 03 | Mull It Over | ✅ | ✅ | Regex parsing; state machine for do/don't |
| 04 | Ceres Search | ✅ | ✅ | Grid word search; X-MAS pattern matching |
| 05 | Print Queue | ✅ | ✅ | Topological sort / ordering rules |
| 06 | Guard Gallivant | ✅ | ✅ | Grid simulation; loop detection via visited `(pos, dir)` states |
| 07 | Bridge Repair | ✅ | ✅ | Recursive search with pruning; arithmetic concatenation operator |
| 08 | Resonant Collinearity | ✅ | ✅ | Antenna pair antinode projection |
| 09 | Disk Fragmenter | ✅ | ✅ | Two-pointer block compaction; span-tracking whole-file moves |
| 10 | Hoof It | ✅ | ✅ | DFS from trailheads; HashSet for Part I, path count for Part II |
| 11 | Plutonian Pebbles | ✅ | ✅ | `Dictionary<stone, count>` blink simulation (75 blinks) |
| 12 | Garden Groups | ✅ | ✅ | BFS flood-fill; area × perimeter / area × sides (corner counting) |
| 13 | Claw Contraption | ✅ | ✅ | Cramer's rule for 2×2 linear integer systems |
| 14 | Restroom Redoubt | ✅ | ✅ | Modular simulation; CRT + variance minimisation for Christmas tree |
| 15 | Warehouse Woes | ✅ | ✅ | Box-pushing grid simulation; BFS-collected wide-box pushes for Part II |
| 16 | Reindeer Maze | ✅ | ✅ | Dijkstra on `(position, direction)`; forward + backward pass for all optimal tiles |
| 17 | Chronospatial Computer | ✅ | ✅ | 3-bit VM simulation; digit-by-digit recursive quine search |
| 18 | RAM Run | ✅ | ✅ | BFS shortest path; binary search for first blocking byte |
| 19 | Linen Layout | ✅ | ✅ | Memoised recursive DP — count arrangements |
| 20 | Race Condition | ✅ | ✅ | BFS distance maps from start and end; Manhattan radius cheat enumeration |
| 21 | Keypad Conundrum | ✅ | ✅ | Memoised `(sequence, depth)` path expansion through robot chains |
| 22 | Monkey Market | ✅ | ✅ | Secret number evolution (mix/prune); 4-change window dictionary |
| 23 | LAN Party | ✅ | ✅ | Triangle enumeration; Bron–Kerbosch max clique for password |
| 24 | Crossed Wires | ✅ | ✅ | Gate network simulation; structural ripple-carry adder analysis |
| 25 | Code Chronicle | ✅ | — | Lock/key pin height brute-force matching |
