using UnityEngine;
using static Move;
public class DefaultEnemyBehavior : MonoBehaviour
{
    //Simple, but useful to override for advanced monsters
    private Side SwapSide(Move m)
    {
        if(m.side==Side.left)
        {
            return Side.right;
        }else
        {
            return Side.left;
        }
    }
    private Height SwapHeight(Move m)
    {
        if(m.height==Height.high)
        {
            return Height.low;
        }else
        {
            return Height.high;
        }
    }


    public Move lastMove;
    public Move currentMove;
    // Positive values represent turns without attacking
    // Negative values represent turns without blocking
    [SerializeField] private float _lastAttackCounter;

    public void Start()
    {
        lastMove=new Move(State.idle);
    }
    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag=="player")
        {
            //if force threshold reached
            Debug.Log("I was hit");
            if(lastMove.state!=State.stagger)
            {

                Transition(new Move(State.stagger));
                //Damage math
                //score
            }
        }
    }

    private void Transition(Move m)
    {
        //gradually switch to anim
        lastMove=currentMove;
        currentMove=m;
    }
    public void OnAnimationEnd()
    {
        if(currentMove.state==State.stagger||currentMove.state==State.idle)
            {
                Move nextMove=ChooseNextMove();
                Transition(nextMove);
            }else
            {
                Transition(new Move(State.idle));
            }
    }
    // Calling without arguments generates 3 pseudo random numbers to pass in
    private Move ChooseNextMove(){
        return ChooseNextMove(Random.Range(0,100),Random.Range(0,100),Random.Range(0,100));
    }
    // calling with arguments is for testing (when you don't want random numbers)
    public Move ChooseNextMove(int probA, int probB, int probC)
    {
        // prefer to not repeat moves (not all the time)
        // prefer to switch targets (high->low and/or left-right)
        // more attacks in a row decreases likelyhood of attack
        Move nextMove=new Move(lastMove.height,lastMove.side,lastMove.state);

        const int SWAP_LIKELYHOOD = 60; //Chance to switch left/right and high or low, chance to switch neither = 16%
        //calc side
        if(probA < SWAP_LIKELYHOOD)
        {
            nextMove.side=SwapSide(nextMove);
        }
        //calc height
        if(probB < SWAP_LIKELYHOOD)
        {
            nextMove.height=SwapHeight(nextMove);
        }

        //prevent counter from going beyond the root of the probability function
        if(_lastAttackCounter>20)
        {
            _lastAttackCounter=20;
        }
        //calc attack or block
        //1/40(50-5x/2)^2
        if(probC < .025f*Mathf.Pow(50.0f-2.5f*_lastAttackCounter,2.0f))
        {
            nextMove.state=State.attack;
            if(_lastAttackCounter<0)
            {
                _lastAttackCounter=0;
            }
            _lastAttackCounter++;
        }else
        {
            if(_lastAttackCounter>0)
            {
                _lastAttackCounter=0;
            }
            _lastAttackCounter--;
            // if the side is changed then dodge
            // otherwise guard
            if(nextMove.side!=lastMove.side)
            {
                nextMove.state=State.dodge;
            }else
            {
                nextMove.state=State.guard;
            }
        }
        return nextMove;


    }
}
