# EzSaverLite
[![PRs Welcome](https://img.shields.io/badge/PRs-welcome-blue)](http://makeapullrequest.com) [![License: MIT](https://img.shields.io/badge/License-MIT-blue)](https://ebukaracer.github.io/ebukaracer/md/LICENSE.html)

A Unity package that provides a flexible way to save and load simple game data.

[View in DocFx](https://ebukaracer.github.io/EzSaverLite)

## Features
- **Multiple Storage Backends**: PlayerPrefs and browser's LocalStorage.
- **Easy Integration**: simple API for saving and loading data.
- **Customizable**: easily extendable to support additional storage backends.
- **Data Types**: supports saving and loading of simple data types.
- **Demo**: includes a Demo project to help you quickly get started.

## Installation
- Open the Unity Package Manager
- Click the (+) button.
- Select **Install package from git URL**.
- Enter the URL below and click **Install**:
   ```text
   https://github.com/ebukaracer/EzSaverLite.git#upm
   ```

If your project uses **Assembly Definitions**, add a reference to this package's assembly under **Assembly Definition References**.

For additional setup information, see the [Setup Guide](https://ebukaracer.github.io/ebukaracer/md/SETUPGUIDE.html)

## Usage Examples

You can then access the singleton instance through:
```
SaverManager.Saver
```
#### Saving Data:
```csharp
using Racer.EzSaverLite.Scripts.Runtime.Core;

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

#### Checking for existence of a key:
``` csharp
bool hasHighscore = SaverManager.Saver.Contains("highscore");
```

#### Clearing Data:
``` csharp
SaverManager.Saver.Clear("highscore");
SaverManager.Saver.ClearAll();
```

## How it works
EzSaverLite uses PlayerPrefs by default in the Unity Editor during development and on most platforms after deployment. For WebGL builds, it uses the browser's `localStorage` via the included WebGL plugin so save data persists between browser sessions.

## Samples and Best Practices
### Importing the Save Plugin
Before exporting a WebGL build,
- navigate to: `Racer > EzSaverLite >  Import WebGL Save Plugin (force)`

To import or update the WebGL save plugin from the menu.
### Importing the Demo
A demo is included with the package and can be imported from the Unity Package Manager's **Samples** tab.
### Removing the Package
To remove the package completely, 
- navigate to: `Racer > EzSaverLite > Remove package`

## [Contributing](https://ebukaracer.github.io/ebukaracer/md/CONTRIBUTING.html)  
Contributions are welcome! Please open an issue or submit a pull request.