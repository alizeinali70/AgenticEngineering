Agentic Engineering
A local Blazor-based assistant workspace that integrates a Web API backend for workspace inspection and Ollama-powered AI tasks.
Supported framework: .NET 10 (Blazor Interactive Server for the UI)
 
Table of Contents
•	Overview
•	Requirements
•	Solution layout
•	Configuration
•	Run (CLI & Visual Studio)
•	API surface used by the UI
•	Development notes
•	Contributing
•	License
 
Overview
AgenticEngineering is a multi-project .NET 10 solution composed of:
•	AgenticEngineering.Api — Web API (controllers, workspace service, AI task proxy)
•	AgenticEngineering.Application — application logic / service layer
•	AgenticEngineering.Domain — domain models
•	AgenticEngineering.Infrustructure — infrastructure implementations (AI, workspace)
•	AgenticEngineering.UI — Blazor Interactive Server UI
The UI communicates with the API to:
•	Check AI health (api/health)
•	Load workspace metadata (api/workspace/details)
•	Send tasks/chat (api/ollamatask/chat)
The API reads configuration keys Ai:Endpoint and Workspace:* (see Configuration).
 
Requirements
•	.NET 10 SDK
•	Visual Studio 2026 or dotnet CLI
•	(Optional) Ollama or other local AI endpoint running at http://localhost:11434/ unless configured otherwise
 
Solution layout
Top-level solution file: AgenticEngineering.slnx
Important projects:
•	AgenticEngineering.Api/
•	AgenticEngineering.UI/
•	AgenticEngineering.Application/
•	AgenticEngineering.Domain/
•	AgenticEngineering.Infrustructure/
UI project references AgenticEngineering.Domain. The API references application/domain/infrastructure projects.
 
Configuration
The API reads configuration values from appsettings.json / environment:
•	Ai:Endpoint — base URL for the AI (default fallback in code: http://localhost:11434/)
•	Workspace:ProjectName — optional project name
•	Workspace:ProjectPath — optional workspace path
Set these values in AgenticEngineering.Api/appsettings.Development.json or via environment variables before running.
Example environment-based run (powershell):
