using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    Timer timer;
    PlayerSounds playerSounds;
    [SerializeField] AudioClip powerUpSFX;
    
    private void Start()
    {
        timer = FindObjectOfType<Timer>();
        playerSounds = FindObjectOfType<PlayerSounds>();
      
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Beer")
        {
            Time.timeScale = 1.5f;
            StartCoroutine(ReturnTime());
            AudioSource.PlayClipAtPoint(powerUpSFX, Camera.main.transform.position);
        }
        else if (other.gameObject.tag == "Gold Clock")
        {
            timer.AddFiveSecondsToTimer();
            AudioSource.PlayClipAtPoint(powerUpSFX, Camera.main.transform.position);
        }
        else if (other.gameObject.tag == "Black Clock")
        {
            timer.TakeFiveSecondsFromTimer();
            AudioSource.PlayClipAtPoint(powerUpSFX, Camera.main.transform.position);
        }
        else if (other.gameObject.tag == "Pickaxe" && playerSounds.hitCounter == 1)
        {
            playerSounds.hitCounter--;
            AudioSource.PlayClipAtPoint(powerUpSFX, Camera.main.transform.position);
        }
        
    }

    IEnumerator ReturnTime()
    {
        yield return new WaitForSeconds(2f);

        Time.timeScale = 1f;
    }

   
}
