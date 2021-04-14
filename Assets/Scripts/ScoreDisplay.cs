using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {
	// Use this for initialization
	
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<Text>().text = "Score: " + GameManagement.totalScore.ToString();
	}
}
