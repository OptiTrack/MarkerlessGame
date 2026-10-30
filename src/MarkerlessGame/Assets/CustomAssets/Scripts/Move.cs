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

    public Move(Height h, Side s2, State s1)
    {
        state=s1;
        side=s2;
        height=h;
    }
    public Move(Height h, State s1): this(h,Side.none,s1){}
    public Move(State s1): this(Height.none,Side.none,s1){}

}
