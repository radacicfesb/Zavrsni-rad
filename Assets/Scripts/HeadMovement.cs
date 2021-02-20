using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadMovement : MonoBehaviour//najvjerojatnije cu ovu skriptu maknit
{
    [SerializeField] float mouseSensitivity = 50f;

    [SerializeField] Transform player;

    float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;//zakljuca kursor da nestane s ekrana
    }

    
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;//dobivamo informaciju di je mis neovisno o frame rateu na osi x
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;//dobivamo informaciju di je mis neovisno o frame rateu na osi y

       // mouseY = Mathf.Clamp(mouseY, -90f, 90f);
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        player.Rotate(Vector3.up * mouseX);
    }
}
