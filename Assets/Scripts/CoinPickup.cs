using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinSFX;
    [SerializeField] float volume = 1f;
    [SerializeField] int coinValue = 100;

    // prevent double pickup (not a problem for me)
    bool wasCollected = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !wasCollected)
        {
            wasCollected = true;

            FindObjectOfType<GameSession>().IncreaseScore(coinValue);

            // using AudioSource coinSFX -> coinSFX.Play() would give problems since this gameObject gets deleted right after playing sound!
            // AudioSource.PlayClipAtPoint(): won't be destroyed

            // Play sound at position of camera! (= where headset is)
            AudioSource.PlayClipAtPoint(coinSFX, Camera.main.transform.position, volume);

            // prevent double pickup (not a problem for me)
            gameObject.SetActive(false);
            
            Destroy(gameObject);
        }
    }
}
