using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    public PlayerJumpState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) : base(currentContext, playerStateFactory) { }
    public override void EnterState() {
        HandleJump();
    }
    public override void UpdateState() {
        CheckSwitchStates();
    }
    public override void ExitState() {
        _ctx.Animator.SetBool(_ctx.IsJumpingHash, true);
        _ctx.IsJumpAnimating = true;
        _ctx.IsJumping = true;
        _ctx.JumpCount += 1;
        _ctx.Animator.SetInteger(_ctx.JumpCountHash, _ctx.JumpCount);
        _ctx.CurrentMovementY = _ctx.InitialJumpVelocity;
        _ctx.AppliedMovementY = _ctx.InitialJumpVelocity;

        if (_ctx.JumpCount == 3)
        {
            _ctx.JumpCount = 0;
        }
    }
    public override void CheckSwitchStates() { 
    }
    public override void InitializeSubState() { }

    void HandleJump()
    {
        _ctx.Animator.SetBool(_ctx.IsJumpingHash, true);
        _ctx.IsJumpAnimating = true;
        _ctx.IsJumping = true;
        _ctx.JumpCount += 1;
        _ctx.Animator.SetInteger(_ctx.JumpCountHash, _ctx.JumpCount);
        _ctx.CurrentMovementY = _ctx.InitialJumpVelocity;
        _ctx.AppliedMovementY = _ctx.InitialJumpVelocity;

        if (_ctx.JumpCount == 3)
        {
            _ctx.JumpCount = 0;
        }


    }
}
