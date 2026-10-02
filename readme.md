# Agentic Engineering

<div align="center">

  <p><strong>A local Blazor-based assistant workspace integrating a Web API backend for workspace inspection and Ollama-powered AI tasks.</strong></p>

  <p>
    <img src="https://img.shields.io/badge/.NET-10.0-512BD4?style=flat-square&logo=.net&logoColor=white" alt=".NET 10" />
    <img src="https://img.shields.io/badge/Blazor-Interactive%2520Server-5C2D91?style=flat-square&logo=blazor&logoColor=white" alt="Blazor" />
    <img src="https://img.shields.io/badge/Ollama-Local%2520AI-000000?style=flat-square&logo=ollama&logoColor=white" alt="Ollama" />
    <img src="https://img.shields.io/badge/License-MIT-green.svg?style=flat-square" alt="License" />
  </p>

</div>

---

## 📖 Table of Contents

- [Overview](#-overview)
- [Architecture & Solution Layout](#-architecture--solution-layout)
- [System Requirements](#-system-requirements)
- [Configuration](#-configuration)
- [API Surface Reference](#-api-surface-reference)
- [Running the Project](#-running-the-project)
  - [Using the .NET CLI](#using-the-net-cli)
  - [Using Visual Studio 2026](#using-visual-studio-2026)
- [Development Notes](#-development-notes)
- [Contributing](#-contributing)
- [License](#-license)

---

## 🔍 Overview

**AgenticEngineering** is a multi-project .NET 10 solution designed as a local assistant workspace. It bridges a modern Blazor Interactive Server UI with a backend Web API capable of inspecting local workspaces and proxying requests to local AI models via Ollama.

The UI communicates seamlessly with the API to:
* 🩺 **Check AI health** (`api/health`)
* 📂 **Load workspace metadata** (`api/workspace/details`)
* 💬 **Send tasks & chat prompts** (`api/ollamatask/chat`)

---

## 🏛️ Architecture & Solution Layout

Top-level solution file: `AgenticEngineering.slnx`

```text
AgenticEngineering/
│
├── AgenticEngineering.Api/              # Web API (Controllers, Workspace Service, AI Proxy)
├── AgenticEngineering.Application/      # Application logic & service layer
├── AgenticEngineering.Domain/           # Core domain models
├── AgenticEngineering.Infrustructure/   # Infrastructure implementations (AI clients, workspace I/O)
└── AgenticEngineering.UI/               # Blazor Interactive Server user interface
```

* **`AgenticEngineering.UI`** references `AgenticEngineering.Domain`.
* **`AgenticEngineering.Api`** references Application, Domain, and Infrastructure projects.

---

## ⚙️ System Requirements

* **.NET 10 SDK** or later
* **Visual Studio 2026** or the **`dotnet` CLI**
* **Ollama** (or another compatible local AI endpoint) running at `http://localhost:11434/` *(unless configured otherwise)*

---

## 🛠️ Configuration

The API reads configuration values from `appsettings.json`, environment variables, or user secrets:

* `Ai:Endpoint` — Base URL for the AI service *(default fallback in code: `http://localhost:11434/`)*
* `Workspace:ProjectName` — Optional project name
* `Workspace:ProjectPath` — Optional target workspace path

Configure these values in `AgenticEngineering.Api/appsettings.Development.json` or pass them via environment variables.

### Environment Variable Example (PowerShell)
```powershell
$env:Ai__Endpoint="http://localhost:11434/"
$env:Workspace__ProjectName="MyWorkspace"
$env:Workspace__ProjectPath="C:\Projects\AgenticEngineering"
dotnet run --project AgenticEngineering.Api
```

---

## 🔌 API Surface Used by the UI

| Endpoint | Method | Description |
| :--- | :---: | :--- |
| `api/health` | `GET` | Checks connectivity and readiness of the local AI service. |
| `api/workspace/details` | `GET` | Loads metadata regarding the current workspace and project. |
| `api/ollamatask/chat` | `POST` | Dispatches tasks or chat streams to the Ollama-backed agent. |

---

## 🚀 Running the Project

### Using the .NET CLI

1. **Restore dependencies** across the solution:
   ```bash
   dotnet restore AgenticEngineering.slnx
   ```

2. **Run the API project**:
   ```bash
   dotnet run --project AgenticEngineering.Api/AgenticEngineering.Api.csproj
   ```

3. **Run the UI project** (open a separate terminal window):
   ```bash
   dotnet run --project AgenticEngineering.UI/AgenticEngineering.UI.csproj
   ```

### Using Visual Studio 2026

1. Open `AgenticEngineering.slnx` in Visual Studio 2026.
2. Configure **Multiple Startup Projects**:
   * Set both `AgenticEngineering.Api` and `AgenticEngineering.UI` to **Start**.
3. Press **`F5`** to build and run the solution.

---

## 💡 Development Notes

* Ensure your local AI container or Ollama instance is actively running before triggering any AI-driven tasks from the Blazor UI.
* Use absolute filesystem paths for configuration options if your workspace inspection service relies on root-level directory traversal.

---

## 🤝 Contributing

Contributions, bug reports, and feature requests are always welcome! Feel free to open an issue or submit a Pull Request.

---

## 📄 License

This project is licensed under the terms specified in the [LICENSE](LICENSE) file.