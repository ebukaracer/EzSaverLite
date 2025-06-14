# EzSaverLite
[![PRs Welcome](https://img.shields.io/badge/PRs-welcome-blue)](http://makeapullrequest.com) [![License: MIT](https://img.shields.io/badge/License-MIT-blue)](https://ebukaracer.github.io/ebukaracer/md/LICENSE.html)

**EzSaverLite** is a Unity package that provides a flexible way to save and load game data. It supports multiple storage backends, including PlayerPrefs and browser local storage for WebGL builds.

[View in DocFx](https://ebukaracer.github.io/EzSaverLite)
## Features
- **Multiple Storage Backends**: Supports PlayerPrefs and browser's LocalStorage.
- **Easy Integration**: Simple API for saving and loading data.
- **Customizable**: Easily extendable to support additional storage backends.
- **Data Types**: Supports saving and loading integers, floats, strings, and Booleans.
- **Demo**: Includes a Demo to help you quickly get started.

## Installation
_Inside the Unity Editor using the Package Manager:_
- Click the **(+)** button in the Package Manager and select **"Add package from Git URL"** (requires Unity 2019.4 or later).
-  Paste the Git URL of this package into the input box: https://github.com/ebukaracer/EzSaverLite.git#upm
-  Click **Add** to install the package.
-  If your project uses **Assembly Definitions**, make sure to add a reference to this package under **Assembly Definition References**. 
    For more help, see [this guide](https://ebukaracer.github.io/ebukaracer/md/SETUPGUIDE.html).

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
**EzSaverLite** uses `PlayerPrefs` by default in the Unity editor during development and for all platforms after deployment, except for WebGL. In the case of WebGL, it relies on the browser's `localstorage` to save and load data. Additionally, when multiple builds of the same project (with the same name) are created, the data persists across those builds.

## Samples and Best Practices
- Before deploying your WebGL project, navigate to the menu option: `Racer > EzSaverLite > Import WebGL Save Plugin(Force)` to import or update the LSS(Local Storage Saver) plugin, which will be used for saving and loading in the browser after deployment.
- Optionally import this package's demo from the package manager's `Samples` tab.
- To remove this package completely(leaving no trace), navigate to: `Racer > EzSaverLite > Remove package`

## [Contributing](https://ebukaracer.github.io/ebukaracer/md/CONTRIBUTING.html)  
Contributions are welcome! Please open an issue or submit a pull request.