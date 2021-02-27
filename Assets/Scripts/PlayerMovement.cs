﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    [SerializeField] Rigidbody myRigidbody;

    float horizontalInput;
    [SerializeField] float horizontalFactor = 2f;//igrac ide presporo ulijevo i udesno

    float jumpInput;
    [SerializeField] float jumpSpeed = 100f;

    bool alive = true;

    public float speedIncreasePerSeconds;
    

    private void FixedUpdate()//zovemo fixed update jer se zove 50 puta u sekundi da bi malo bolju kontrolu nad fizikon
    {
        if (!alive) return;

        Vector3 forwardMove = transform.forward * moveSpeed * Time.fixedDeltaTime;//micemo se 5 polja svaku sekundu a ne svaki frame
        Vector3 horizontalMove = transform.right * horizontalInput * moveSpeed * Time.fixedDeltaTime * horizontalFactor;
        Vector3 jumpMove = transform.up * jumpInput * jumpSpeed * Time.fixedDeltaTime;
        Vector3 direction = (myRigidbody.position + forwardMove + horizontalMove + jumpMove);//rijesen problem s colliderima, sad ne moze proc livo i desno od onog sta je zacrtano
        direction.x = Mathf.Clamp(direction.x, -4.5f, 4.5f);
        myRigidbody.MovePosition(direction);
        
    }

    void Update()
    {
        

        horizontalInput = Input.GetAxis("Horizontal");
        jumpInput = Input.GetAxis("Jump");

        //Vector3 vel = myRigidbody.velocity;

        if (transform.position.y < -5)//ako padne s platforme isto ga ubij, ako maknem skriptu HeadMovement ovo maknit
        {
            Die();
        }

      //  CheckPosition();
    }

    public void Die()
    {
        alive = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);//ucitat ce odma ovu scenu al prominit cu to kasnije
    }

  /* void CheckPosition()
    {
        if (transform.position.x == -4.4f || transform.position.x == 4.4f)
        {
            if (!timerStarted)
            {
                timerStarted = true;
                timer = 0f;
            }
            else
            {
                timer += Time.deltaTime;
            }

            if (timer >= 2f)
                timerForTimer = false;
            else
                timerStarted = false;
        }
            
    }*/
}
