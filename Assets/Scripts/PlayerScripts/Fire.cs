using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletForce = 20f;
    public SpriteRenderer muzzle;
    public float fireRate = 0.7f;

    private CamShake camShake;
    private float nextFireRate = 0f;

    void Start()
    {
        camShake = Camera.main.GetComponent<CamShake>();
    }

    void Update()
    {
        
        if(Input.GetButtonDown("Fire1") && Time.time >= nextFireRate)
        {
            StartCoroutine(Shoot());
            nextFireRate = Time.time + fireRate;
        }
    }

    IEnumerator Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, gameObject.transform.position, gameObject.transform.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(gameObject.transform.up * bulletForce, ForceMode2D.Impulse);
        muzzle.enabled = true;

        StartCoroutine(camShake.Shake(.1f,.1f));

        yield return new WaitForSeconds(.1f);
        muzzle.enabled = false;
    }

}
