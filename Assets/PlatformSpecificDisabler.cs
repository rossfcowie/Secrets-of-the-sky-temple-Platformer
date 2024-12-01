using System.Collections.Generic;
using UnityEngine;

public class PlatformSpecificDisabler : MonoBehaviour
{
    //A list of platforms this gameobject is disabled on.
    public List<RuntimePlatform> DisabledPlatforms = new List<RuntimePlatform>();
    void Start()
    {
        if (DisabledPlatforms.Contains(Application.platform)){
            gameObject.SetActive(false);
        }else{
            gameObject.SetActive(true);
        }
    }
}
