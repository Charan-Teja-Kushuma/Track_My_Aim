# Track_My_Aim — 3D FPS Aim Trainer

A first-person shooter (FPS) prototype built with Unity, focused on responsive player movement, mouse-look controls, aiming down sights (ADS), raycast-based shooting, ammunition management, and target practice.

**Repository:** https://github.com/Charan-Teja-Kushuma/Track_My_Aim  
**Development branch:** `recover-main`  
**Engine:** Unity 6

## Overview

Track_My_Aim is a playable FPS training prototype designed to demonstrate core first-person gameplay systems in a simple training arena. Players can move around the arena, aim with the mouse, fire at targets, manage ammunition, reload, and use ADS zoom for more precise aiming.

## Features

- **First-person movement:** WASD movement, sprinting, jumping, gravity, and grounded movement using Unity's Character Controller.
- **Mouse look:** First-person camera control with vertical pitch limits and cursor locking.
- **Aim Down Sights (ADS):** Camera zoom for more precise aiming.
- **Raycast shooting:** Fires from the camera's aiming direction and detects hits.
- **Target damage:** Targets receive damage and can be destroyed when their health is depleted.
- **Ammunition and reloading:** Ammo counter and reload input.
- **Crosshair feedback:** Crosshair changes appearance based on whether the latest shot hit a target.
- **Weapon effects:** Muzzle flash, impact effects, and bullet-tracer visuals.
- **Training arena:** A dedicated scene with a player, weapon, target, and arena environment.

## Controls

| Input | Action |
|---|---|
| `W` `A` `S` `D` | Move |
| Mouse | Aim / look around |
| `Shift` | Sprint |
| `Space` | Jump |
| Left Mouse Button | Shoot |
| `R` | Reload |
| Right Mouse Button | Aim Down Sights (ADS) |
| `Esc` | Unlock the cursor |

*Note: Controls reflect the implemented prototype configuration. If your local Input Actions or Inspector settings differ, use the bindings configured in the project.*

## Technology Stack

- **Game engine:** Unity 6
- **Language:** C#
- **Movement:** Character Controller
- **Input:** Unity Input System
- **Shooting:** Physics raycasting
- **Version control:** Git and GitHub

## Project Structure

```text
Assets/
├── _Project/
│   ├── Scripts/       # Player, camera, weapon, target, and effect scripts
│   ├── Prefabs/       # Training target and weapon-effect prefabs
│   ├── Scenes/        # TrainingArena.unity
│   └── Materials/     # Project materials
├── LowPolyWeapons_LITE/  # Imported weapon assets
└── Sector Warzone/       # Imported environment assets
```

Unity may create additional asset and metadata files in these directories.

## Getting Started

### Requirements

- Unity 6 compatible with the project version
- Git (optional, if cloning the repository)

### Clone the repository

```bash
git clone https://github.com/Charan-Teja-Kushuma/Track_My_Aim.git
cd Track_My_Aim
git checkout recover-main
```

### Open in Unity

1. Open Unity Hub.
2. Select **Add** or **Open** and choose the cloned `Track_My_Aim` project folder.
3. Open the project with a compatible Unity 6 Editor version.
4. In the Project window, open `Assets/_Project/Scenes/TrainingArena.unity`.
5. Press **Play** to test the prototype.

If Unity prompts you to import or resolve packages, allow it to finish and check the Console for errors before playing.

## Gameplay Test Checklist

- [ ] WASD movement, sprint, and jump work.
- [ ] Mouse look and cursor locking work.
- [ ] ADS zoom works.
- [ ] Shooting hits targets and applies damage.
- [ ] Ammo decreases when firing and reload restores ammunition.
- [ ] Crosshair hit feedback works.
- [ ] Muzzle flash, impact effects, and tracer visuals appear as configured.

## Current Scope

This repository contains a playable FPS prototype and training arena. It focuses on core mechanics rather than a complete commercial game. Additional training modes, menus, scoring, and progression can be added in future iterations.

## Future Improvements

- Tracking, target-switching, and reaction-time training modes
- Score, accuracy, and time statistics
- Adjustable training settings and target behavior
- Improved weapon and firing animations
- Main menu and in-game settings
- Standalone builds for supported platforms

## Author

**Charan Teja Kushuma**

## License and Third-Party Assets

Check the license terms of the included imported asset packs before redistributing the project or its assets. Add a project license file if you intend to publish the project under a specific open-source license.
