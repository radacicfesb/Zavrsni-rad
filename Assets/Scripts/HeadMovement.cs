using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadMovement : MonoBehaviour//najvjerojatnije cu ovu skriptu maknit
{
    [SerializeField] float mouseSensitivity = 100f;

    [SerializeField] Transform cam;

    float xRotation = 0f;
    PlayerMovement playerMovement;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;//zakljuca kursor da nestane s ekrana
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    
    void Update()
    {
        if (!playerMovement.alive) return;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;//dobivamo informaciju di je mis neovisno o frame rateu na osi y
 
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation , 0f, 0f);
        cam.Rotate(Vector3.up * mouseY);
    }
}
