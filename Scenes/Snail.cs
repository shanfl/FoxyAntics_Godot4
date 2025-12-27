using Godot;
using System;
using System.ComponentModel;

public partial class Snail : EnemyBase
{

    [Export] RayCast2D _floorRayCast2D;

    public override void _PhysicsProcess(double delta)
    {
        //return;
        //base._PhysicsProcess(delta);
        Vector2 velocity = Velocity;

        if(!IsOnFloor())
        {
            velocity.Y += _gravity * (float)delta;
        }
        else
        {
            velocity.X = _animatedSprite2D.FlipH ? _speed : -_speed;
        }

        Velocity = velocity;

        MoveAndSlide(); 

        FlipMe();   

    }

    private void FlipMe()
    {
        if(IsOnFloor())
        {
            //GD.Print("FloorRayCast Colliding: " + _floorRayCast2D.IsColliding());
            //GD.Print("IsOnWall: " + IsOnWall());
            if(IsOnWall() || !_floorRayCast2D.IsColliding())
            {
                //GD.Print("Flip");
                _animatedSprite2D.FlipH = !_animatedSprite2D.FlipH;
                _floorRayCast2D.Position= new Vector2(_floorRayCast2D.Position.X*-1, _floorRayCast2D.Position.Y);                
            }
        }

    }


}
