using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatArea : MonoBehaviour
{
    public Collider2D collider;
    void  Awake(){
        collider = GetComponent<Collider2D>();
    }
     void Update()
    {
        if(collider.IsTouching(Player.collider)){            
                Player.animator.SetBool("IsFloating", true);
        }
        }
        
    }

