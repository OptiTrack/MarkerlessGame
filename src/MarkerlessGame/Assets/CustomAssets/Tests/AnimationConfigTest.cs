using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class AnimationConfigTest
{
    // A Test behaves as an ordinary method
    [Test]
    public void AnimAttatched(GameObject obj,string animName)
    {
        //Check if an object exists before calling get component on it
        Assert.IsNotNull(obj);

        //check if a component of the given name exists on the game object
        Assert.IsTrue(
            Array.Exists<Animator>(obj.GetComponents<Animator>(),((Animator anim)=>{return anim.name==animName;}))
        );

    }
}
