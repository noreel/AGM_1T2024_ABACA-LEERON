using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHp : MonoBehaviour
{
    public int maxHealth = 5;
    public float Pradius = .5f;
    public bool isVulnerable = true;
    public float iFramesDuration;
    public int numberOfFlashes;
    public SpriteRenderer spriteRender;
    public int currentHealth;
    public Animator anim;
    public GameOverScreen gOver;
    public PlayerScore pScore;
    public TextMeshProUGUI hp;

    private GameObject[] enemy;
    
       
    void Awake()
    {
        currentHealth = maxHealth;        
    }

    void Update()
    {
        enemy = GameObject.FindGameObjectsWithTag("Enemy");
        
        hp.text = "" + currentHealth;

        if (enemy.Length > 0)
        {
            foreach (GameObject Enemy in enemy)
            {
                var enemHp = Enemy.GetComponent<EnemyHp>();
                float enemR = enemHp.Eradius;
                
                Vector2 center1 = Enemy.transform.position;
                Vector2 center2 = gameObject.transform.position;

                if (IsThereCollision(center1, center2, enemR) && isVulnerable && enemHp.currentHealth > 0)
                {                    
                    TakeDamage(1);
                    StartCoroutine(Invulnerability());
                }
            }
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            isVulnerable = false;
            StartCoroutine(DeathAnim());            
        }
    }

    bool IsThereCollision(Vector2 center1, Vector2 center2, float radius)
    {
        float Cx = center1.x - center2.x;
        float Cy = center1.y - center2.y;
        float distance = Mathf.Sqrt(Cx * Cx + Cy * Cy);
        
        return distance <= Pradius + radius;
    }

    private IEnumerator Invulnerability()
    {
        Debug.Log("invulnerable");
        if (isVulnerable == true)
        {
            isVulnerable = false;
            for (int i = 0; i < numberOfFlashes; i++)
            {
                spriteRender.color = new Color(1, 0, 0, .5f);
                yield return new WaitForSeconds(.5f);
                spriteRender.color = Color.white;
                yield return new WaitForSeconds(.5f);
            }
            isVulnerable = true;
        }
    }

    private IEnumerator DeathAnim()
    {
        anim.SetBool("isDead", true);
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
        gOver.Setup(pScore.scoreCount);
        
    }
}
