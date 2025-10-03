# EMtools
A collection of useful tools for Windows users, made via Windows Forms, for Hedgehog engine tools, and tools for Windows that make manual work automated.

## Features

### 🗑️ Recycle Bin Cleaner
- Instantly empty your recycle bin - faster than Windows default
- Shows real-time statistics: number of files and total size
- No progress dialogs or delays - instant cleanup
- Confirmation dialog to prevent accidental deletions

### 📦 Batch File Tools
Automate file operations on multiple files at once:
- **Batch Rename**: Add prefix/suffix to multiple files, optional numbering
- **Batch Copy/Move**: Move or copy multiple files to a destination folder
- **Batch Delete**: Delete multiple files at once with confirmation

## Requirements

- Windows OS (7, 8, 10, 11)
- .NET 9.0 Runtime or later

## Building from Source

### Prerequisites
- Visual Studio 2022 or later (with .NET desktop development workload)
- .NET 9.0 SDK or later

### Build Instructions

1. Clone the repository:
```bash
git clone https://github.com/EM20080/EMtools.git
cd EMtools
```

2. Open the solution:
```bash
EMtools.sln
```

3. Build using Visual Studio or command line:
```bash
dotnet build
```

4. Run the application:
```bash
dotnet run --project EMtools/EMtools.csproj
```

Or build a release version:
```bash
dotnet build -c Release
```

The executable will be in `EMtools/bin/Release/net9.0-windows/`

## Usage

1. Launch EMtools.exe
2. Select the tool you want to use from the main menu:
   - **Recycle Bin Cleaner**: Clean your recycle bin instantly
   - **Batch File Tools**: Perform batch operations on multiple files

### Recycle Bin Cleaner
- Click "Refresh Info" to see current recycle bin statistics
- Click "Empty Recycle Bin (Instant)" to clean it
- Confirm the action when prompted

### Batch File Tools
1. Add files using "Add Files" or "Add Folder" buttons
2. Choose an operation from the tabs:
   - **Batch Rename**: Configure prefix/suffix and numbering options
   - **Copy/Move**: Select destination folder and choose copy or move
   - **Delete**: Permanently delete selected files
3. Click the operation button and confirm

## Project Structure

```
EMtools/
├── EMtools.sln              # Solution file
├── .gitignore               # Git ignore file
└── EMtools/                 # Main project
    ├── EMtools.csproj       # Project file
    ├── Program.cs           # Application entry point
    ├── MainForm.cs          # Main menu form
    ├── RecycleBinCleanerForm.cs  # Recycle bin cleaner
    └── BatchToolsForm.cs    # Batch file tools
```

## License

This project is open source and available for personal and commercial use.
