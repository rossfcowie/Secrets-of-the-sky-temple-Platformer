using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killarea : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
                Player.animator.SetBool("IsDying", true);
        }
    }
}
