using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public abstract class GameBaseState : BaseState
{
    protected GameManager controller;
    protected GameBaseState(GameManager mono)
    {
        controller = mono;
    }
}

public class GameManagerFSM : BaseFSM
{
    private GameManager controller;
    public GameManagerFSM(GameManager mono)
    {
        controller = mono;
        //AddEventListener(EventName.EnterLevel, args =>
        //{
        //    controller.SetIsInGame(true);
        //    UIManager.instance.DestroyAllUIWithType<StartMenuPage>(); 
        //});
        //// todo 第一次打开游戏的时候可能会导致这里重复载入场景？
        //AddEventListener(EventName.ReturnToStartMenu, args =>
        //{
        //    AudioManager.instance.StopBGM();
        //    controller.SetIsInGame(false);
        //    SceneManager.LoadScene("StartMenu");
        //    UIManager.instance.CreateUI<StartMenuPage>();
        //    UIManager.instance.DestroyAllUIWithType<GameMainPage>(); 
        //    UIManager.instance.DestroyAllUIWithType<PauseMenuUI>();
        //    UIManager.instance.DestroyAllUIWithType<TutorialBubbleUI>();
        //});
        currentState = new StartState(mono);
    }
    public class StartState : GameBaseState
    {
        public StartState(GameManager mono) : base(mono)
        {
            controller = mono;
            EnterState();
        }
        public override void EnterState()
        {
            controller.SetIsInGame(false);
            //LevelManager.instance.BackToMainMenu();
        }
        public override void HandleUpdate()
        {
            
        }
        public override void HandleFixedUpdate()
        {
            
        }
        public override void ExitState()
        {

        }
        public override void HandleCollide2D(Collision2D collision)
        {

        }

        public override void HandleTrigger2D(Collider2D collider)
        {
            
        }
    }
    public class GameNormalState : GameBaseState
    {
        public GameNormalState(GameManager mono) : base(mono)
        {
            controller = mono;
            EnterState();
        }
        public override void EnterState()
        {
            controller.SetIsInGame(true);
        }
        public override void HandleUpdate()
        {
            
        }
        public override void HandleFixedUpdate()
        {
            
        }
        public override void ExitState()
        {

        }
        public override void HandleCollide2D(Collision2D collision)
        {

        }

        public override void HandleTrigger2D(Collider2D collider)
        {
            
        }
    } 
}
