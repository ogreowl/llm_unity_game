using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button startButton;
    
    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(() => SceneTran()); // Start the coroutine directly

    }

    // Update is called once per frame
    void SceneTran()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
    }
}
