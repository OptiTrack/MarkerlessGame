# Contributing to This Project

Thank you for your interest in contributing to this project! This document provides guidelines and instructions for contributing to the project as well as how to get started. 

## Table of Contents

- [Prerequisites](#prerequisites)c
- [Local Setup](#local-setup)
- [Development Workflow](#development-workflow)
- [Running CI Locally](#running-ci-locally)
- [Code Quality](#code-quality)
- [Contribution Process](#contribution-process)
- [Code Review](#code-review)
- [Reporting Bugs](#reporting-bugs)
- [Getting Help](#getting-help)

## Prerequisites

### Required Software

- **Unity** - Game Engine
- **Git** - For version control

### Optional Tools

- **Visual Studio Code** - Alternative editor with CMake/C++ extensions
- **clang-format** - For code formatting (optional but recommended)

## Local Setup

### 1. Clone the Repository

```bash
git clone git@github.com:OptiTrack/MarkerlessGame.git
cd MarkerlessGame
```

### 2. Install Unity

1. Download Unity Hub
2. Create a free Unity account or log in with an existing one
3. Download editor version 6000.5.2f1

### 3. Install Motive

Install Motive
Get a license
Run Motive
Do XXX steps to get tracking running. 

### 4. Connect tracking in game to Motive

[Instructions]

### 6. Run the Application

[Instructions]

## Development Workflow

### Branch Naming Convention

- `dev` - Primary development branch
- `main` - Primary release branch

(Optional)
Use descriptive branch names following this pattern:
- `feature/<description>` - For new features
- `fix/<description>` - For bug fixes
- `refactor/<description>` - For code refactoring
- `docs/<description>` - For documentation updates
- `test/<description>` - For test additions/improvements

**Examples:**
- `feature/add-waypoint-import`
- `fix/camera-connection-timeout`
- `refactor/consolidate-network-handlers`
- `docs/update-api-reference`

### Commit Messages

Follow the conventional commits format:

```
<type>: <short summary>

<detailed description (optional)>

<footer (optional)>
```

**Types:**
- `feat:` - New feature
- `fix:` - Bug fix
- `docs:` - Documentation changes
- `refactor:` - Code refactoring
- `test:` - Adding or updating tests
- `chore:` - Maintenance tasks
- `style:` - Code style/formatting changes

**Example:**
```
feat: Add flight path export to JSON

Implement functionality to export flight paths to JSON format
including waypoints, metadata, and coordinate system information.

Closes #42
```

## Running CI Locally

The project uses GitHub Actions for continuous integration. You can verify your changes will pass CI by running the build process locally.

### Build Verification

[Instructions]

### Manual Testing Checklist

Before submitting a PR, verify:
- [ ] Application launches without crashes
- [ ] No compile warnings in your changes
- [ ] UI elements render correctly
- [ ] Network connections work (VOXL 2 or simulation mode)
- [ ] 3D waypoint visualization renders properly
- [ ] No memory leaks (use Qt Creator's analyzer)

## Code Quality

### Code Style

- Use **4 spaces** for indentation (no tabs)
- Follow **Qt coding conventions** for C++ code
- Keep lines under **120 characters** where practical
- Use **meaningful variable and function names**
- Add **comments** for complex logic

### File Organization

Below is the folder structure of the project
Any folders that aren't mentioned are managed by the Unity Engine and should not be directly modified.
```
project/
├── src/MarkerlessGame/
│   ├── Assets/
│   │   ├── CustomAssets/
│   │   │   ├── Audio/       # Music and SFX
│   │   │   ├── GameObjects/ # 3D models and their animations
│   │   │   ├── Prefabs/     # Game objects with scripts attached and options configured
│   │   │   ├── Scenes/      # Worlds that have game objects placed in them
│   │   │   ├── Scripts/     # C# scripts containing most of the game logic
│   │   │   ├── Tests/       # Unit and Integration tests
│   │   │   ├── UI/          # 2D images used on a canvas to make the UI
│   │   ├── UnityAssets/     # Game Objects, materials and scripts provided by Unity, may be useful
├── docs/                    # Documentation

```

## Contribution Process

### 1. Create a Feature Branch

```bash
git checkout -b feature/your-feature-name
```

### 2. Make Your Changes

- Write clean, maintainable code
- Follow the code style guidelines
- Add tests if applicable
- Update documentation as needed

### 3. Test Your Changes

- Build and run the application
- Verify all existing functionality still works
- Test your new feature/fix thoroughly

### 4. Commit Your Changes

```bash
git add .
git commit -m "feat: Add your feature description"
```

### 5. Push to GitHub

```bash
git push -u origin feature/your-feature-name
```

### 6. Open a Pull Request

**PR Title:** Clear and descriptive (50 characters or less)

**PR Description Should Include:**
- **What:** Brief description of the changes
- **Why:** Reason for the changes
- **How:** Implementation approach (if complex)
- **Testing:** How you tested the changes
- **Screenshots:** For UI changes
- **Closes:** Reference to any related issues (e.g., `Closes #42`)


### Definition of Done (DoD)

[Fill In]
A contribution is considered complete when:

- [ ] Code builds without errors or warnings
- [ ] All new code follows project style guidelines
- [ ] Public APIs are documented
- [ ] Changes have been tested manually
- [ ] No existing functionality is broken
- [ ] UI changes are responsive and match design guidelines
- [ ] PR description is complete and clear
- [ ] Code has been reviewed and approved
- [ ] All review comments are addressed
- [ ] CI build passes on GitHub Actions

## Code Review

### Review Expectations

**For Contributors:**
- Respond to review comments within 2 business days
- Be open to feedback and suggestions
- Ask questions if feedback is unclear
- Update your PR based on review comments

**For Reviewers:**
- Provide constructive, specific feedback
- Review within 2 business days of PR submission
- Test functionality when possible
- Approve or request changes clearly

### What Reviewers Look For

- **Correctness:** Does the code work as intended?
- **Quality:** Is the code clean, maintainable, and well-structured?
- **Style:** Does it follow project conventions?
- **Safety:** Are there potential bugs, crashes, or memory leaks?
- **Performance:** Are there any performance concerns?
- **Documentation:** Is the code adequately documented?
- **Testing:** Has it been appropriately tested?

## Reporting Bugs

### Where to Report

Report bugs by creating a new issue on GitHub:
**(https://github.com/OptiTrack/MarkerlessGame/issues/new)

### Bug Report Template

When reporting bugs, please include:

**Title:** Clear, descriptive summary (e.g., "Camera feed freezes after 5 minutes")

**Description:**
```markdown
## Description
Clear description of the bug

## Steps to Reproduce
1. Step one
2. Step two
3. Step three

## Expected Behavior
What should happen

## Actual Behavior
What actually happens

## Environment
- OS: Windows 11 / Ubuntu 22.04
- Build Type: Debug / Release

## Screenshots/Logs
[Add screenshots or log output if applicable]



## Getting Help

### Communication Channels

**For Questions:**
- **Issues:** For bug reports and feature requests only


---

**Thank you for contributing!**
