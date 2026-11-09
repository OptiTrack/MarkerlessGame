using UnityEngine;

public class SpeedCalculator : MonoBehaviour
{
    const float k_Scaler = 50.0f;

    public float ObjectSpeed;

    private Vector3 _previousPosition;

    private void Start()
    {
        _previousPosition = transform.position;
    }

    private void FixedUpdate()
    {
        Vector3 positionDelta = transform.position - _previousPosition;
        ObjectSpeed = Mathf.Abs(positionDelta.magnitude) * k_Scaler;
        _previousPosition = transform.position;
    }
}
