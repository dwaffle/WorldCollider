using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameScr : MonoBehaviour {
    
public void StartGame()
    {
        GameManagement.totalScore = 0;
        GameManagement.level = 1;
        GameManagement.lives = 6;
        GameManagement.totalLevels++;
        GameManagement.toOneUp = 100;
        SceneManager.LoadScene("WorldCollider");
    }
}
