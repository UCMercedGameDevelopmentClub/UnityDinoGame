# UnityDinoGame
![](https://github.com/UCMercedGameDevelopmentClub/UnityDinoGame/blob/master/Screenshots/GDC_Logo.png)
Unity Workshop for HackMerced IV<br/>
Hosted by Game Development Club @ UC Merced

### Welcome
To UCMGDC's Intro to Unity workshop. Today we're going to demostrate the basics of Unity and creating your first project in Unity. All the instructions are on this page, even if you have no experiance coding, you'll be albe to follow along. The completed project can be downloaded above.

### What You'll need
* The latest version of [Unity](https://store.unity.com/download?ref=personal)
* For linux users (we have not tested these install instructions): [Unity Linux Install](https://linuxhint.com/install-unity3d-linux/) 
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

### Setting the Game Window
1. Switch over to the `Game` tab , the button is located next to `Scene` tab.
2. To change the resolution, click the `Standalone (1024x768)` dropdown > click the `+` button
3. Change the width to `600` and height to `200`, then press Ok.

### Prepping the Camera
1. Select the `Main Camera` in the hierarchy
2. Set `Projection` to ` Orthographic`
3. Change `Size` to `3`
4. Change  Y value of Transform Position to `1.5`,

### Creating the Player
1. Inorder to use images as sprites we have to change the assest's import settings.
2. Select `squareboi` in the Project View
3. In the inspector, change `Texture Type` to `Sprite (2D and UI)` and apply
4. Drag `squareboi` from Project view into the hiearchy. This will add it to the current scene.
5. Within **Transform**, Set the `X position` to `-8`
6. Select `squareboi` in the hierarchy. In the inspector add component Physics2D > `Box Collider 2D`
7. Add component Physics2D > `Rigidbody 2D` to `squareboi` 
8. Change the `Body Type` to `Dynamic`
9. Set Gravity Scale to `1.4`
10. Add component Script named `squareboi`
11. Edit the script by double clicking it within the Project tab

### Scripting the Player (players.cs)

#### Referencing the RigidBody2D
1. By default none of the components under the game object are accessible in the script. We can aquire a reference to them by using the `GetComponent<ObjectType>()` function. We want to reference the RigidBody2D in the player object so we can apply force for jumps.
```Csharp
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

```Csharp
   void Jump()
   {
      rb2d.AddForce(new Vector2(0.0f, 5.5f), ForceMode2D.Impulse);
   }
```

3. Now in the `Update()` fuction we'll check for player input.
```Csharp
   void Update()
   {
      if (Input.GetKeyDown("space"))
      {
         Jump();
      }
   }
```

### Creating a floor
1. Drag `floor.png` to the hierarchy
2. Set position to x: `-8` and Y:`-0.63`
3. Add a BoxCollider2D and RigidBody2D components to the floor.
4. Set the Body Type to `Static`.
5. Move the Transform of the floor to under the player.

#### Restricting player jumps
Currently the player can jump whenever, where ever. We want to make sure the player can only jump on the ground.

1. Add the following members to the player class.
```Csharp
bool grounded = false;
```

2. In the Update() check the player is grounded as well before jumping
```Csharp
...
if (Input.GetKeyDown("space") && grounded)
...
```

3. Whenever the player jumps, set grounded to false. Add the following code to the Jump() function.
```Csharp
      grounded = false;
```

4. Whenever the player collides with the floor we set grounded to true
```Csharp
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.name == "floor")
        {
            grounded = true;
        }
    }
```

### Creating a game manager
We want a script to manage the game over state as well as counting player score. 
1. In heiarchy, add a Canvas by right-clicking to `UI` > `Canvas`.
2. In the inspector under **Canvas**, we change the Render Mode to `Screen Space - Camera`.
3. Drag the Main Camera within the Heiarchy to Render Camera within the inspector.
4. Select the new Canvas in the heiarchy, create a Text called *score* by right-clicking to `UI` > `Text`.
5. Within the inspector change the text under **Text(Script)** to `High: 0000 Score: 0000` to display both the high score and current score.
6. Within the scene, move the `Text` game object to the top left of the camera.
7. Create a empty game object within heiarchy called `gameManager`by right clicking to `Create Empty`.
8. Create script gameManager and attach to the gameManager object.

#### gameManager.cs
1. To program the game manager, we need a special library for UI:
```Csharp
  using UnityEngine.UI
```
2. Declare Text scoreDisplay as a SerializeField. This is what we use to reference the score object in the scene. We also want a variable to track current score and high score.
```Csharp
  

    [SerializeField]
    Text scoreDisplay;
    public float high = 0.0f;
    public float score = 0.0f;

```
3. In Update() we increment the player score
```Csharp
 score += Time.deltaTime * 100;
scoreDisplay.text = "High: " + Mathf.RoundToInt(high).ToString() + " Score: " + Mathf.RoundToInt(score).ToString();
```

4. We add a gameOver() function that resets the score when the player loses
```Csharp
    public void gameOver(){
        if(high < score){
            high = score;
        }
        score = 0.0f;
    }
```

#### Window size
We can restrict the window size of the exported game with `Screen.SetResolution()`
1. Under the Start() function, add `Screen.SetResolution(600, 200, false)`;

### Obstacles!

1. Drag `obstacle.png` to the hierarchy
2. Add a BoxCollider2D and RigidBody2D components to the floor.
3. Set Rigidbody2D Body Type to kinematic.

#### Triggers
4. Add a BoxCollider2D
5. Check `Is Trigger`
6. Make a new script `obstacle

##### Obstacle.cs
1. Add class member `Rigidbody2D rb2d`, `gameManager gm` and `Vector2 velocity;
2. Get the rigidbody and gameManager in `Start()` 
```Csharp
rb2d = GetComponent<Rigidbody2D>();
gm = GameObject.Find("gameManager").GetComponent<gameManager>();

```
3. Use function OnTriggerEnter2D to detect collisions with the player
```Csharp
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "squareboi")
        {
            gm.gameOver();
        }
    }
```

#### Moving the obstacles / moveLeft.cs
1. Attach new script `moveLeft` to Obstacle
2. Add class member rb2d and gm
```Csharp
...
Rigidbody2D rb2d;
gameManager gm;
...
void Start()
{
   gm = GameObject.Find("gameManager").GetComponent<gameManager>();
   rb2d = GetComponent<Rigidbody2D>(); 
}
```
3. Add class member `speed` as a float and initialize as 7.0f. Above, declare the variable as a [SerializeField]
```Csharp
    [SerializeField]
    float speed = 7.0f;
    Vector2 velocity;
```
4. Initlaize velocity in `Start()`
`velocity = new Vector2(-speed, 0f);`
5. In the Update() function move the rigidbody.
```Csharp
rb2d.MovePosition(rb2d.position + velocity * Time.deltaTime);
```

### Prefabs!
Prefabs are packaged objects that you can spawn during run time.
1. Create folder `Resources`
2. Drag `obstacle` from the hierarchy into the `Resources` folder

### Spawning obstacles
We want to create a object that spawns obstacles for the player.

1. Create empty object `spawner` in the hierarchy
2. Set the X position to 10
3. make a new script `spawner` and attach to the object.

#### spawner.cs

We want a variable to store the obstacles to instantiate.
We also need a cooldown period everytime we spawn an obstacle

1. Add the following class members
```Csharp
    GameObject obstacle;
    float cooldown = 1.0f;
```

2. We need to load the obstacle prefab to  `obstacle` in the Start() function
`obstacle = Resources.Load("badie") as GameObject;`

3. Now we have to create a timer and spawn a new obstacle when the time reaches 0. Add the following to the Update() function. We can add variety by randomizing the cooldown times for the next spawn.
```
        cooldown -= Time.deltaTime;
        if(cooldown <= 0)
        {
            GameObject newO = Instantiate(obstacle, transform);
            cooldown =  Random.Range(0.5f, 3.0f);
        }
    }
```

Now you have completed the basic mechanics of the dino game. There are some things we still have to do before the game is finished.

### Increasing Difficulty
We want the game to get harder as the player accumulates score. By increasing obstacle speed and spawn rate we can make the game harder.

#### Making obstacles faster
1. Add speedMultiplier to move.cs
`float speedMultiplier = 1.0f;`

2. In the `Start()` function, calculate `speedMultiplier` from the score in gm. 

`speedMultiplier = 1.0f + gm.score / 3000;`
3. Change the way velocity is calculated
`velocity =  new Vector2(-speed * speedMultiplier, 0f);`

#### Overclocking the Spawner
We'll edit spawner.cs as well to spawn things faster.
1. Add `speedMultiplier` to the spawner class.
`float speedMultiplier = 1.0f;`

2. We need to access the score in gameManager as well.
`gameManager gm;`

3. In `Start()`:
  `gm = GameObject.Find("gameManager").GetComponent<gameManager>();`
  
4. In `Update()`, calculate the speedMultiplier and modify the cooldown
```Csharp
...
        speedMultiplier = 1.0f + gm.score/7000;
        if(cooldown <= 0)
        {
            ...
            cooldown =  Random.Range(0.5f / speedMultiplier, 3.0f / speedMultiplier);
        }
...
```

#### High jump
We'll give the player extra control over their jump. If they hold the spacebar they can jump higher.

We'll enhance player.cs

1. Add the following class members.
```Csharp
    float holdDuration = 0.1f;
    float currHold = 0.0f;
    bool highJump = false;
```
2. Add the following to the Jump() function
```Csharp
        highJump = true;
        currHold = 0.0f;

```
3. In the update function, check if the player has released the jump button. If they have, they can no longer high jump for the current jump.
```Csharp
        if (Input.GetKeyUp("space"))
        {
            longJump = false;
        }
```
4. if not grounded:
```Csharp

        if (!grounded)
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
```

### Final Note
Congratulations on completing your first game in Unity! If you are interested in learning more about Unity and the game industry overall please join UC Merced's Game Development Club. <br/>

Join our social media groups for more information:
* [Discord](https://discord.gg/v5SR9ca)
* [Facebook](https://www.facebook.com/groups/269175380309064/?ref=bookmarks)
