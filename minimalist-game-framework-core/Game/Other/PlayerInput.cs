using System;
using System.Collections.Generic;
using System.Text;

class PlayerInput
{
    Player player = null;

    private float _time = 0;
    private float _lastGrounded = 0;
    private readonly float _jumpHoldTime = 0.14f;

    private float _wallJumpTimer = 0;
    private readonly float _wallJumpDuration = 0.15f;
    private bool _isWallJumping = false;

    private float _attackDelay = 0.12f;
    private bool _attacking = false;
    private Actions _queuedAttack;

    public PlayerInput(Player p)
    {
        player = p;
    }

    public void Update()
    {
        _time += Engine.TimeDelta;

        if(_attacking)
        {
            _attackDelay -= Engine.TimeDelta;

            if (_attackDelay <= 0)
            {
                player.Attack(_queuedAttack);
                _attacking = false;
            }
        }

        if(player.IsGrounded)
        {
            _lastGrounded = _time;
        }

        if(_isWallJumping)
        {
            _wallJumpTimer += Engine.TimeDelta;
            player._dX = player.IsFacingRight ? player.maxSpeed : -player.maxSpeed;
        }

        if(_wallJumpTimer > _wallJumpDuration)
        {
            _isWallJumping = false;
            _wallJumpTimer = 0;
        }

        // jump
        if (Engine.GetKeyHeld(Key.Up) || Engine.GetKeyHeld(Key.Space))
        {
            if (!player.ColLadder && !player.ColWall && _time < _lastGrounded + _jumpHoldTime)
            {
                player._dY = player.jumpHeight;
            }
            else if (player.ColWall)
            {
                _isWallJumping = true;
                player._dY = player.jumpHeight - 2;
                player._dX = player.IsFacingRight ? -player.maxSpeed : player.maxSpeed;
                player.IsFacingRight = !player.IsFacingRight;
            }
        }

        if(!_isWallJumping)
        {
            HorizontalMovement();
        }
        
        // ladder
        if(Engine.GetKeyHeld(Key.Up) && player.ColLadder && !player.WillOverlap(0, -5f, CollisionHandler.GetCollisions(player)))
        {
            player.MoveY(-5f);
        }

        if(Engine.GetKeyHeld(Key.Down) && player.ColLadder && !player.WillOverlap(0, 5f, CollisionHandler.GetCollisions(player)))
        {
            player.MoveY(5f);
        }

        // attack
        HandleAttack();
    }

    private void HorizontalMovement()
    {
        if (Engine.GetKeyHeld(Key.Right))
        {
            if (player._dX < player.maxSpeed)
            {
                player._dX += player._aX * player.Friction;
            }

            // move right
            player.IsFacingRight = true;
            player.anim.ChangeAction(Actions.Run);
        }
        else if (Engine.GetKeyHeld(Key.Left))
        {
            if (player._dX > -player.maxSpeed)
            {
                player._dX -= player._aX * player.Friction;
            }
            // move left
            player.IsFacingRight = false;
            player.anim.ChangeAction(Actions.Run);
        }
        else
        {
            if (player._dX > 0)
            {
                player._dX -= player._aX * player.Friction;
                player._dX = Math.Clamp(player._dX, 0, player.maxSpeed);
            }

            if (player._dX < 0)
            {
                player._dX += player._aX * player.Friction;
                player._dX = Math.Clamp(player._dX, -player.maxSpeed, 0);
            }

            player.anim.ChangeAction(Actions.Idle);
        }

        player._dX = Math.Clamp(player._dX, -player.maxSpeed, player.maxSpeed);
    }

    private void HandleAttack()
    {
        if(player.AttackCD <= 0)
        {
            if (Engine.GetKeyDown(Key.Z))
            {
                _queuedAttack = Actions.Attack_high;
                _attackDelay = 0.12f;
                _attacking = true;
                player.anim.ChangeAction(_queuedAttack);
            }

            if (Engine.GetKeyDown(Key.X))
            {
                _queuedAttack = Actions.Attack_mid;
                _attackDelay = 0.12f;
                _attacking = true;
                player.anim.ChangeAction(_queuedAttack);
            }

            if (Engine.GetKeyDown(Key.C))
            {
                _queuedAttack = Actions.Attack_low;
                _attackDelay = 0.12f;
                _attacking = true;
                player.anim.ChangeAction(_queuedAttack);
            }
        }
    }
}
    
