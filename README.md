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

### Prepping the Camera
1. Select the `Main Camera` in the hierarchy
2. Set `Projection` to ` Orthographic`
3. Change `Size` to `3`
4. Change  Y value of Transform Position to 1.5,

### Creating the Player
1. Inorder to use images as sprites we have to change the assest's import settings.
2. Select `squareboi` in the Project View
3. In the inspector, change `Texture Type` to `Sprite (2D and UI)` and apply
4. Drag `squareboi` from Project view into the hiearchy. This will add it to the current scene.
5. Select `squareboi` in the hierarchy. In the inspector add component Physics2D > `Box Collider 2D`
6. Add component Physics2D > `Rigidbody 2D` to `squareboi` 
7. Change the `Body Type` to `Dynamic`
8. Set Gravity Scale to `1.4`
9. Add component Script named `squareboi`
10. Edit the script by double clicking it

### Scripting the Player (players.cs)

#### Referencing the RigidBody2D
1. By default none of the components under the game object are accessible in the script. We can aquire a reference to them by using the `GetComponent<ObjectType>()` function. We want to reference the RigidBody2D in the player object so we can apply force for jumps.
```
...
public class squareboi : MonoBehaviour
{
    Rigidbody2D rb2d;
    ...
     void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    ...
```

#### Adding Jumps
2. We'll add a additional function to the player class for jumping. We'll use `AddForce()` on the rigidbody to apply upward foce  on the player.

```
   void Jump()
   {
      rb2d.AddForce(new Vector2(0.0f, 5.5f), ForceMode2D.Impulse);
   }
```

3. Now in the `Update()` fuction we'll check for player input.
```
   void Update()
   {
      if (Input.GetMouseButtonDown(0) || Input.GetKeyDown("space"))
      {
         Jump();
      }
   }
```

### Creating a floor
1. Drag `floor.png` to the hierarchy
2. Add a BoxCollider2D and RigidBody2D components to the floor.
3. Set the Body Type to Static.
4. Move the Transform of the floor to under the player.

#### Restricting player jumps
Currently the player can jump whenever, where ever. We want to make sure the player can only jump on the ground.

1. Add the following members to the player class.
```
bool grounded = false;
```

2. In the Update() check the player is grounded as well before jumping
```
...
if (Input.GetKeyDown("space") && grounded)
...
```

3. Whenever the player collides with the floor we set grounded to true
```
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.name == "floor")
        {
            grounded = true;
        }
    }
```

4. Whenever the player jumps, set grounded to false. Add the following code to the Jump() function.
```
      grounded = false;
```

### Adding gravity
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
### adding an obstacle

'''
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    // Start is called before the first frame update

    GameObject obstacle;
    float cooldown = 1.0f;

    gameManager gm;

    float speedMultiplier = 1.0f;

    void Start()
    {
        gm = GameObject.Find("gameManager").GetComponent<gameManager>();
        obstacle = Resources.Load("badie") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        cooldown -= Time.deltaTime;
        speedMultiplier = 1.0f + gm.score/7000;
        if(cooldown <= 0)
        {
            GameObject newO = Instantiate(obstacle, transform);
            cooldown =  Random.Range(0.5f / speedMultiplier, 3.0f / speedMultiplier);
        }
    }
}
'''

### Adding a game manager 
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



