using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{

    [SerializeField] Text title,scoreGame,scoretxt;
    [SerializeField] GameObject FinalPanel,panelPos;
    void Start()
    {
        
    }

    
    void Update()
    {
        if(Player.lose == true)
        {
            title.text = "YOU LOSE";
            MovePanel();
        }
        if(Player.win == true)
        {
            title.text = "YOU SURVIVED";
            MovePanel();
        }
        scoreGame.text = Player.score.ToString();
        scoretxt.text = Player.score.ToString();
    }

    private void MovePanel()
    {
        FinalPanel.transform.position = panelPos.transform.position;
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

}
