using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryPlatform : MonoBehaviour
{
    public float life = 8f;
    float baselife;
    // Start is called before the first frame update
    void Awake()
    {
        baselife = life;
    }

    // Update is called once per frame
    void Update()
    {
        life-=Time.deltaTime;
        Color c = GetComponent<SpriteRenderer>().color;
        GetComponent<SpriteRenderer>().color  = new Color(c.r, c.g, c.b,life/baselife);
        if(life <=0){
            Destroy(gameObject);
        }
    }
}
