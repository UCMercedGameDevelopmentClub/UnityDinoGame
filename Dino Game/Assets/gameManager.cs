using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class gameManager : MonoBehaviour
{
    [SerializeField]
    Text scoreDisplay;
    
    public float score = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(950, 450, false);
    }

    // Update is called once per frame
    void Update()
    {
        score += Time.deltaTime * 100;
        scoreDisplay.text = "Score: " + Mathf.RoundToInt(score).ToString();
    }

    public void gameOver(){
        score = 0.0f;
    }
}
