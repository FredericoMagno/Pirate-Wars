using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Menu : MonoBehaviour
{
    private bool op = false;

    [SerializeField] GameObject panelOptions,panelPos;
    [SerializeField] Dropdown sessionTime, timeSpaw;
    void Start()
    {
        
    }

    
    void Update()
    {
        if(op)
        {
            panelOptions.transform.position = panelPos.transform.position;
            op = false;
        }

        PlayerPrefs.SetInt("SessionTime", sessionTime.value);
        PlayerPrefs.Save();

        PlayerPrefs.SetInt("SpawTime", timeSpaw.value);
        PlayerPrefs.Save();

    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Level");
    }

    public void Options()
    {
        op = true;
    }

    public void CloseOptions()
    {
        op = false;
        panelOptions.transform.position = new Vector3(1000, 1000, 1000);
    }
}
