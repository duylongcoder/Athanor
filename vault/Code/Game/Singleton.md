---
generated: true
type: code
layer: Game
kind: class
namespace: Athanor.Game
file: Athanor/Assets/Scripts/Game/Managers/Singleton.cs
tags: [code/game, kind/class]
aliases: []
---

# Singleton

`public abstract class Singleton` : `MonoBehaviour`
- Layer: **Game** · Source: `Athanor/Assets/Scripts/Game/Managers/Singleton.cs:30-62`

> A manager that exists once, in Main.unity › Managers. There is no lazy creation and no DontDestroyOnLoad: the scene owns it. Instance throws when the manager is not in the scene, so a missing manager fails at once rather than as a null later. Derived classes put their start-up in OnAwake and call base.OnDestroy.

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| property | `public static T Instance` | 35 | The one manager; throws when none is in the scene. |
| property | `public static bool Exists` | 39 | True while a manager of this type is alive. |
| method | `protected void Awake()` | 43 |  |
| method | `protected virtual void OnAwake()` | 56 |  |
| method | `protected virtual void OnDestroy()` | 58 |  |

## Uses
[[Singletons]]

## Used by
[[ContentManager]], [[GameManager]], [[LocalizationManager]], [[ProgressManager]], [[RunManager]], [[SaveManager]], [[TutorialManager]]

## Tested by
[[SingletonTests]]
