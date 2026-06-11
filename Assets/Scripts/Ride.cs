using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Ride : MonoBehaviour
{
    public GameObject goblin;
    public TMP_Text alert;
    public float pickupDistance;

    // Update is called once per frame
    void Update()
    {
        CheckDistance();
    }

    void CheckDistance()
    {
        float distance = Vector3.Distance(this.gameObject.transform.position, goblin.transform.position);

        if (distance <= pickupDistance)
        {
            alert.enabled = true;
            if (Keyboard.current.rKey.wasPressedThisFrame)
            {
                SceneManager.LoadScene("Main");
            }
        }
        else
        {
            alert.enabled = false;
        }

    }
}
