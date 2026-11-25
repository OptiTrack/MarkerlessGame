using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using static Move;

public class AnimationConfigTest
{
    private GameObject _kaijuPrefab;
    private GameObject _kaiju;
    private Animator _kaijuAnimator;
    private DefaultEnemyBehavior _kaijuBehavior;

    [SetUp]
    public void SetupAnimator()
    {
        _kaijuPrefab = Resources.Load<GameObject>("TestKaiju");
        // Instantiate an instance so tests don't mutate the raw resource asset
        _kaiju = Object.Instantiate(_kaijuPrefab);
        _kaijuAnimator = _kaiju.GetComponent<Animator>();
        _kaijuBehavior = _kaiju.GetComponent<DefaultEnemyBehavior>();

    }

    [TearDown]
    public void Teardown()
    {
        if (_kaiju != null)
        {
            Object.DestroyImmediate(_kaiju);
        }
    }

    [Test]
    public void SetupTest()
    {
        Assert.IsNotNull(_kaijuPrefab, "TestKaiju prefab could not be loaded from Resources.");
        Assert.IsNotNull(_kaiju);
        Assert.IsNotNull(_kaijuAnimator);
        Assert.IsNotNull(_kaijuBehavior);
    }

    /*
    [UnityTest]
    public IEnumerator StateIntegration
    (
        [Values(Height.high, Height.low, Height.none)] Height height,
        [Values(Side.left, Side.right, Side.none)] Side side,
        [Values(State.attack, State.guard, State.idle, State.dodge, State.stagger)] State state
    )
    {
        // Initialize behavior

        yield return null; // Let start run

        Move m = new Move(height, side, state);
        var tag = m.ToString();

        // Trigger the transition
        _kaijuBehavior.Transition(m);

        // Give the Animator a frame to register the trigger and start blending/transitioning
        yield return null;

        // Check layer 1 (adjust index if your state machine is on a different layer)

        yield return new WaitUntil(()=>_kaijuAnimator.GetCurrentAnimatorStateInfo(0).IsTag("dummy"));
        AnimatorStateInfo currentInfo = _kaijuAnimator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = _kaijuAnimator.GetNextAnimatorStateInfo(0);

        bool isInState = currentInfo.IsTag(tag) || nextInfo.IsTag(tag);

        AnimatorClipInfo[] clipInfo = _kaijuAnimator.GetCurrentAnimatorClipInfo(0);

        if (clipInfo.Length > 0)
        {
            string activeClipName = clipInfo[0].clip.name;
            Debug.Log($"Currently playing animation clip: {activeClipName}");
        }

        clipInfo = _kaijuAnimator.GetNextAnimatorClipInfo(0);

        if (clipInfo.Length > 0)
        {
            string activeClipName = clipInfo[0].clip.name;
            Debug.Log($"Next playing animation clip: {activeClipName}");
        }

        Assert.IsTrue(isInState,
            $"Animator failed to reach state with tag '{tag}'. "+
            $"Current State Tag matched: {currentInfo.IsTag(tag)},"+
            $" Next State Tag matched: {nextInfo.IsTag(tag)}");

            }*/
}
