using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{

    public type myType;
    public float life = -15f;
    public GameObject body;
    public GameObject baby;
    // Update is called once per frame
    void Update()
    {
        life -= Time.deltaTime;
        if (life <= 0 && life >= -16f)
        {
            Destroy(baby);
            baby = null;
        }
        if (life <= -16f && baby == null)
        {
            baby = Instantiate(body, transform);
        }

    }
    public void BreakMe(type t)
    {
        if (t == myType)
        {
                if (life > -16)
                {
                    life = 1f;

                }
                else
                {
                    if (life < 0)
                    {
                        life = -1f;

                    }
                }
        }
    }
}
