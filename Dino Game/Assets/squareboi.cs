using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class squareboi : MonoBehaviour
{
    Rigidbody2D rb2d;
    // Start is called before the first frame update
    bool falling = true;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (falling)
        {

        }

        else if (Input.GetKeyDown("space"))
        {
            Jump();
            falling = true;
        }

    }

    void Jump()
    {
        rb2d.AddForce(new Vector2(0.0f, 500.0f));
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
