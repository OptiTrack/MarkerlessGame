using UnityEngine;

public class SpeedCalculator : MonoBehaviour
{
    const float k_scaler = 50.0f;

    public float currentSpeed;

    private Vector3 _previousPosition;

    private void Start()
    {
        _previousPosition = transform.position;
    }

    private void FixedUpdate()
    {
        Vector3 positionDelta = transform.position - _previousPosition;
        currentSpeed = Mathf.Abs(positionDelta.magnitude) * k_scaler;
        _previousPosition = transform.position;
    }
}
