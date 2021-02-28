using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField] AudioClip jumpingSFX;
    [SerializeField] AudioClip ouchSFX;
   // [SerializeField] AudioClip deathSFX;
    AudioSource audios;

    [SerializeField]  LayerMask groundLayer;
    bool onground = false;

    CapsuleCollider myBody;

    bool jumpInput;

    PlayerMovement playerMovement;
    public int hitCounter = 0;//samo da mi se vidi u inspectoru
    void Start()
    {
        audios = GetComponent<AudioSource>();
        myBody = GetComponent<CapsuleCollider>();
        playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
    }

    void Update()
    {
        if (!playerMovement.alive) audios.Stop();
        jumpInput = Input.GetButtonDown("Jump");
       

        if (jumpInput)
        {
            audios.Stop();
            AudioSource.PlayClipAtPoint(jumpingSFX, Camera.main.transform.position);
        }
        else
            GroundCheck(Input.GetButtonUp("Jump"));
    }
    void GroundCheck(bool landing)
    {
        if (!playerMovement.alive) return;
        onground = Physics.CheckSphere(myBody.transform.position, 5f, groundLayer);
        if(landing)
            audios.Play();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (!playerMovement.alive) return;
        if (collision.gameObject.tag == "Obstacle")
        {
            hitCounter++;
            if (hitCounter == 1)
                AudioSource.PlayClipAtPoint(ouchSFX, Camera.main.transform.position);
            if (hitCounter == 2)
            {
                AudioSource.PlayClipAtPoint(ouchSFX, Camera.main.transform.position);
                playerMovement.Die();
            }
        }

    }
}
