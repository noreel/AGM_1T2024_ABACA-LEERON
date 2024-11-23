using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHp : MonoBehaviour
{
    public float maxHealth = 3;
    public float Eradius = 1f;
    public float currentHealth;

    private PlayerScore scoreCounter;
    private Buffs buff;
    private Buffs dmg;
    private GameObject[] bullets;
    private Animator anim;

   
    void Awake()
    {
        currentHealth = maxHealth;
        dmg = GameObject.FindGameObjectWithTag("Player").GetComponent<Buffs>();
        scoreCounter = GameObject.FindGameObjectWithTag("Score").GetComponent<PlayerScore>();
        buff = GameObject.FindGameObjectWithTag("Player").GetComponent<Buffs>();
        anim = gameObject.GetComponentInChildren<Animator>();
    }


    void Update()
    {
        bullets = GameObject.FindGameObjectsWithTag("bullet");
        
        if (bullets.Length > 0)
        {
            foreach (GameObject Bullet in bullets)
            {
                var bulletD = Bullet.GetComponent<BulletDamage>();
                float bulletR = bulletD.bulletRadius;
                Vector2 center1 = Bullet.transform.position;
                Vector2 center2 = gameObject.transform.position;

                if (IsThereCollision(center1, center2, bulletR) && currentHealth > 0)
                {
                    
                    TakeDamage(dmg.dmgModify);
                    Destroy(Bullet);
                }
            }
            
        }
    }

    public void TakeDamage (float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {

            StartCoroutine(DeathAnim());

        }
    }

    bool IsThereCollision(Vector2 center1, Vector2 center2, float radius)
    {
        float Cx = center1.x - center2.x;
        float Cy = center1.y - center2.y;
        float distance = Mathf.Sqrt(Cx * Cx + Cy * Cy);

        return distance <= radius + Eradius;
    }

    IEnumerator DeathAnim()
    {
        scoreCounter.scoreCount += 1f;
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        anim.SetTrigger("isDead");
        yield return new WaitForSeconds(.5f);
        Destroy(gameObject);
        buff.DropCoin(transform.position);
    }
}
