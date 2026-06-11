using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Camera cam;
    public Actions action;
    private Vector2 glider;
    public AudioSource engine;
    public float moveSpeed;
    public float crashDistance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        action = new Actions();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Collision();
    }

    void Movement()
    {
        glider = action.Player.Glider.ReadValue<Vector2>();
        float slowDown = glider.y;

        Vector3 forward = cam.transform.forward;
        forward = forward.normalized;

        this.transform.rotation = Quaternion.LookRotation(forward);

        if (this.gameObject != null)
        {
            transform.Translate(forward * moveSpeed * Time.deltaTime, Space.World);
            engine.pitch = 1.5f;

            if (glider.sqrMagnitude > 0.1)
            {
                moveSpeed = 12;
                engine.pitch = 2f;

                if( slowDown == -1)
                {
                    moveSpeed = 3;
                    engine.pitch = 1f;
                }
            }
            else
            {
                moveSpeed = 6f;
            }
        }
    }

    void Collision()
    {
        if (IsCollied())
        {
            SceneManager.LoadScene("Dead");
            Debug.Log($"You are dead!");
        }
    }

    void OnEnable()
    {
        action.Enable();
    }

    bool IsCollied()
    {
        if (Physics.Raycast(this.transform.position, this.transform.forward, crashDistance))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
