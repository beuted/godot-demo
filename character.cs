using Godot;
using System;

public class character : KinematicBody2D
{
  private const int _motionSpeed = 200;
  private const int _gravity = 30;
  private const int _jumpSpeed = 300;
  private bool _jumping = false;
  private bool _jump = false;

  private Vector2 speed = Vector2.Zero;


  private AnimationPlayer _animationPlayer;
  private Sprite _sprite;
  private float _inputDirection;

  // Called when the node enters the scene tree for the first time.
  public override void _Ready()
  {
    _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
    _sprite = GetNode<Sprite>("Sprite");
  }

  public override void _Input(InputEvent ev)
  {
    _inputDirection = Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left");

    if (ev.IsActionPressed("jump"))
    {
      if (!_jumping)
      {
        _jump = true;
      }
      _jumping = true;
    }

    if (ev.IsActionReleased("jump"))
    {
      _jumping = false;
    }
  }

  public override void _PhysicsProcess(float delta)
  {
    // Movements
    speed.x = _inputDirection * _motionSpeed;

    if (_inputDirection < 0)
    {
      _sprite.FlipH = true;
      _animationPlayer.Play("Run");
    }
    else if (_inputDirection > 0)
    {
      _sprite.FlipH = false;
      _animationPlayer.Play("Run");
    }
    else
    {
      _animationPlayer.Play("Idle");
    }

    // Gravity
    speed.y += _gravity;

    // Jump
    if (_jump)
    {
      speed.y = -_jumpSpeed;
      _jump = false;
    }

    // Jump Pro tip: inverted gravity
    if (_jumping)
      speed.y -= _gravity / 3;

    speed = MoveAndSlide(speed);
  }
}
