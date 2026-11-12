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

}
