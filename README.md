# 🧪 Data Validation Test Automation – Proof of Concept

## 📖 Table of Contents

- [Overview](#overview)
- [Objectives](#objectives)
- [Implemented-Test-Scenarios](#implemented-test-scenarios)
- [Technical-Stack](#technical-stack)
- [Why-Two-Separate-Projects](#why-two-separate-projects)
- [Project-Structure](#project-structure)
- [How-To-Run-The-Tests](#how-to-run-the-tests)
- [Test-Status](#test-status)
- [Issues-Identified](#issues-identified)
- [Assumptions](#assumptions)
- [For-Stakeholders](#for-stakeholders)
- [Future-Enhancements](#future-enhancements)
- [Conclusion](#conclusion)


## 📌 Overview

This solution is a **Proof of Concept (PoC)** for automating validations across regulatory report files. It addresses two key test scenarios identified by the Regulatory SME team. The framework is designed with scalability in mind, enabling future expansion as requirements evolve.

This PoC lays the foundation for a **sustainable regression testing framework** that can be easily maintained and extended by internal teams.

---

## 🎯 Objectives

- Automate repeatable data validation tasks
- Reduce manual errors and time spent on report checks
- Enable early defect detection in evolving report structures
- Provide a framework that can adapt to future validations

---

## ✅ Implemented Test Scenarios

### 1. **Duplicate Column Validation**

- Ensures each CSV file does **not contain any duplicate column headers**
- Performs case-insensitive header checks
- Fails the test if any duplicates are detected and logs the issue

### 2. **Yes/No Field Validation**

- Uses an Excel definition file (`Data Fields_New.xlsx`) to identify columns that must contain only `Yes` or `No` values
- Checks these fields in every report file (`y_*.csv`)
- Reports missing columns or invalid values

---

## 🧩 Technical Stack

- **Language**: C# (.NET 6)

- **Testing Framework**: Reqnroll (SpecFlow)
- **Test Runner**: NUnit
- **Assertions**: FluentAssertions
- **Excel Parser**: EPPlus
- **Logging**: Custom text & HTML reporting

---

## 🧱 Why Two Separate Projects?

The solution is structured into two distinct projects for clarity, scalability, and separation of concerns:

### 1. **`DataValidationFramework`**  

This project contains all reusable logic such as:
- CSV/Excel parsing utilities
- Business validation logic
- Configuration readers
- Logging and reporting helpers
- Interfaces and services for future extensibility

This separation makes it easier to:
- Reuse the framework in different test harnesses or environments
- Maintain a clean codebase with single-responsibility focus

---

### 2. **`DataValidationFramework.Tests`**  

This is the **SpecFlow (Reqnroll)** test project where:
- BDD-style feature files and step definitions are implemented
- The framework components are consumed via dependency injection
- NUnit test runners execute and log test cases


---

### ✅ Benefits of This Structure:

- Encourages **modular design** and **test-driven development**
- Keeps business logic **decoupled from test execution**
- Supports **scaling into a larger automation suite** over time
- Enables **reusability across teams or CI/CD environments**

---

## 📁 Project Structure

| Folder | Description |
|--------|-------------|
| `DataValidationFramework` | Core logic, configuration, and services |
| `DataValidationFramework.Tests` | Test project using BDD (SpecFlow) |
| `Utils` | CSV and Excel helpers, HTML report generator |
| `Config` | App settings loading (from `appsettings.json`) |
| `Helper` | Thread-safe test logger |
| `Reports` | Auto-generated test logs and reports |
| `TestData/Files` | Input report files (e.g. `y_*.csv`) |
| `TestData/Requirements` | Excel definition file for validations |

---

## 🚀 How to Run the Tests

### ▶️ CLI Execution

```bash
dotnet test
```

### 💻 Run from Visual Studio

You can also run the tests directly within **Visual Studio Test Explorer**:

1. Open the solution in Visual Studio
2. Build the solution (`Ctrl + Shift + B`)
3. Go to **Test > Test Explorer**
4. Click **Run All** or run individual scenarios by right-clicking them

### 📊 View Reports

- **Text log**: `Reports/TestReport.txt`
- **HTML report**: `Reports/TestReport.html`

---

## 📋 Test Status

| Test | Status | Notes |
|------|--------|-------|
| ✅ No Duplicate Columns | ✔️ Passed | All files validated successfully |
| ❌ Yes/No Field Validation | ❌ Failed | Invalid value `PJKqTqBT:xAg2bs` in `y_07.01.csv`, column `c0080` |

---

## 🐞 Issues Identified

- ✅ No duplicate columns detected
- ❌ Invalid `[Yes/No]` field entry:  
  File: `y_07.01.csv`, Column: `c0080`  
  Value: `PJKqTqBT:xAg2bs`  
  This violates the Yes/No format rule from the Excel definition.

---

## 📌 Assumptions

- Only files prefixed with `y_` (e.g., `y_*.csv`) are within scope.
- Excel sheet structure remains consistent for Yes/No field definitions.
- Column mappings follow a naming convention (`y_xx.xx.csv → cZZZZ`).
- "Yes" and "No" are accepted in any case (e.g., "YES", "no", etc.).

---

## 🧠 For Stakeholders

This test automation PoC validates two core compliance checks on regulatory reports:

1. Verifies **no duplicate columns** exist in any report file  
2. Validates that fields marked as `[Yes/No]` in the Excel definition contain only those valid values

By automating these checks:
- Manual inspection effort is reduced
- Errors are caught earlier and consistently
- New validations can be added easily without rewriting code
- Output reports are human-readable and easy to interpret

The solution is modular, lightweight, and ready to evolve into a full-fledged regression suite. It can also integrate into CI/CD systems for ongoing test execution.

---

## 🔮 Future Enhancements

### 🔗 Integration & Automation

- Integration with CI/CD platforms (Azure DevOps, GitHub Actions)
- Email/Slack alerts on test failures

### 🧠 Scalability

- Support for additional file formats (e.g., JSON, XLSX)
- Data-driven testing from external APIs or databases
- Central test configuration via JSON or YAML

### 📊 Reporting & Usability

- Historical test result comparisons
- Visual dashboards (e.g., Allure, ReportPortal)
- Severity-based validations (e.g., warning vs critical)

### 🛠 Functional Enhancements

- Cross-file validation
- Self-service rule management (non-devs can add/edit validations)
- Localized reporting (for international teams)

---

## ✅ Conclusion

This solution meets the PoC requirements by automating two meaningful test cases with a scalable, reusable, and developer-friendly framework. It demonstrates a solid foundation for a full data validation pipeline that can grow with regulatory needs.

---