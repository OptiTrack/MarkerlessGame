using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class CollisionStateIntegrationTests
{
    private Hurtbox _hurtbox;
    private GameObject _emptyEnemy;
    private DefaultEnemyBehavior _script;
    private GameObject _emptyPlayer;

    [SetUp]
    public void SetupHurtBox()
    {
        _emptyEnemy=new GameObject();
        _emptyPlayer=new GameObject();
        _script=_emptyEnemy.AddComponent<DefaultEnemyBehavior>();
        _hurtbox=_emptyEnemy.AddComponent<Hurtbox>();
    }

    [Test]
    public void SetupTests()
    {
        Assert.IsNotNull(_emptyEnemy);
        Assert.IsNotNull(_emptyPlayer);
        Assert.IsNotNull(_hurtbox);
        Assert.IsNotNull(_script);
    }

    [UnityTest]
    public IEnumerator NoRepeatStagger()
    {

        //run awake
        yield return null;
        /*
        HitContext hc=new HitContext();
        hc.Attacker=_emptyPlayer;
        hc.Victim=_emptyEnemy;
        hc.baseDamage=50.0f;
        _hurtbox.RecieveHit(hc);
        _hurtbox.RecieveHit(hc);
        yield return null;
        LogAssert.Expect("Caught repeat stagger state");*/

    }
}
