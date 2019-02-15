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
3. Save the scene as `MainScene`

### Creating the Player
1. Inorder to use images as sprites we have to change the assest's import settings.
2. Select `squareboi` in the Project View
3. In the inspector, change `Texture Type` to `Sprite (2D and UI)` and apply
4. Drag `squareboi` from Project view into the hiearchy. This will add it to the current scene.
5. Select `squareboi` in the heiarchy. In the inspector add component Physics2D > `Box Collider 2D`
6. Add component Physics2D > `Rigidbody 2D` to `squareboi`
7. Change the `Body Type` to `Kinematic`
8. Add component Script named `squareboi`

### Scripting the Player

#### Adding gravity
```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class squareboi : MonoBehaviour
{
    Rigidbody2D rb;
    // Start is called before the first frame update
    Vector2 gravity = new Vector2(0.0f, -9.8f);
    void Start()
    {
        rb = gameObject.GetComponent(typeof(Rigidbody2D)) as Rigidbody2D;
    }

    // Update is called once per frame
    void Update()
    {
        rb.MovePosition(rb.position + gravity * Time.deltaTime);
    }
}

```
