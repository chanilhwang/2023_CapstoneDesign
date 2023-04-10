using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

enum GameState
{
    menu,
    inGame,
    gameOver
}

public class GameManager : MonoBehaviour 
{
    public static GameManager _instance;

    public RoomManager roomScript;
    private int level = 3;

    private GameState _gameState { get; set; }

    private void Awake()
    {
        if(_instance != null)
        {
            Destroy(this.gameObject);
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);

        roomScript = GetComponent<RoomManager>();

        InitGame();
    }
    void InitGame()
    {
        roomScript.SetupScene();
    }
}