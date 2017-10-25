using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {
    public static int score=0;
    private Text myText; 
	// Use this for initialization
	void Start () {
        myText = GetComponent<Text>();
        myText.text = score.ToString();
	}
	public void Reset()
    {
        score = 0;
        myText.text = score.ToString();
    }
    public void addScore(int point)
    {
        score += point;
        myText.text = score.ToString();
    }
	// Update is called once per frame
	
}
