using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public abstract class PlayerBaseState : BaseState
{
    protected PlatformPlayerController controller;
    protected PlayerBaseState(PlatformPlayerController mono)
    {
        controller = mono;
    }

}


public class PlayerFSM : BaseFSM
{
    private PlatformPlayerController controller;
    public PlayerFSM(PlatformPlayerController mono)
    {
        controller = mono;
        currentState = new IdleState(mono);

        //controller.spriteObj.GetComponent<AnimationEvent>().SetAnimEventCallBack("die_animation_end", () =>
        //{
        //    controller.spriteObj.GetComponent<SpriteRenderer>().enabled = false;
        //    controller.transform.DOScale(1, 0f);
        //    controller.StartCoroutine(RespawnPlayer());
        //});
    }
    //IEnumerator RespawnPlayer()
    //{
    //    yield return new WaitForSeconds(0.5f);
    //    controller.stateMachine.TransitState(new IdleState(controller));
    //}
    public class IdleState : PlayerBaseState
    {
        public IdleState(PlatformPlayerController mono) : base(mono)
        {
            this.controller = mono;
            EnterState();
        }
        public override void EnterState()
        {
            // Debug.Log("idle enter");
            controller.rig.gravityScale = controller.initGravityScale;
            controller.groundSpeed = 0;

            //controller.spriteObj.GetComponent<SpriteRenderer>().enabled = true;
            //controller.walkingParticle.SetActive(false);
        }
        public override void HandleUpdate()
        {


            controller.HandleJumpInput();
            controller.HorizontalMove();
            if (controller.IsOnGround())
            {
                controller.jumpCount = -1;
                controller.canDoubleJump = false;

            }
            if (Mathf.Abs(controller.rig.velocity.x) > 0.01f) 
            {
                controller.stateMachine.TransitState(new MoveState(controller));
            }
            if (controller.rig.velocity.y < 0 && !controller.IsOnGround())
            {
                controller.stateMachine.TransitState(new JumpState(controller, false));
            }

            if (controller.HandleDashInput())
            {
                controller.stateMachine.TransitState(new DashState(controller));

            }

            if (controller.isDie)
            {
                controller.stateMachine.TransitState(new DieState(controller));
            }
        }
        public override void HandleFixedUpdate()
        {
            bool hasJumped = controller.HandleJump();
            if(hasJumped)
            {
                controller.stateMachine.TransitState(new JumpState(controller, true));
            }
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



    private class JumpState : PlayerBaseState
    {
        // Detect whether this is a volunteered jump  是否是一次主动跳跃
        private bool isJump;
        // Detect whether player has released jump 是否在跳跃中松开过跳跃按键，防止多次间歇按跳跃的操作（应该现在不需要）
        // private bool hasReleaseJump;
        private int jumpPressFixedUpdateCount = 0;
        private bool shouldControlJumpUp;
        public JumpState(PlatformPlayerController mono, bool isJump) : base(mono)
        {
            controller = mono;
            this.isJump = isJump;
            EnterState();
        }
        public override void EnterState()
        {
            //controller.walkingParticle.SetActive(false);
            if (isJump == false)
            {
                controller.isCoyote = true;
                controller.StartCoroutine(controller.RecoverCoyote());
            }
            else
            {
                //AudioManager.instance.PlayEffectAudio_ForSO(controller.jumpAudio);
            }
        }
        public override void HandleUpdate()
        {


            controller.HandleJumpInput();
            controller.HorizontalJumpingMove();

            controller.animator.SetFloat("HorizontalSpeed", Mathf.Abs(controller.groundSpeed));
            controller.animator.SetBool("isOnGround", controller.IsOnGround());
            controller.animator.SetFloat("VerticalSpeed", controller.rig.velocity.y);

            if (controller.IsOnGround())
            {
                controller.jumpCount = -1;
                controller.canDoubleJump = false;

            }
            if (controller.jumpCount == 0)
            {
                controller.canDoubleJump = true;
            }
            if (controller.jumpCount > 0)
            {
                controller.canDoubleJump = false;
            }


            // left and right flip
            var inputSpeed = Input.GetAxis("Horizontal");
            if (inputSpeed > 0)
            {
                controller.transform.localScale = new Vector3(1,1,1);
            }
            if (inputSpeed < 0)
            {
                controller.transform.localScale = new Vector3(-1,1,1);
            }

            if (isJump && controller.rig.velocity.y > 0 && Input.GetKey(KeyCode.Space))
            {
                shouldControlJumpUp = true;
            }
            else
            {
                shouldControlJumpUp = false;
            }

            if (controller.isDie)
            {
                controller.stateMachine.TransitState(new DieState(controller));
            }
        }
        public override void HandleFixedUpdate()
        {
            // Debug.Log("跳!  重力:  " + controller.rig.gravityScale);
            bool hasJumped = controller.HandleJump();
            if (shouldControlJumpUp)
            {
                jumpPressFixedUpdateCount++;
                if (jumpPressFixedUpdateCount > controller.smallestJumpFrameCount)
                {
                    controller.rig.gravityScale = Mathf.Min(
                        controller.initGravityScale * (controller.jumpGravityScale +
                                                       controller.ascendingGravityStep * jumpPressFixedUpdateCount),
                        controller.initGravityScale);
                }
            }
            else
            {
                // if (jumpPressFixedUpdateCount > controller.smallestJumpFrameCount)
                // {
                    controller.rig.gravityScale = controller.initGravityScale * controller.gravityFalling;
                // }
                // else
                // {
                    // controller.rig.gravityScale = controller.initGravityScale;
                // }
                jumpPressFixedUpdateCount = 0;
            }
            if (controller.rig.velocity.y < 0)
            {
                controller.rig.gravityScale = controller.initGravityScale * controller.gravityFalling;
            }

            if (Mathf.Abs(controller.rig.velocity.y) <= 0.02f && controller.IsOnGround()) 
            {
                controller.stateMachine.TransitState(new IdleState(controller));
                return;
            }

            if (hasJumped)
            {
                controller.stateMachine.TransitState(new JumpState(controller, true));
            }
        }
        public override void ExitState()
        {
            controller.rig.gravityScale = controller.initGravityScale;
        }
        public override void HandleCollide2D(Collision2D collision)
        {
            controller.groundSpeed = 0;
        }

        public override void HandleTrigger2D(Collider2D collider)
        {
            throw new System.NotImplementedException();
        }
    }

    public class MoveState : PlayerBaseState
    {
        public MoveState(PlatformPlayerController mono) : base(mono)
        {
            this.controller = mono;
            EnterState();
        }
        public override void EnterState()
        {
            //controller.walkingParticle.SetActive(true);
        }
        public override void HandleUpdate()
        {



            controller.HandleJumpInput();
            controller.HorizontalMove();
            controller.groundSpeed = controller.rig.velocity.x;

            controller.animator.SetFloat("HorizontalSpeed", Mathf.Abs(controller.groundSpeed));
            controller.animator.SetBool("isOnGround", controller.IsOnGround());
            controller.animator.SetFloat("VerticalSpeed", controller.rig.velocity.y);

            if (controller.IsOnGround())
            {
                controller.jumpCount = -1;
                controller.canDoubleJump = false;

            }

            //left & right flip
            if (controller.groundSpeed > 0)
            {
                controller.transform.localScale = new Vector3(1, 1, 1);
            }
            if (controller.groundSpeed < 0)
            {
                controller.transform.localScale = new Vector3(-1, 1, 1);
            }
            //  stopped detection 判定是否停下
            if (Mathf.Abs(controller.rig.velocity.x) < 0.01f)
            {
                controller.stateMachine.TransitState(new IdleState(controller));
            }
            //  falling detection 判定是否掉落
            if (controller.rig.velocity.y < 0 && !controller.IsOnGround())
            {
                controller.stateMachine.TransitState(new JumpState(controller, false));
            }

            //  death detection  判定是否死亡
            if (controller.isDie)
            {
                controller.stateMachine.TransitState(new DieState(controller));
            }
        }
        public override void HandleFixedUpdate()
        {
            bool hasJumped = controller.HandleJump();
            if (hasJumped)
            {
                controller.stateMachine.TransitState(new JumpState(controller, true));
            }
        }
        public override void ExitState()
        {

        }
        public override void HandleCollide2D(Collision2D collision)
        {

        }

        public override void HandleTrigger2D(Collider2D collider)
        {
            throw new System.NotImplementedException();
        }
    }

    public class DashState : PlayerBaseState
    {
        public DashState(PlatformPlayerController mono) : base(mono)
        {
            controller = mono;
            EnterState();
        }
        public override void EnterState()
        {

        }
        public override void HandleUpdate()
        {
            controller.rig.gravityScale = 0;

            //left & right flip
            if (controller.groundSpeed > 0)
            {
                controller.transform.localScale = new Vector3(1, 1, 1);
            }
            if (controller.groundSpeed < 0)
            {
                controller.transform.localScale = new Vector3(-1, 1, 1);
            }

            if (controller.dashTimer < controller.dashTime)
            {
                controller.Dash();
                controller.dashTimer += Time.deltaTime;
            }
            else if (controller.dashTimer >= controller.dashTime)
            {
                controller.dashTimer = 0;
            }

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


    public class MeleeState  : PlayerBaseState
    {
        public MeleeState(PlatformPlayerController mono) : base(mono)
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




    public class DieState : PlayerBaseState
    {
        public DieState(PlatformPlayerController mono) : base(mono)
        {
            controller = mono;
            EnterState();
        }
        public override void EnterState()
        {
            // controller.GetComponent<Collider2D>().enabled = false;
            // Input.
            //controller.walkingParticle.SetActive(false);
            controller.rig.velocity = Vector2.zero;
            controller.rig.gravityScale = 0;
            //controller.animator.SetTrigger("is_die");
            controller.transform.DOScale(3, 0.4f);
            //controller.spriteObj.GetComponent<SpriteRenderer>().DOColor(new Color(128, 0, 0), 0.3f).onComplete = () =>
            //{
            //    controller.spriteObj.GetComponent<SpriteRenderer>().color = Color.white;;
            //};
        }
        public override void HandleUpdate()
        {
            
        }
        public override void HandleFixedUpdate()
        {
            
        }
        public override void ExitState()
        {
            // controller.GetComponent<Collider2D>().enabled = true;
        }
        public override void HandleCollide2D(Collision2D collision)
        {

        }

        public override void HandleTrigger2D(Collider2D collider)
        {
            
        }
    }
}

