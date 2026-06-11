using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal.Internal;

public class PlayerController : MonoBehaviour
{
    public Camera cam;
    public Actions action;
    public float moveSpeed;
    private Vector2 move;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        action = new Actions();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        move = action.Player.Move.ReadValue<Vector2>();
        float slowDown = move.y;

        Vector3 forward = cam.transform.forward;
        forward = forward.normalized;

        this.transform.rotation = Quaternion.LookRotation(forward);

        if (this.gameObject != null)
        {
            transform.Translate(forward * moveSpeed * Time.deltaTime, Space.World);

            if (move.sqrMagnitude > 0.1)
            {
                moveSpeed = 12;

                if( slowDown == -1)
                {
                    moveSpeed = 3;
                }
            }
            else
            {
                moveSpeed = 6f;
            }
        }
    }

    void OnEnable()
    {
        action.Enable();
    }

    bool IsCollied()
    {
        if (Physics.Raycast(this.transform.position, this.transform.forward, 5f))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
