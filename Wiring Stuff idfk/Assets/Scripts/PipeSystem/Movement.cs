using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [Range(.1f, 8f)]
    public float speed = 5;

    public float sensivity = 100;
    private float xRotation = 0f;

    public Camera playerCam;
    public GameObject orientation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
        float z = Input.GetAxisRaw("Vertical") * speed * Time.deltaTime;



        transform.position += orientation.transform.forward * z + orientation.transform.right * x;

        float mouseX = Input.GetAxis("Mouse X") * sensivity * Time.fixedDeltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensivity * Time.fixedDeltaTime;

        Vector3 rot = playerCam.transform.localRotation.eulerAngles;
        float desiredX = rot.y + mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCam.transform.localRotation = Quaternion.Euler(xRotation, desiredX, 0);
        orientation.transform.localRotation = Quaternion.Euler(0, playerCam.transform.eulerAngles.y, 0);
    }

}
