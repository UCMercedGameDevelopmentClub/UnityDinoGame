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
        speedMultiplier = 1.0f + gm.score/2000;
        if(cooldown <= 0)
        {
            GameObject newO = Instantiate(obstacle, transform);
            cooldown =  Random.Range(2.0f / speedMultiplier, 7.0f / speedMultiplier);
        }
    }
}
