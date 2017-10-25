using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowScore : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Text scoreTxt = GetComponent<Text>();
        scoreTxt.text = Score.score.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
