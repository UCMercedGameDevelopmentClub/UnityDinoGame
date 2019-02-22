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
        Screen.SetResolution(950, 450, false);
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
