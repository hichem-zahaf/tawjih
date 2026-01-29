# UIAutomation

A Windows UI automation tool built with .NET Framework 4.5.2 and Windows Automation API, designed to automate legacy applications on older operating systems like Windows Server 2008/2009 where modern RPA tools fail to work.

## Disclaimer

**This is a hobby project created for fun and educational purposes.** The source code is provided for those who want to enhance, reuse, or commercialize it.

### Security Notice

This tool has some security concerns that you must be aware of before using it in a production environment:

1. **SSL Certificate Validation Bypass**: The application bypasses SSL certificate validation for HTTPS requests. This makes it vulnerable to man-in-the-middle attacks.
2. **No Authentication**: The HTTP/TCP listeners do not implement any authentication mechanism. Anyone who can reach the endpoint can execute automation commands.
3. **Elevated Privileges**: The application requires elevated privileges to interact with UI elements, which could be exploited if not properly secured.
4. **Remote Code Execution**: The ability to start tasks and send keys could be exploited to execute arbitrary commands.
5. **Debug Mode**: Debug mode can expose sensitive information about your system and automation logic.

**Use at your own risk. Always run in a secure, isolated environment and never expose the API endpoints to untrusted networks.**

---

## Table of Contents

- [Features](#features)
- [Getting Started](#getting-started)
- [XPath Selector Guide](#xpath-selector-guide)
- [Testing & Debugging](#testing--debugging)
- [Creating Automation (JSON)](#creating-automation-json)
- [API Control](#api-control)
- [Available Actions](#available-actions)
- [Building](#building)

---

## Features

- **UI Element Detection**: Find and interact with any Windows UI element using XPath-like selectors
- **Manual Testing Interface**: Test individual actions before creating full automation workflows
- **JSON-Based Automation**: Create reusable automation sequences using JSON
- **Remote API Control**: Expose automation via HTTP/TCP/UDP listeners for remote control
- **Debug Mode**: Toggle debug mode to break on errors for troubleshooting
- **File Operations**: Copy, move, and rename files as part of automation
- **Process Control**: Start, kill tasks and processes
- **SSH Tunneling**: Built-in SSH tunneling integration with PuTTY

---

## Getting Started

### Prerequisites

- Windows 7 or later (tested on Windows Server 2008/2009)
- .NET Framework 4.5.2 or later
- Visual Studio 2015+ (for building from source)
- Administrator privileges (required for UI automation)

### Installation

1. Clone or download this repository
2. Open `UIAutomation.sln` in Visual Studio
3. Build the solution (Release mode recommended)
4. Run the compiled executable

**Note**: The application must be run with administrator privileges to interact with other applications' UI elements.

---

## XPath Selector Guide

The application uses a custom XPath-like syntax to locate UI elements. This is not standard XML XPath, but a simplified syntax for Windows UI Automation.

### Syntax Structure

```
Window[@Name='WindowName']//ControlType[@Attribute='Value']
```

### Window XPath (Top Level)

The **Window XPath** field identifies the target application window:

| Pattern | Description | Example |
|---------|-------------|---------|
| `Window[@Name='...']` | Match by window title | `Window[@Name='Notepad']` |
| `Window[@ClassName='...']` | Match by class name | `Window[@ClassName='Notepad']` |
| `Window[@AutomationId='...']` | Match by automation ID | `Window[@AutomationId='MainWindow']` |

### Element XPath (Child Elements)

The **Element XPath** field identifies specific elements within the window:

#### Control Types

Supported control types (case-insensitive):

| Type | Description |
|------|-------------|
| `Window` | Application window |
| `Button` | Clickable button |
| `Edit` | Text input field |
| `Text` | Static text label |
| `CheckBox` | Checkbox control |
| `RadioButton` | Radio button |
| `ComboBox` | Dropdown list |
| `List` | List control |
| `ListItem` | Item in a list |
| `Tab` | Tab control |
| `TabItem` | Individual tab |
| `Tree` | Tree view |
| `TreeItem` | Tree item |
| `MenuItem` | Menu item |
| `Table` | Table control |
| `Document` | Document/text area |
| `Pane` | Generic container |
| `Hyperlink` | Link |
| `Header` | Header control |
| `StatusBar` | Status bar |
| `Custom` | Custom control type |

#### Attributes

| Attribute | Description | Example |
|-----------|-------------|---------|
| `@Name` | Display name/text | `Button[@Name='OK']` |
| `@AutomationId` | Automation ID | `Edit[@AutomationId='username']` |
| `@ClassName` | Window class name | `Window[@ClassName='Notepad']` |

#### Navigation Operators

| Operator | Description | Example |
|----------|-------------|---------|
| `//` | Search all descendants (default) | `Window//Button` |
| `#/` | Search only direct children | `Window#/Pane` |
| `*` | Wildcard (any control type) | `Window//*[@Name='Save']` |

#### Examples

```
# Find a button by name
Button[@Name='Click Me']

# Find an edit field by automation ID
Edit[@AutomationId='usernameInput']

# Nested path with wildcards
Window//Pane//*[@Name='Submit']

# Direct child only
Window#/Button

# Multiple attributes combined
Button[@Name='Save' and @AutomationId='btnSave']
```

---

## Testing & Debugging

Before creating automation, use the built-in testing interface to verify your XPath selectors work correctly.

### Step 1: Enter Window XPath
Type the target window's selector, e.g., `Window[@Name='Calculator']`

### Step 2: Enter Element XPath
Type the target element's selector, e.g., `Button[@Name='1']`

### Step 3: Test Actions

| Button | Description |
|--------|-------------|
| **Click** | Click the target element |
| **Get value** | Retrieve the element's current value/text |
| **Set Value** | Set a value (for editable elements) |
| **Set Text** | Type text into the element |
| **Wait** | Wait for the element to appear (uses timeout value) |
| **Move Mouse** | Move cursor to the element's position |
| **Close Window** | Close the target window |

### Step 4: Review Results
- Success actions will show in the "Field Value" output field
- Failures will trigger debug mode (if enabled) or silent error reporting

---

## Creating Automation (JSON)

Once you've tested individual actions, create an automation sequence using JSON.

### JSON File Location

Place your `actions.json` file on your Desktop (default location) or modify the code to load from a different path.

### JSON Structure

```json
[
  {
    "actionType": "ActionName",
    "windowXPath": "Window[@Name='...']",
    "elementXPath": "ControlType[@Attribute='Value']",
    "value": "...",
    "timeout": 3,
    "description": "Optional description"
  }
]
```

### Running Automation

1. Create your `actions.json` file on the Desktop
2. Click **"Start Actions"** button in the application
3. Actions execute sequentially

### Example: Calculator Automation

```json
[
  {
    "actionType": "Click",
    "windowXPath": "Window[@Name='Calculator']",
    "elementXPath": "Button[@Name='1']",
    "description": "Press 1"
  },
  {
    "actionType": "Click",
    "windowXPath": "Window[@Name='Calculator']",
    "elementXPath": "Button[@Name='+']",
    "description": "Press Plus"
  },
  {
    "actionType": "Click",
    "windowXPath": "Window[@Name='Calculator']",
    "elementXPath": "Button[@Name='2']",
    "description": "Press 2"
  },
  {
    "actionType": "Click",
    "windowXPath": "Window[@Name='Calculator']",
    "elementXPath": "Button[@Name('=']",
    "description": "Press Equals"
  }
]
```

### Example: Form Filling Automation

```json
[
  {
    "actionType": "SetText",
    "windowXPath": "Window[@Name='Login Form']",
    "elementXPath": "Edit[@AutomationId='username']",
    "value": "admin",
    "description": "Enter username"
  },
  {
    "actionType": "SetText",
    "windowXPath": "Window[@Name='Login Form']",
    "elementXPath": "Edit[@AutomationId='password']",
    "value": "password123",
    "description": "Enter password"
  },
  {
    "actionType": "Click",
    "windowXPath": "Window[@Name='Login Form']",
    "elementXPath": "Button[@Name='Login']",
    "description": "Click login button"
  },
  {
    "actionType": "Wait",
    "windowXPath": "Window[@Name='Dashboard']",
    "elementXPath": "Text[@Name='Welcome']",
    "timeout": 5,
    "description": "Wait for dashboard to load"
  }
]
```

### Example: File Operations

```json
[
  {
    "actionType": "CopyFile",
    "path1": "C:\\Source\\file.txt",
    "path2": "C:\\Destination\\",
    "FileName": "file.txt",
    "description": "Copy file to destination"
  },
  {
    "actionType": "StartTask",
    "exe": "notepad.exe",
    "arg": "C:\\Destination\\file.txt",
    "description": "Open the copied file"
  }
]
```

---

## API Control

The application can expose its automation capabilities via HTTP/TCP listeners for remote control.

### Starting the HTTP Listener

1. Enter the URL prefix (e.g., `http://localhost:8080/`)
2. Click **"Start Listener"**
3. Send POST requests with automation JSON

### HTTP API

**Endpoint**: `POST {YourPrefix}?rpa=1`

**Headers**:
```
Content-Type: application/json
```

**Body**: Same JSON format as `actions.json`

**Response**:
```json
{
  "status": "success",
  "message": "Automation completed"
}
```

### Example: cURL Request

```bash
curl -X POST "http://localhost:8080/?rpa=1" \
  -H "Content-Type: application/json" \
  -d '[
    {"actionType": "Click", "windowXPath": "Window[@Name=\"Calculator\"]", "elementXPath": "Button[@Name=\"1\"]"}
  ]'
```

### Example: Python Request

```python
import requests

url = "http://localhost:8080/?rpa=1"
actions = [
    {
        "actionType": "Click",
        "windowXPath": "Window[@Name='Calculator']",
        "elementXPath": "Button[@Name='1']"
    }
]

response = requests.post(url, json=actions)
print(response.json())
```

### TCP/UDP Listeners

- **TCP Listener**: Use "Start TCP Listener" to start a raw TCP server on the specified port
- **SSH Tunneling**: Configure SSH settings to tunnel connections securely

---

## Available Actions

### UI Interaction Actions

| Action | Parameters | Description |
|--------|------------|-------------|
| `Click` | windowXPath, elementXPath | Click on an element |
| `SetValue` | windowXPath, elementXPath, value | Set value of an element |
| `SetText` | windowXPath, elementXPath, value | Type text into an element |
| `Get` | windowXPath, elementXPath | Get value from an element |
| `Wait` | windowXPath, elementXPath, timeout | Wait for element to exist |
| `MoveMouse` | windowXPath, elementXPath | Move cursor to element |
| `Focus` | windowXPath, elementXPath | Set focus to element |
| `SendKeys` | value | Send keystrokes |
| `Enter` | - | Send Enter key |
| `Tab` | - | Send Tab key |
| `F5` | - | Send F5 key |
| `CloseWindow` | elementXPath | Close target window |

### File & System Actions

| Action | Parameters | Description |
|--------|------------|-------------|
| `CopyFile` | path1, path2, FileName | Copy file to location |
| `MoveFile` | path1, path2, FileName | Move file to location |
| `StartTask` | exe, arg | Start a program |
| `KillTask` | process | Kill a task by name |
| `KillProcess` | process | Kill a process |
| `Sleep` | timer | Delay in seconds |
| `MaximizeWindow` | elementXPath | Maximize a window |

### Network Actions

| Action | Parameters | Description |
|--------|------------|-------------|
| `Send` | url, flow, target | Send data to URL |
| `file` | url, file | Send file as Base64 |

### Mouse Actions

| Action | Parameters | Description |
|--------|------------|-------------|
| `mouse` | x, y | Move mouse and click at coordinates |

---

## Debug Mode

Enable **Debug Mode** by checking the checkbox in the UI:

- **When Enabled**: Errors will show message boxes with detailed error information
- **When Disabled**: Errors are silently sent to the debug URL (if configured) or logged to console

### Debug URL Configuration

Set the `UIA_debugUrl` variable in the code or configure via the Debug URL textbox to receive error reports via HTTP POST.

---

## Building

### Requirements

- Visual Studio 2015 or later
- .NET Framework 4.5.2 SDK
- NuGet packages (restore automatically)

### Build Steps

```bash
# Build from command line
msbuild UIAutomation.sln /p:Configuration=Release

# Or open in Visual Studio and press Ctrl+Shift+B
```

### Dependencies

- Newtonsoft.Json (for JSON parsing)
- .NET Framework 4.5.2
- Windows Automation API (UIAutomationClient)

---

## License

This is open-source hobby project code. You are free to use, modify, enhance, or commercialize this code.

## Contributing

Contributions are welcome! Feel free to submit pull requests or open issues for bugs and feature requests.

## Author

Created to solve automation challenges on legacy Windows systems where modern RPA tools failed to work.

---

## Troubleshooting

### Issue: "Window not found"
- Verify the target application is running
- Check the Window XPath matches exactly (use Inspect.exe or UIAVerify for help)
- Try using fewer specific attributes

### Issue: "Element not found"
- Verify the Element XPath is correct
- Use the manual testing interface to validate
- Some elements may only be visible after certain actions

### Issue: HTTP Listener won't start
- Run as Administrator
- Check if the port is already in use
- Verify the URL prefix format (include trailing slash)

### Issue: Actions execute but nothing happens
- The application may need to be run with elevated privileges
- Some applications block UI automation
- Try increasing Wait timeout values
