using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
     
    public float followSpeed = 2.0f; 
    public float maxDistance = 0.1f;
    private SpriteRenderer sprite;
    private GameObject player;
    private EnemyHp enemHp;
    

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        sprite = gameObject.GetComponentInChildren<SpriteRenderer>();
        enemHp = gameObject.GetComponent<EnemyHp>();
    }

    void Update()
    {
        
        if (player != null && enemHp.currentHealth > 0)
        {
            
            Vector3 direction = player.transform.position - transform.position;
            float distance = Mathf.Sqrt(direction.x * direction.x + direction.y * direction.y);

            Vector3 normalizedDirection = direction / distance;
            
            if (distance > maxDistance)
            {                
                Vector3 newPosition = Vector3.Lerp(transform.position, transform.position + normalizedDirection, followSpeed * Time.deltaTime);                
                transform.position = newPosition;                
            }

            if (player.transform.position.x > gameObject.transform.position.x && sprite.flipX)
            {                    
                sprite.flipX = false;                    
            }
            else if (player.transform.position.x < gameObject.transform.position.x && !sprite.flipX)
            {         
                sprite.flipX = true;
            }

        }
        

    }
}
