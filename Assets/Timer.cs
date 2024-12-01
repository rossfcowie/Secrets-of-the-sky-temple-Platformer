using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
   private DateTime time;
    public Text text;
    // Start is called before the first frame update
    void Awake()
    {
        
    time = DateTime.Now;
    }

    // Update is called once per frame
    void Update() { 
        if(Counter.total>Counter.collected){
        TimeSpan elapsedTime = DateTime.Now - time; 
        text.text = elapsedTime.ToString(@"mm\:ss\:fff");
        }
        }
}
