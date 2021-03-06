﻿using System.Collections;
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
        
    }

}
