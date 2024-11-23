using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buffs : MonoBehaviour
{
    public float radius = .5f;
    public float dmgModify = 1;
    public GameObject[] coinPrefabs;
    public float dropChance = 0.01f;
    public float killCount;
    public BuffCooldownUI buffUI;

    private PlayerMovement pM;
    private PlayerHp pHp;
    private GameObject[] coin;

    void Start()
    {
        pM = GetComponent<PlayerMovement>();
        pHp = GetComponent<PlayerHp>();
    }

    void Update()
    {
        coin = GameObject.FindGameObjectsWithTag("Coin");

        if (coin.Length > 0)
        {

            foreach (GameObject Coin in coin)
            {

                Vector2 center1 = Coin.transform.position;
                Vector2 center2 = gameObject.transform.position;

                if (IsThereCollision(center1, center2, radius))
                {

                    StartCoroutine(BuffDuration(Coin));
                    Destroy(Coin);
                    
                }
            }

        }

    }

    bool IsThereCollision(Vector2 center1, Vector2 center2, float radius)
    {
        float Cx = center1.x - center2.x;
        float Cy = center1.y - center2.y;
        float distance = Mathf.Sqrt(Cx * Cx + Cy * Cy);

        return distance <= 2 * radius;
    }

    private IEnumerator BuffDuration(GameObject Buff)
    {

        if (Buff.name == "Speed(Clone)")
        {
            
            pM.speed = 7f;
            buffUI.ShowSpeedBuff(10f);
            yield return new WaitForSeconds(10f);
            pM.speed = 5f;
            
        }

        else if (Buff.name == "Health(Clone)")
        {
            if (pHp.currentHealth < pHp.maxHealth)
            {

                pHp.currentHealth += 1;
            }
            
        }

        else if (Buff.name == "Damage(Clone)")
        {

            dmgModify = 1.5f;
            buffUI.ShowDamageBuff(10f);
            yield return new WaitForSeconds(10f);
            dmgModify = 1f;

        }
        
    }

    public void DropCoin(Vector3 position)
    {
        killCount++;
        if (killCount >= 1)
        {
            killCount = 0;
            if (Random.value <= dropChance)
            {
                int coinIndex = Random.Range(0, coinPrefabs.Length);
                GameObject coin = Instantiate(coinPrefabs[coinIndex], position, Quaternion.identity);
                Destroy(coin, 4f);
            }
        }

    }
}


