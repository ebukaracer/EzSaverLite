# EzSaverLite
[![PRs Welcome](https://img.shields.io/badge/PRs-welcome-blue)](http://makeapullrequest.com) [![License: MIT](https://img.shields.io/badge/License-MIT-blue)](https://ebukaracer.github.io/ebukaracer/md/LICENSE.html)

**EzSaverLite** is a Unity package that provides a flexible way to save and load game data. It supports multiple storage backends, including PlayerPrefs and browser local storage for WebGL builds.

[Read Docs](https://ebukaracer.github.io/EzSaverLite)
## Features
- **Multiple Storage Backends**: Supports PlayerPrefs and browser's LocalStorage.
- **Easy Integration**: Simple API for saving and loading data.
- **Customizable**: Easily extendable to support additional storage backends.
- **Data Types**: Supports saving and loading integers, floats, strings, and Booleans.
- **Demo**: Includes a Demo to help you quickly get started.

## Installation
 *In unity editor inside package manager:*
- Hit `(+)`, choose `Add package from Git URL`(Unity 2019.4+)
- Paste the `URL` for this package inside the box: https://github.com/ebukaracer/EzSaverLite.git#upm
- Hit `Add`
- If you're using assembly definition in your project, be sure to add this package's reference under: `Assembly Definition References` or check out [this](https://ebukaracer.github.io/ebukaracer/md/SETUPGUIDE.html)

## Quick Usage
#### Saving Data:
```csharp
SaverManager.Saver.SaveInt("highscore", 100);
SaverManager.Saver.SaveFloat("volume", 0.75f);
SaverManager.Saver.SaveString("playerName", "Racer");
SaverManager.Saver.SaveBool("isMusicOn", true);
```

#### Loading Data:
``` csharp
// With default values set to the second argument
int highscore = SaverManager.Saver.GetInt("highscore", 0);
float volume = SaverManager.Saver.GetFloat("volume", 1.0f);
string playerName = SaverManager.Saver.GetString("playerName", "Guest");
bool isMusicOn = SaverManager.Saver.GetBool("isMusicOn", false);
```

#### Checking for existence of Data:
``` csharp
bool hasHighscore = SaverManager.Saver.Contains("highscore");
```

#### Clearing Data:
``` csharp
SaverManager.Saver.Clear("highscore");
SaverManager.Saver.ClearAll();
```

## How it works
 **EzSaverLite** by default, uses `playerprefs` inside the unity editor during development and post-deployment to other platforms except for WebGL, which in this case relies on the browser's `local storage` for saving and loading data. When multiple builds of the same project(name) are made, data is also persisted along.

## Samples and Best Practices
Check out this package's sample scene by importing it from the package manager *sample's tab* and exploring the script for the recommended approach for saving and loading data easily.

*To remove this package completely(leaving no trace), navigate to: `Racer > EzSaverLite > Remove package`*

## [Contributing](https://ebukaracer.github.io/ebukaracer/md/CONTRIBUTING.html)  
Contributions are welcome! Please open an issue or submit a pull request.