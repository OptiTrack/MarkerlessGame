using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator anim;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("LeftClick");
        } else
        {
            if (Input.GetMouseButtonDown(1))
            {
                anim.SetTrigger("RightClick");
             }
        }
        if (Input.GetKey(KeyCode.Z))
        {
            anim.SetBool("Block", true);
        } else
        {
            anim.SetBool("Block", false);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            anim.SetBool("Low", true);
        } else
        {
            anim.SetBool("Low", false);
        }
        if (Input.GetKey(KeyCode.E))
        {
            anim.SetBool("Hook", true);
        }
        else
        {
            anim.SetBool("Hook", false);
        }
        if (Input.GetKey(KeyCode.R))
        {
            anim.SetBool("Upper", true);
        }
        else
        {
            anim.SetBool("Upper", false);
        }
        if (Input.GetKey(KeyCode.T))
        {
            anim.SetBool("Overhand", true);
        }
        else
        {
            anim.SetBool("Overhand", false);
        }
        if (Input.GetKey(KeyCode.F))
        {
            anim.SetBool("Haymaker", true);
        }
        else
        {
            anim.SetBool("Haymaker", false);
        }
        if (Input.GetKey(KeyCode.X))
        {
            anim.SetBool("Feint", true);
        }
        else
        {
            anim.SetBool("Feint", false);
        }
        if (Input.GetKey(KeyCode.W))
        {
            anim.SetBool("Roll", true);
        }
        else
        {
            anim.SetBool("Roll", false);
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetBool("Slip", true);
        }
        else
        {
            anim.SetBool("Slip", false);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            anim.SetTrigger("LeftDodge");
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            anim.SetTrigger("RightDodge");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            anim.SetTrigger("Duck");
        }
    }
}
