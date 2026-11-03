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
        Assert.AreNotEqual(script.currentMove.state,animName);
    }

    [Test]
    [TestCase(100,100,100,Height.low,Side.left,State.guard)]
    [TestCase(100,100,0,Height.low,Side.left,State.attack)]
    [TestCase(100,0,100,Height.low,Side.right,State.dodge)]
    [TestCase(100,0,0,Height.low,Side.right,State.attack)]
    [TestCase(0,100,100,Height.high,Side.left,State.guard)]
    [TestCase(0,0,100,Height.high,Side.right,State.dodge)]
    [TestCase(0,0,0,Height.high,Side.right,State.attack)]
    [TestCase(0,100,0,Height.high,Side.left,State.attack)]
    public void SwitchAnim(int heightProb,int sideProb,int atkProb, Height height,Side side,State state)
    {
        var expected=new Move(height,side,state);
        var actual=script.ChooseNextMove(sideProb,heightProb,atkProb);
        Assert.AreEqual(expected.state,actual.state);
        Assert.AreEqual(expected.side,actual.side);
        Assert.AreEqual(expected.height,actual.height);
    }

    [Test]
    [TestCase(100,Side.left,State.guard)]
    [TestCase(0,Side.right,State.dodge)]
    public void GuardOrDodge(int sideProb,Side side,State state)
    {
        script.lastMove=new Move(Side.left,State.idle);
        var expected=new Move(side,state);
        var actual=script.ChooseNextMove(sideProb,0,100);
        Assert.AreEqual(expected.side,actual.side);
        Assert.AreEqual(expected.state,actual.state);

    }

    [Test]
    [TestCase(70,0,State.dodge)]
    [TestCase(60,0,State.attack)]
    [TestCase(20,10,State.dodge)]
    [TestCase(15,10,State.attack)]
    [TestCase(5,20,State.dodge)]
    [TestCase(0,20,State.dodge)]
    public void AttackCounterDecreasesLikelihood(int atkProb,int atkCounter,State state)
    {
        var expected=new Move(state);
        //make i attacks
        for(int i=0;i<atkCounter;i++)
        {
                script.ChooseNextMove(0,0,0);
        }
        var actual=script.ChooseNextMove(0,0,atkProb);
        Assert.AreEqual(expected.state,actual.state);
    }
}
