using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    Timer timer;
    PlayerSounds playerSounds;
    [SerializeField] AudioClip powerUpSFX;
    AudioSource audioSource;
    private void Start()
    {
        timer = FindObjectOfType<Timer>();
        playerSounds = FindObjectOfType<PlayerSounds>();
        audioSource = FindObjectOfType<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Beer")
        {
            Time.timeScale = 1.5f;
            StartCoroutine(ReturnTime());
            audioSource.PlayOneShot(powerUpSFX);
        }
        else if (other.gameObject.tag == "Gold Clock")
        {
            timer.AddFiveSecondsToTimer();
            audioSource.PlayOneShot(powerUpSFX);
        }
        else if (other.gameObject.tag == "Black Clock")
        {
            timer.TakeFiveSecondsFromTimer();
            audioSource.PlayOneShot(powerUpSFX);
        }
        else if (other.gameObject.tag == "Pickaxe" && playerSounds.hitCounter == 1)
        {
            playerSounds.hitCounter--;
            audioSource.PlayOneShot(powerUpSFX);
        }
        
    }

    IEnumerator ReturnTime()
    {
        yield return new WaitForSeconds(2f);

        Time.timeScale = 1f;
    }

   
}
