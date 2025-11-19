# AdminHelper

[![.NET](https://img.shields.io/badge/.NET-8+-blue)](https://dotnet.microsoft.com/)  

> A small, focused utility library in C# providing helper methods to:  
> 1. Check if the current Windows user / process is running with Administrator privileges.  
> 2. Restart an application *as administrator*, using the Windows “runas” verb.

---

## 💡 Table of Contents

- [About](#about)  
- [Features](#features)  
- [Getting Started](#getting-started)  
  - [Prerequisites](#prerequisites)  
  - [Installation](#installation)  
- [Usage](#usage)  
  - [Check for Admin Rights](#check-for-admin-rights)  
  - [Restart as Administrator](#restart-as-administrator)  
- [Architecture / Code Structure](#architecture--code-structure)  
- [Security & Behavior Notes](#security--behavior-notes)  
- [Contributing](#contributing)  
- [Contact](#contact)

---

## About

**AdminHelper** is a minimal, reusable library built in **C#** for Windows applications. Its goal is to simplify two common tasks:

1. **Privilege checking** — determining whether the current user / process is running with elevated (administrator) rights.  
2. **Self-elevation** — restarting the current process with administrator privileges, when needed.

This is especially useful in desktop applications, tools, or installers that require elevation only for specific operations.

---

## Features

- ✅ Lightweight static helper methods  
- 🔍 Checks admin rights using `WindowsIdentity` and `WindowsPrincipal`
- 🔁 Re-launches the running executable with administrative privileges via `ProcessStartInfo` and the `runas` verb
- 📦 No external dependencies — purely built on .NET base libraries  
- 🔧 Easy to integrate into any .NET or .NET Core / .NET 8+ Windows application

---

## Getting Started

### Prerequisites

- .NET 8 SDK (or compatible .NET version)  
- A Windows environment (since this is Windows-specific: WindowsIdentity / UAC)  
- A code editor / IDE such as Visual Studio, Rider, or VS Code

### Installation

1. Clone the repository:  
   ```bash
   git clone https://github.com/dannylbr/AdminHelper.git  
   cd AdminHelper
	```

2. Build the project / library:
   ```bash
   dotnet build
   ```
3. Add the compiled DLL (or project) as a dependency in your application, or pack as a NuGet package for reuse.

### Usage

Here are some typical ways to use AdminHelper in your own app.

#### Check for Admin Rights

```csharp
using AdminHelper;

bool isAdmin = AdminHelper.Security.IsRunAsAdministrator();
if (isAdmin)
{
    Console.WriteLine("Running as admin!");
}
else
{
    Console.WriteLine("Not running as admin.");
}
```

Internally, this uses `WindowsIdentity.GetCurrent()` + `WindowsPrincipal` to check if the current user is in the `Administrator` role.

#### Restart as Administrator

If your application needs elevated rights, you can call:

```csharp
using AdminHelper;

AdminHelper.Security.RestartAsAdministrator();
```

Under the hood, this uses `ProcessStartInfo` with `Verb = "runas"` to re-launch the same executable with elevated privileges. 
After calling this, the current process typically exits.

### Architecture / Code Structure

```graphql
AdminHelper/
│
├── src/
│   └── AdminHelper/              # Core library
│       ├── Security.cs            # Contains the helper methods
│       └── …                      # Possible future helpers
│
├── tests/                         # (Optional) For automated unit tests
│   └── AdminHelper.Tests/
│
├── samples/                       # (Optional) Example console or WinForms apps
│   └── ElevationDemo/
│
└── AdminHelper.sln                # Solution file
```

- `Security.cs` holds the core static methods: checking admin status and restarting as admin.
- `Tests` help ensure behaviors across different Windows / UAC contexts.

### Security & Behavior Notes

- **User Account Control (UAC)**: Even if a user belongs to the Administrators group, the process might not be running elevated *unless explicitly started as admin*. 
- **Elevation prompt**: When `RestartAsAdministrator` is called, Windows will show a UAC prompt, because you're using the `runas` verb.
- **Arguments and state**: If your app needs to preserve command-line arguments, environment variables, or state across the elevation, make sure to pass them in when restarting.
- **Exit current process**: After spawning the elevated process, you usually call `Environment.Exit(0)` (or equivalent) in the original process so you don’t continue running non-elevated.

### Contributing

Contributions are very welcome! Here are a few ideas for ways to help:

- Add **unit or integration tests** (especially around re-launch logic)
- Support passing command-line arguments or working directory to the elevated process
- Add **logging** (optional) when elevation fails or is declined
- Create example apps (WinForms / WPF / Console) to showcase common use cases
- Add documentation / sample usage in a README or wiki

Feel free to open **issues** or **pull requests** — happy to collaborate.

### Contact

- GitHub: [dannylbr](https://github.com/dannylbr)
- Email: integraesol@gmail.com
- LinkedIn: [Daniel Soares](https://www.linkedin.com/in/daniel-soares-82b71189/)

Thank you for exploring **AdminHelper**!
