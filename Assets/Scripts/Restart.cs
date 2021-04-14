using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class Restart : MonoBehaviour
{
    private System.Random rng = new System.Random();
    public void RestartGame()
    {
        var adChance = rng.Next(0, 4);
        Destroy(GameObject.FindGameObjectWithTag("GameController"));
        SceneManager.LoadScene("StartScene");
        if (Advertisement.IsReady() && adChance < 2)
        {
            Advertisement.Show();
        }
        Debug.Log(adChance);
    }
}
