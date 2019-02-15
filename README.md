# UnityDinoGame
UCMGDC's workshop for HackMerced IV

### Welcome
To UCMGDC's Intro to Unity workshop. Today we're going to demostrate the basics of Unity and creating your first project in Unity. All the instructions are on this page, even if you have no experiance coding, you'll be albe to follow along. The completed project can be downloaded above.

### What You'll need
* The latest version of [Unity](https://unity3d.com/get-unity/download)
* Your prefered text or IDE editior to edit your code. If you don't have one installed, we recommend [VS Code](https://code.visualstudio.com/download)

### Creating a new project

![](https://github.com/UCMercedGameDevelopmentClub/UnityDinoGame/blob/master/Screenshots/new_project.png)

1. Open unity (If you're prompted to log in, you can hit skip)
2. Click the `+ New` button
3. Name your  project
4. Select the 2D template

### Unity's UI breakdown
![](https://github.com/UCMercedGameDevelopmentClub/UnityDinoGame/blob/master/Screenshots/layout.png)
1. Hierachy - Lists every `object` (aka GameObject) in the `scene`
    * In this case we have the main camera for the game view and the directional light for illuminating the scene
2. Scene, Game, and Asset Store windows
    * Scene - Interactive view of the current scene. You can select and manipulate objects from the scene tab.
    * Game - Your game running in real time. This tab is stopped until you press the play button, in which case Unity will automatically switch over.
    * Asset Store - Marketplace where you can download tools and assets made by other people.
3. Inspector - Displays information on the currently selected object. You can modify properties of the object's `components`
4. Project and Console
    * Project - Shows all assets belonging to the project.
    * Console - Output of the game. Throws errors at you if you have any.

### Getting Ready
1. Delete the directional light, we will not be using it for this project
2. Switch over to 2D view, the button is located in the `Scene window`
