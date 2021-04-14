using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LivesDisplay : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<Text>().text = "Lives: " + GameManagement.lives.ToString();
	}
}
