---
generated: true
type: code
layer: Editor
kind: class
namespace: -
file: Athanor/Assets/Editor/WebBuild.cs
tags: [code/editor, kind/class]
aliases: []
---

# WebBuild

`public static class WebBuild`
- Layer: **Editor** · Source: `Athanor/Assets/Editor/WebBuild.cs:10-57`

> Builds the game for the browser into Build/web. Also callable from the command line: unity build Athanor --target WebGL --execute-method WebBuild.Build

## Members
| Kind | Signature | Line | Note |
|---|---|---|---|
| field | `public const string Output = "Build/web", DevOutput =` | 12 |  |
| method | `public static void Build()` | 15 |  |
| method | `public static void BuildQuick()` | 19 | Same game, built in a fraction of the time and a little larger: for checking changes in a browser. |
| method | `public static void BuildDev()` | 26 | The quick build as a development build, where F9 hands the bench to the scripted player (tools/web/autoplay.mjs). It goes to a folder of its own, so it never replaces the build that is to be published. |
