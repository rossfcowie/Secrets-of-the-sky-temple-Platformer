using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public Collider2D collider;
    public Counter myCounter;
    // Start is called before the first frame update
    void Awake()
    {
        collider = gameObject.GetComponent<Collider2D>();
        Counter.total++;
    }
    // Update is called once per frame
    void Update()
    {
        if(collider.IsTouching(Player.collider)){
            myCounter.collect();
            gameObject.SetActive(false);
        }
        
    }
}
