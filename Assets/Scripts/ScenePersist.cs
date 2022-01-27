using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePersist : MonoBehaviour
{
    // On restart of scene: automatically created
    void Awake()
    {
        int numOfScenePersists = FindObjectsOfType<ScenePersist>().Length;
        if (numOfScenePersists > 1)
        {
            // Destroy this gameobject if there is already a ScenePersist (=> ScenePersist = singelton)
            Destroy(gameObject);
        }
        else
        {
            // Never destroy this object on scene restart
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ResetScenePersist() 
    {
        Destroy(gameObject);
    }
}
