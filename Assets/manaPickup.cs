using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manaPickup : MonoBehaviour
{
    
    public Collider2D collider;
    public float manaAmount;
    // Start is called before the first frame update
    void Awake()
    {
        collider = gameObject.GetComponent<Collider2D>();
        
    }
    void Update(){
        if (collider.IsTouching(Player.collider))
        {
            ManabarScript.singleton.mana+=manaAmount;
            Destroy(gameObject);
        }
    }
}
