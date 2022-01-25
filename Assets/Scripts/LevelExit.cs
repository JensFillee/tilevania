using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float loadLevelDelay = 1f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(loadNextLevel());
        }
    }

    // Coroutines: used to create a delay
    IEnumerator loadNextLevel()
    {
        // do immediately when method is called (BEFORE WaitForSecondsRealtime(loadNextLevelDelay);)


        // wait for WaitForSecondsRealtime(loadNextLevelDelay) to finish (async, in meantime: other things can happen (in other methods))
        yield return new WaitForSecondsRealtime(loadLevelDelay);


        // do AFTER WaitForSecondsRealtime(loadNextLevelDelay);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        // If this is the last level
        if (nextSceneIndex == SceneManager.sceneCount)
        {
            // next level = level 1
            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
