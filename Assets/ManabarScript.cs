using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManabarScript : MonoBehaviour
{
    public static ManabarScript singleton;
    public float mana = 100;
    public float manacap=100;
    
    // Start is called before the first frame update
    void Start()
    {
        singleton = this;
    }
    public Image ManaBarDisplay;
    // Update is called once per frame
    void Update()
    {
        mana = Math.Min(mana, manacap);
        ManaBarDisplay.fillAmount = mana/manacap;
    }
}
