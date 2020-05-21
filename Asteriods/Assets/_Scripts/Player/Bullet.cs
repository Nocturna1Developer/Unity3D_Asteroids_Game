using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Bullet : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Astroids")
        {
            Destroy(bulletPrefab);
        }
    }
}
