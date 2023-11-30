using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text timer;
    public Text winner;
    public GameObject[] menuPanels;
    public static GameStates currentGameState = GameStates.MainMenu;
    public GameObject Box;
    public GameObject Goal;
    public float time;
    TimeSpan timespan;

    bool isRunning = false;
    public static bool started = false;
    public void Start()
    {
        if (started)
        {
            SetState(GameStates.Game);
        }
       else
        {
            SetState(GameStates.MainMenu);

        }

        started = true;
    }
    private void Update()
    {

        if (isRunning && ! Finish.victory)
        {
            time += Time.deltaTime;

            timespan = TimeSpan.FromSeconds(time);

            timer.text = string.Format("{0:D2}", timespan.Seconds);
        }     

        if(Finish.victory)
        {
            SetState(GameStates.Winner);
        }
    }


    /*

        On Trigger Enter switch state to Victory 
          Txt.("Color") + Wins + "in" + Time + "Seconds"

    */

    public void RestGame()
    {
        Finish.victory = false;
        SceneManager.LoadScene(0);
    }

    public void PlayGame()
    {
        SetState(GameStates.Game);
    }

    public void SetState(GameStates newState)
    {
        currentGameState = newState;
        for (int i = 0; i < menuPanels.Length; i++)
        {
            menuPanels[i].SetActive(false);
        }
        switch (currentGameState)
        {
            case GameStates.MainMenu:
                menuPanels[0].SetActive(true);
                break;
            case GameStates.Winner:
                menuPanels[1].SetActive(true);
                break;
            case GameStates.Game:
                menuPanels[2].SetActive(true);
                GameStart();
                break;
            default:
                currentGameState = GameStates.MainMenu;
                menuPanels[0].SetActive(true);
                break;
        }
    }

    public void GameStart()
    {
        Destroy(Box);
        isRunning = true;


    }



}
public enum GameStates
{
    MainMenu,
    Game,
    Winner
}