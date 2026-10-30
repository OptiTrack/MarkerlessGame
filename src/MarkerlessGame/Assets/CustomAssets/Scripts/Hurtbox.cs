using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    public Attack attack;
    private Rigidbody _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hurtbox collided with " + other.gameObject.name);
        Hitbox hitbox = other.GetComponent<Hitbox>();

        if (hitbox != null)
        {
            Debug.Log("Hitbox detected, attempting to damage");

            float speed = Vector3.Magnitude(_rb.linearVelocity);
            Debug.Log("Punching moving at this speed: " + speed);

            if (hitbox.RecieveHit(attack, speed))
            {
                Debug.Log("Hit successful");
            }
        }
    }
}
