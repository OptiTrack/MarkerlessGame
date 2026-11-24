using UnityEngine;
using System;
[System.Serializable]
public class Move
{
    public enum State
    {
        stagger,idle,guard,dodge,attack,none
    }
    public enum Side
    {
        left,right,none
    }
    public enum Height
    {
        high,low,none
    }

    public State state;
    public Side side;
    public Height height;

    //Allow creating a move with just a state, or a height and a state
    public Move(Height _height, Side _side, State _state)
    {
        state=_state;
        side=_side;
        height=_height;
    }
    public Move(Height _height, State _state): this(_height,Side.none,_state){}
    public Move(Side _side, State _state): this(Height.none,_side,_state){}
    public Move(State _state): this(Height.none,Side.none,_state){}

    public override String ToString()
    {
        String height="";
        String side="";
        switch(this.height)
        {
            case Height.high:
                height="high";
                break;
            case Height.low:
                height+="low";
                break;
        }
        switch(this.side)
        {
            case Side.left:
                side="Left";
                break;
            case Side.right:
                side="Right";
                break;
        }
        switch(this.state)
        {
            case State.idle:
                return "idle";
            case State.attack:
                return side+height+"Attack";
            case State.guard:
                return height+"Guard";
            case State.dodge:
                return side+"Dodge";
            case State.stagger:
                return "stagger";
        }
        return "none";
    }

}
