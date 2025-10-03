# Contributing to EMtools

Thank you for your interest in contributing to EMtools! This document provides guidelines for contributing to the project.

## Getting Started

1. Fork the repository
2. Clone your fork: `git clone https://github.com/YOUR_USERNAME/EMtools.git`
3. Create a new branch: `git checkout -b feature/your-feature-name`
4. Make your changes
5. Test your changes thoroughly
6. Commit your changes: `git commit -am 'Add some feature'`
7. Push to the branch: `git push origin feature/your-feature-name`
8. Create a Pull Request

## Development Environment

- Windows 10/11
- Visual Studio 2022 or later
- .NET 9.0 SDK or later

## Code Style

- Follow C# naming conventions
- Use meaningful variable and method names
- Add comments for complex logic
- Keep methods focused and concise
- Use proper error handling with try-catch blocks

## Adding New Tools

When adding a new tool to EMtools:

1. Create a new Form class in the `EMtools` project (e.g., `YourToolForm.cs`)
2. Implement the tool's functionality following the pattern of existing tools
3. Add a button in `MainForm.cs` to launch your tool
4. Update the README.md with your tool's description and usage
5. Test thoroughly on different Windows versions if possible

## Testing

- Test all functionality manually on Windows
- Verify error handling works correctly
- Test with different file types and edge cases
- Ensure proper cleanup of resources

## Pull Request Guidelines

- Provide a clear description of the changes
- Include screenshots for UI changes
- Reference any related issues
- Ensure code builds without errors
- Test thoroughly before submitting

## Questions?

Feel free to open an issue for any questions or concerns!
