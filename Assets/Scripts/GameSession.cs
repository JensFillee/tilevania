using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;

    // On restart of scene: automatically created
    void Awake()
    {
        int numOfGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numOfGameSessions > 1)
        {
            // Destroy this gameobject if there is already a GameSession (=> GameSession = singelton)
            Destroy(gameObject);
        }
        else
        {
            // Never destroy this object on scene restart
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    // Restart level
    void TakeLife()
    {
        playerLives--;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Restart game
    void ResetGameSession()
    {
        SceneManager.LoadScene(0);

        // Destroy this object (singleton)
        Destroy(gameObject);
    }

}
