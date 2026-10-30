using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using static Move;


public class EnemyBehaviorTest
{
    private GameObject emptyEnemy;
    private DefaultEnemyBehavior script;

    [SetUp]
    public void SetupScript()
    {
        emptyEnemy=new GameObject();
        emptyEnemy.AddComponent<DefaultEnemyBehavior>();
        script=emptyEnemy.GetComponent<DefaultEnemyBehavior>();
        script.lastMove=new Move(Height.low,Side.left,State.idle);
    }

    [Test]
    public void SetupTest()
    {
        Assert.IsNotNull(emptyEnemy);
        Assert.IsNotNull(emptyEnemy.GetComponent<DefaultEnemyBehavior>());
        Assert.IsNotNull(script);
    }


    [Test]
    [TestCase(State.stagger)]
    [TestCase(State.idle)]
    public void NoRepeatsAnim(State animName)
    {
        script.currentMove=new Move(animName);
        script.OnAnimationEnd();
        Assert.IsTrue(script.currentMove.state!=animName);
    }

    [Test]
    [TestCase(100,100,100,Height.low,Side.left,State.attack)]
    public void SwitchAnim(int sideProb,int heightProb,int atkProb, Height height,Side side,State state)
    {
        var expected=new Move(height,side,state);
        script.ChooseNextMove(sideProb,heightProb,atkProb);
        Assert.IsTrue(script.currentMove.state==expected.state);
        Assert.IsTrue(script.currentMove.side==expected.side);
        Assert.IsTrue(script.currentMove.height==expected.height);


    }
}
