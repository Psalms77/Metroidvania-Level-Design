using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public bool isInGame { get; private set; }
    private GameManagerFSM stateMachine;
    public Camera cam;
    public Vector3 mousePos3;
    public Vector2 mousePos2;
    public bool isPulling = false;
    //public int potionCount = 0;

    protected override void Awake()
    {
        base.Awake();
      
        //DontDestroyOnLoad(gameObject);
        //DontDestroyOnLoad(GameObject.Find("AudioManager"));

        Application.targetFrameRate = 165;

    }

    void Start()
    {
        stateMachine = new GameManagerFSM(this);
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        // LevelManager.instance.BackToMainMenu();
        // UIManager.instance.CreateUI<GeneralFadePage>();
    }

    // private float _pressXTime = 0f;
    void Update()
    {
        UIManager.instance.Update();
        stateMachine.currentState.HandleUpdate();



        // GM only in editor
#if UNITY_EDITOR
        //if (Input.GetKeyUp(KeyCode.G))
        //{
        //    if (UIManager.instance.GetFirstUIWithType<GMPage>() != null)
        //    {
        //        UIManager.instance.DestroyAllUIWithType<GMPage>();
        //    }
        //    else
        //    {
        //        UIManager.instance.CreateUI<GMPage>();
        //    }
        //}
#endif
    }
    


    // returns player game object
    public GameObject GetPlayer()
    {
        return GameObject.FindGameObjectWithTag("Player");
    }

    public void SetIsInGame(bool status)
    {
        isInGame = status;
    }
}