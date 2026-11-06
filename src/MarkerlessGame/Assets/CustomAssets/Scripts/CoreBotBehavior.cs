using UnityEngine;
using static Move;
public class CoreBotBehavior:DefaultEnemyBehavior
{

    [SerializeField]private int coreHealth=50;
    private bool hasBroken=false;
    public Collider coreCol;

    public new void OnTriggerEnter(UnityEngine.Collider col)
    {
        if(col==coreCol&&!hasBroken)
        {
            coreHealth-=10;
            if(coreHealth<=0)
            {
                hasBroken=true;
                Transition(new Move(State.broken));
            }
        }else
        {
            health-=10;
        }
    }
    public new void Start()
    {
        lastMove=new Move(State.idle);
        health=100;
    }

}
