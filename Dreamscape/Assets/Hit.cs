using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    public enum TargetType {Player, Monster }
    public TargetType targetType;
    public int damage;
    public bool destroyAfter;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (targetType)
        {
            case TargetType.Player:
                if (collision.gameObject.tag == "Player")
                {
                    collision.gameObject.GetComponent<playerHealth>().GetHit(1); // Player always get hit by 1
                    if(destroyAfter)
                        Destroy(gameObject);
                }
                break;
            case TargetType.Monster:
                if (collision.gameObject.tag == "Enemy")
                {
                    collision.gameObject.GetComponent<Health>().GetHit(damage);
                    if(destroyAfter)
                        Destroy(gameObject);
                }
                break;
        }


    }
}
