using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using static Move;


public class CoreBotBehaviorTest
{
    private GameObject emptyCoreBot;
    private CoreBotBehavior script;
    private Collider botCollider;

    [SetUp]
    public void SetupScript()
    {
        emptyCoreBot=new GameObject();
        script=emptyCoreBot.AddComponent<CoreBotBehavior>();
        script.coreCol=(BoxCollider)emptyCoreBot.AddComponent<BoxCollider>();
        botCollider=emptyCoreBot.AddComponent<BoxCollider>();
    }

    [Test]
    public void SetupTest()
    {
        Assert.IsNotNull(emptyCoreBot);
        Assert.IsNotNull(script);
        Assert.IsNotNull(script.coreCol);
        Assert.IsNotNull(botCollider);
    }


}
