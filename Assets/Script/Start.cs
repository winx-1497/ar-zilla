using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Start: MonoBehaviour
{
    // Start is called before the first frame update
    public void StarttheGame()
    {
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
