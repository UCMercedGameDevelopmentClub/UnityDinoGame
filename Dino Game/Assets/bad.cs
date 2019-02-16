using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bad : MonoBehaviour
{
    Rigidbody2D rb2d;
    Vector2 velocity = new Vector2(-1f ,0f);
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void Update()
    {
        rb2d.MovePosition(rb2d.position + velocity * Time.deltaTime);
    }
}
