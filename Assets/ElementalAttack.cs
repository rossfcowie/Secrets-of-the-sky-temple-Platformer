
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;

public class ElementalAttack : MonoBehaviour
{
    public type myType;
    public LayerMask BreakableLayer;
    public Collider2D collider;
    // Start is called before the first frame update
    private void Update()
    {
            Debug.Log("Check");
        if (collider.IsTouchingLayers(BreakableLayer))
        {
            Debug.Log("Break");
            GameObject x = Physics2D.OverlapCircleAll(new Vector2((collider.bounds.min.x + collider.bounds.max.x) / 2, (collider.bounds.min.y + collider.bounds.max.y) / 2),1f, BreakableLayer).First().gameObject;
            x.GetComponentInParent<Breakable>().BreakMe(myType);
        }
        else
        {
        }
    }
}
