using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinSFX;
    [SerializeField] float volume = 1f;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            // using AudioSource coinSFX -> coinSFX.Play() would give problems since this gameObject gets deleted right after playing sound!
            // AudioSource.PlayClipAtPoint(): won't be destroyed

            // Play sound at position of camera! (= where headset is)
            AudioSource.PlayClipAtPoint(coinSFX, Camera.main.transform.position, volume);
            Destroy(gameObject);
        }
    }
}
