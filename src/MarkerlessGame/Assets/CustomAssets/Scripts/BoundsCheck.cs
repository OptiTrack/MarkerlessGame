using UnityEngine;

public class BoundsCheck : MonoBehaviour
{

    public float LowerYAxisBound;
    public bool ResetOnBoundExit = false;
    public Vector3 InitialPosition = Vector3.zero;
    public Rigidbody rb;

    private bool _isOutOfBounds = false;

    void Start()
    {
        if (InitialPosition == Vector3.zero)
        {
            InitialPosition = transform.position;
        }
    }

    private void FixedUpdate()
    {
        // Ignore remaining checks if already out of bounds
        if (_isOutOfBounds)
        {
            return;
        }

        if (transform.position.y < LowerYAxisBound)
        {
            OutOfBounds();
        }
    }

    private void OutOfBounds()
    {
        Debug.Log(gameObject.name + " is out of set bounds");
        _isOutOfBounds = true;

        // "Reset" game object
        if (ResetOnBoundExit)
        {
            ResetToPosition(InitialPosition);
        }
    }

    private void ResetToPosition(Vector3 position)
    {
        // Ensure rigidbody's velocity is reset too
        if (rb)
        {
            rb.linearVelocity = Vector3.zero;
        }

        transform.position = InitialPosition;
        _isOutOfBounds = false;
    }
}
