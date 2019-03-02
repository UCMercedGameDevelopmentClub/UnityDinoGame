# UnityDinoGame
UCMGDC's workshop for HackMerced IV

### Welcome
To UCMGDC's Intro to Unity workshop. Today we're going to demostrate the basics of Unity and creating your first project in Unity. All the instructions are on this page, even if you have no experiance coding, you'll be albe to follow along. The completed project can be downloaded above.

### What You'll need
* The latest version of [Unity](https://store.unity.com/download?ref=personal)
* Your prefered text or IDE editior to edit your code. If you don't have one installed, we recommend [VS Code](https://code.visualstudio.com/download)

### Creating a new project

![](https://github.com/UCMercedGameDevelopmentClub/UnityDinoGame/blob/master/Screenshots/new_project.png)

1. Open unity (If you're prompted to log in, you can hit skip)
2. Click the `+ New` button
3. Name your project
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
8. Set Gravity Scale to `1.4`
9. Add component Script named `squareboi`

### Scripting the Player (players.cs)

#### Adding gravity
```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class squareboi : MonoBehaviour
{
    Rigidbody2D rb2d;
    // Start is called before the first frame update
    bool falling = true;
    bool longJump = false;
    [SerializeField]
    float holdDuration = 0.1f;
    float currHold = 0.0f;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonUp(0) || Input.GetKeyUp("space"))
        {
            longJump = false;
        }

        if (falling)
        {
            if ((Input.GetMouseButton(0) || Input.GetKey("space")) && longJump){
                currHold += Time.deltaTime;
                if(currHold >= holdDuration)
                {
                    rb2d.AddForce(new Vector2(0.0f, 3.25f), ForceMode2D.Impulse);
                    longJump = false;
                }
            }
        }

        else if (Input.GetMouseButtonDown(0) || Input.GetKeyDown("space"))
        {
            Jump();
            falling = true;
            longJump = true;
            currHold = 0.0f;
        }

    }

    void Jump()
    {
        rb2d.AddForce(new Vector2(0.0f, 5.5f), ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
      
        if(col.gameObject.name == "floor")
        {
            falling = false;
        }
        else if(col.gameObject.name == "bad")
        {
            //gameover
        }
        
    }

    void onTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "bad")
        {
            Debug.Log("ow");
        }
    }


}
```

#### Adding a scoredboard 
```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class gameManager : MonoBehaviour
{
    [SerializeField]
    Text scoreDisplay;
    
    public float high = 0.0f;
    public float score = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(600, 200, false);
    }

    // Update is called once per frame
    void Update()
    {
        score += Time.deltaTime * 100;
        scoreDisplay.text = "High: " + Mathf.RoundToInt(high).ToString() + " Score: " + Mathf.RoundToInt(score).ToString();
    }

    public void gameOver(){
        if(high < score){
            high = score;
        }
        score = 0.0f;
    }
}
```
