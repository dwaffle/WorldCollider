using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ResetScript : MonoBehaviour {
    public void Reset()
    {
        //Game manager takes care of other objects.
        Destroy(GameObject.FindGameObjectWithTag("PlayerPlanet"));
        GameManagement.lives--;
    }
}
