using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Goblin : MonoBehaviour
{
    public Camera cam;
    public float moveSpeed;
    public float collideDistance;
    public Actions action;
    private Vector2 move;
    //private float velocity;
    //private float gravity = 9.81f;
    public Animator animator;
    public AudioSource walk;

    void Awake()
    {
        action = new Actions();
    }

    void Update()
    {
        movement();
        Anim();
        //Gravity();
        Collision();
    }

    void OnEnable()
    {
        action.Enable();
    }

    void OnDisable()
    {
        action.Disable();
    }

    void movement()
    {
        move = action.Player.Move.ReadValue<Vector2>();
        //Vector3 falling = new Vector3(0, velocity, 0);

        Vector3 forward = cam.transform.forward;
        forward.y = 0;
        forward = forward.normalized;

        Vector3 right = cam.transform.right;
        right = right.normalized;

        Vector3 forwardInput = forward * move.y;
        Vector3 rightInput = right * move.x;

        Vector3 movementDir = forwardInput + rightInput /*+ falling*/;

        if (move.sqrMagnitude > 0.1)
        {
            transform.rotation = Quaternion.LookRotation(movementDir, Vector3.up);
            walk.enabled = true;
        }
        else
        {
            walk.enabled = false;
        }

            transform.Translate(movementDir * moveSpeed * Time.deltaTime, Space.World);
    }

    void Anim()
    {
        if (action.Player.Move.ReadValue<Vector2>().sqrMagnitude > 0.1)
        {
            animator.Play("Walk");
        }
        else
        {
            animator.Play("Idle");
        }

        /*
        if (!Grounded())
        {
            animator.Play("Fall");
        }
        */
    }

    void Collision()
    {
        if (IsCollided())
        {
            moveSpeed = 0;
        }
        else
        {
            moveSpeed = 2;
        }
    }

    bool IsCollided()
    {
        if (Physics.Raycast(this.transform.position, this.transform.forward, collideDistance))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /*
    void Gravity()
    {
        if (!Grounded())
        {
            velocity -= gravity * Time.deltaTime;
        }
        else
        {
            velocity = 0f;
        }
    }

    bool Grounded()
    {
        if (Physics.Raycast(transform.position + transform.up * 0.25f, -transform.up, 2f))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    */
}
