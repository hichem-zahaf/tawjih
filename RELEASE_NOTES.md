# UIAutomation - Release

## Overview

UIAutomation is a Windows UI automation tool built with .NET Framework 4.5.2 and the Windows Automation API. It was created to automate legacy applications on older operating systems like Windows Server 2008/2009 where modern RPA tools fail to work.

## Why This Tool Exists?

Commercial RPA tools (UiPath, Automation Anywhere, Blue Prism, etc.) simply don't work on older Windows systems. This tool was born out of necessity - to automate a legacy software system running on Windows Server 2008, and it **successfully accomplished what commercial tools could not**.

## What's Included

This release contains the compiled binary files for the UIAutomation application. No installation required - just run the executable.

### System Requirements

- Windows 7 or later (Windows Server 2008/2009 compatible)
- .NET Framework 4.5.2 or later
- Administrator privileges (required for UI automation)

## Quick Start

1. Download and extract the release files
2. Run `UIAutomation.exe` as Administrator
3. Use the built-in interface to:
   - Test individual UI interactions
   - Create JSON automation workflows
   - Start HTTP/TCP API listeners for remote control

## Key Features

- **UI Element Detection** - Find and interact with any Windows UI element using XPath-like selectors
- **Manual Testing Interface** - Test actions before creating full automation
- **JSON-Based Automation** - Create reusable automation sequences
- **Remote API Control** - Expose automation via HTTP/TCP/UDP listeners
- **Debug Mode** - Toggle for troubleshooting
- **File & Process Control** - Copy/move files, start/kill processes
- **Legacy OS Support** - Works where modern RPA tools don't

## Important: Security Notice

> **This is a hobby project created for fun and educational purposes.**

The application has security considerations:
- SSL certificate validation is bypassed
- No authentication on API endpoints
- Requires elevated privileges
- Can execute arbitrary commands

**DO NOT expose API endpoints to untrusted networks. Use only in secure, isolated environments.**

## Documentation

Full documentation including:
- XPath selector guide
- JSON automation examples
- API usage
- All available actions

Is available in the [README.md](README.md) file.

## Source Code

The complete source code is available at: [Insert Your Repository URL Here]

Feel free to:
- Study and learn from the code
- Enhance and modify for your needs
- Reuse in your own projects
- Commercialize if you wish

## Example: Simple Automation

```json
[
  {
    "actionType": "Click",
    "windowXPath": "Window[@Name='Calculator']",
    "elementXPath": "Button[@Name='1']"
  },
  {
    "actionType": "Click",
    "windowXPath": "Window[@Name='Calculator']",
    "elementXPath": "Button[@Name='+']"
  }
]
```

## License

This is open-source hobby project code. Free to use, modify, enhance, or commercialize.

## Support

This is a hobby project maintained in spare time. Issues and pull requests are welcome at the repository.

---

**Made with frustration towards commercial RPA tools on legacy systems.**
