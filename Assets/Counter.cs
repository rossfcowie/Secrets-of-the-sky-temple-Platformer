using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    public static int total;
    public static int collected;
    private Text myText;
    // Start is called before the first frame update
    void Awake()
    {
        myText=GetComponentInChildren<Text>();
    }
public void collect(){
    collected++;
}
    // Update is called once per frame
    void FixedUpdate()
    {
        myText.text = collected + "/" + total;
        
    }
}
