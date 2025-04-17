using NUnit.Framework;

// Feature-level: runs each feature file in parallel
//[assembly: Parallelizable(ParallelScope.Fixtures)]

// OR

// Scenario-level: runs each scenario in parallel
// [assembly: Parallelizable(ParallelScope.Children)]