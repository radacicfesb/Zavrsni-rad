using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField] AudioClip jumpingSFX;
    [SerializeField] AudioClip ouchSFX;
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
        onground = Physics.CheckSphere(myBody.transform.position, 5f, groundLayer);
        if(landing)
            audios.Play();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            hitCounter++;
            if (hitCounter == 1)
                AudioSource.PlayClipAtPoint(ouchSFX, Camera.main.transform.position);
            if (hitCounter == 2)
                //odsviraj neki zvuk ka da je umra ili tako nesto
                playerMovement.Die();
        }

    }

   // private void OnTriggerEnter(Collider other)
    //{

      //  if (other.gameObject.tag == "Pickaxe" && hitCounter == 1) //i nema nijedan pickaxe u intventoryu
        //    hitCounter--;
    //}
}
