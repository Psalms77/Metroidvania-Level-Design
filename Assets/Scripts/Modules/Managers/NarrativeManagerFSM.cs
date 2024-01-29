using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public abstract class NarrativeBaseState : BaseState
{
    protected NarrativeManager controller;
    protected NarrativeBaseState(NarrativeManager mono)
    {
        controller = mono;
    }
}

public class NarrativeManagerFSM : BaseFSM
{
    private NarrativeManager controller;
    public NarrativeManagerFSM(NarrativeManager mono)
    {
        controller = mono;
        currentState = new StartState(mono);
    }
    public class StartState : NarrativeBaseState
    {
        public StartState(NarrativeManager mono) : base(mono)
        {
            controller = mono;
            EnterState();
        }
        public override void EnterState()
        {

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
    public class GameNormalState : NarrativeBaseState
    {
        public GameNormalState(NarrativeManager mono) : base(mono)
        {
            controller = mono;
            EnterState();
        }
        public override void EnterState()
        {

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
