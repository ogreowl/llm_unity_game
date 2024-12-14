using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Needed to manage scenes

public class TransitionToFight : MonoBehaviour
{
    // Name of the scene you want to load
    public string sceneToLoad = "FightOne";


    // This function is called when another collider enters the trigger collider attached to this object
    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is the player
        if (other.CompareTag("Player")) // Make sure the player has a tag named "Player"
        {
            // Load the fight scene
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
