# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

---

## Repository Overview
This repository contains two primary components:
1. **UIAutomation**: A Windows Forms application for UI automation and remote control tasks.
2. **Claude Code Configuration**: AI-driven development tools and workflows for enhanced productivity.

---

## Codebase Structure
### UIAutomation
- **Language**: C# (.NET Framework 4.5.2)
- **Framework**: Windows Forms
- **Key Files**:
  - `Program.cs`: Application entry point
  - `Form1.cs`: Main automation logic (~2700 lines)
  - `UIAutomation.sln`: Visual Studio solution
  - `UIAutomation.csproj`: Build configuration
  - `App.config`: Application settings
  - `actions.json`: Automation sequence definitions

### Claude Code Configuration
- **Directory**: `.claude/`
- **Purpose**: AI-driven development tools, commands, and agents
- **Key Files**:
  - `README.md`: Documentation for setup and usage
  - `config.json`: Main configuration for AI providers
  - `settings/*.json`: Provider-specific configurations
  - `agents/`, `commands/`, `plugins/`: Custom AI extensions

---

## Common Development Tasks

### UIAutomation
```bash
# Build the application
msbuild UIAutomation.sln

# Run the application
dotnet run --project UIAutomation.csproj

# Debug in Visual Studio
# Open UIAutomation.sln in Visual Studio 2015+ and press F5
```

### Claude Code
```bash
# List available commands
/skills

# List available agents
/agents

# List available plugins
/plugins

# Run a specific command (e.g., review a PR)
/gh:review-pr
```

---

## Architectural Patterns

### UIAutomation
- **Component-Based**: Modular UI components for automation tasks
- **Event-Driven**: Windows Forms event model
- **Configuration-Driven**: Automation sequences defined in `actions.json`

### Claude Code
- **Plugin-Based**: Modular skills and commands
- **Agent-Based**: Specialized AI agents for specific tasks
- **Spec-Driven**: Uses GitHub Spec-Kit for development

---

## Key Features

### UIAutomation
- UI element detection and interaction using XPath-like selectors
- Automation sequences with multiple action types (Click, SetValue, SendKeys, Wait)
- HTTP/TCP listener for remote commands
- File operations (copy, move, rename)
- SSH tunneling integration with PuTTY
- System tray functionality

### Claude Code
- AI-driven code analysis and generation
- GitHub integration for PR review and issue resolution
- Custom commands and agents for specialized tasks
- Multi-provider AI support (GitHub Copilot, LiteLLM, DeepSeek)

---

## Important Configuration Files

### UIAutomation
- `UIAutomation.csproj`: Build configuration and dependencies
- `App.config`: Application settings
- `actions.json`: Automation sequence definitions
- `packages.config`: NuGet package dependencies

### Claude Code
- `.claude/config.json`: Main configuration for AI providers
- `.claude/settings/*.json`: Provider-specific settings
- `.claude/README.md`: Documentation for setup and usage

---

## Security Considerations
- UIAutomation requires elevated privileges for UI operations
- HTTP listener requires `?rpa=1` parameter for validation
- SSL certificate validation is bypassed in HTTP client (potential security risk)

---

## Documentation
- `QWEN.md`: Project overview and usage instructions
- `.claude/README.md`: Claude Code setup and usage
- Inline comments in source files

---

## Workflow Recommendations
1. For UIAutomation development:
   - Use Visual Studio 2015+ for building and debugging
   - Edit `actions.json` for automation sequence definitions
   - Test UI interactions in `Form1.cs`

2. For Claude Code workflows:
   - Use `/skills` to discover available commands
   - Configure AI providers in `.claude/config.json`
   - Use agents for complex tasks like PR review or issue resolution