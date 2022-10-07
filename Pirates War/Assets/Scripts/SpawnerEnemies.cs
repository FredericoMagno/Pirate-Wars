using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnerEnemies : MonoBehaviour
{
    [SerializeField] private float timeToSpawner;
    [SerializeField] GameObject[] enemies, pointsToSpawner;
    [SerializeField] Text TimeGame_txt;
    private float durationGame=100, timerGame;

    void Start()
    {
        InvokeRepeating("Spawner", 0, timeToSpawner);

        switch(PlayerPrefs.GetInt("SpawTime"))
        {
            case 0:
                timeToSpawner = 3;
                break;
            case 1:
                timeToSpawner = 5;
                break;
            case 2:
                timeToSpawner = 7;
                break;
        }

        switch(PlayerPrefs.GetInt("SessionTime"))
        {
            case 0:
                durationGame = 60;
                break;
            case 1:
                durationGame = 120;
                break;
            case 2:
                durationGame = 180;
                break;
        }

        print(durationGame);
        print(timeToSpawner);
    }

    
    void Update()
    {
        timerGame = timerGame + Time.deltaTime;
        TimeGame_txt.text = timerGame.ToString("0");

        if(timerGame>=durationGame)
        {
            CancelInvoke();
            Player.win = true;
        }

        
    }

    private void Spawner()
    {
        if (timerGame < durationGame)
        {
            Instantiate(
            enemies[Random.Range(0, 2)],
            pointsToSpawner[Random.Range(0, 10)].transform.position,
            Quaternion.identity);
        }
        
    }
}
