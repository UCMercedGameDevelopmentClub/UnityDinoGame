using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bad : MonoBehaviour
{
    Rigidbody2D rb2d;
    [SerializeField]
    float speed = 3.0f;

    float speedMultiplier = 1.0f;

    gameManager gm;

    Vector2 velocity;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("gameManager").GetComponent<gameManager>();
        rb2d = GetComponent<Rigidbody2D>(); 
        speedMultiplier += gm.score / 1000;
        velocity =  new Vector2(-speed * speedMultiplier,0f);
  
    }

    // Update is called once per frame
    void Update()
    {
        rb2d.MovePosition(rb2d.position + velocity * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "squareboi")
        {
            gm.gameOver();
        }
    }
    
}
