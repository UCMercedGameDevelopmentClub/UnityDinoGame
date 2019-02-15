using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class squareboi : MonoBehaviour
{
    Rigidbody2D rb;
    // Start is called before the first frame update
    Vector2 gravity = new Vector2(0.0f, -9.8f);
    bool falling = false;
    void Start()
    {
        rb = gameObject.GetComponent(typeof(Rigidbody2D)) as Rigidbody2D;
    }

    // Update is called once per frame
    void Update()
    {
        if (falling)
            rb.MovePosition(rb.position + gravity * Time.deltaTime);

        if (Input.GetKeyDown("space"))
        {
            Jump();
        }
        falling = true;
    }

    void Jump()
    {
        rb.MovePosition(rb.position + new Vector2(0.0f, 5.0f));
    }

    void OnCollisionStay(Collision collisionInfo)
    {
        foreach (ContactPoint contact in collisionInfo.contacts)
        {
            if(contact.otherCollider.gameObject.name == "floor")
            {
                falling = false;
            }
        }
    }



}
