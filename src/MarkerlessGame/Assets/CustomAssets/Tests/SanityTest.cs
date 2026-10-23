using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class SanityTest
{
    // A Test behaves as an ordinary method
    [Test]
    public void SanityTestSimplePasses()
    {
        Assert.IsTrue(true);
    }
}
