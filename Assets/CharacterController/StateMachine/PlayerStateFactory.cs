using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateFactory
{
    PlayerStateMachine _context;

    public PlayerStateFactory(PlayerStateMachine currentContext)
    {
        _context = currentContext;
    }

    public PlayerBaseState Idle()
    {
        return new PlayerIdleState();
    }
    public PlayerBaseState Walk()
    {
        return new PlayerIdleState();
    }
    public PlayerBaseState Run()
    {
        return new PlayerRunState();
    }
    public PlayerBaseState Jump()
    {
        return new PlayerJumpState();
    }
    public PlayerBaseState Grounded()
    {
        return new PlayerGroundedState();
    }
}
