using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    new void Awake()
    {
        base.Awake();

    }

    //Function to run before setting up Gameplay, such as creating a Load Screen / Setting Up Setup Scripts
    private void PreSetup()
    {

    }

    //Function to run to start GamePlay Setup, such as level seeding, level creation, level loading, player loading etc.
    private void Setup()
    {

    }

    private void GamePlay() //??? game go i guess
    {

    }


    public bool IsPaused { get; private set; }
    private void GamePause() //Pauses Game (Through to pause menu)
    {
        IsPaused = true;
    }

    private void GameUnpause()
    {
        IsPaused = false;
    }



}
