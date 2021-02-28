using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    [SerializeField] Vector3 movementVector = new Vector3(0f, 1f, 0f);
    [SerializeField] float period = 2f;
    [SerializeField] Transform target;

    float movementFactor; // 0 ako se ne mice, 1 za skroz se pomaklo
    Vector3 startingPos;

    float jumpInput;
    PlayerMovement playerMovement;
    void Start()
    {
        startingPos = transform.position;
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    
    void Update()
    {
        if (!playerMovement.alive) return;
        jumpInput = Input.GetAxis("Jump");
        if (Mathf.Abs(jumpInput) > Mathf.Epsilon) return;

        // stiti ako je 0
        float cycles = Time.time / period; // raste kontinuirano od nule

        const float tau = Mathf.PI * 2f; // oko 6.28
        float rawSinWave = Mathf.Sin(cycles * tau); //ide od -1 do +1

        movementFactor = rawSinWave / 2f + 0.5f;
        Vector3 offset = movementFactor * movementVector;
        transform.position = startingPos + offset + target.position;
    }

}
