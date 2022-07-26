using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissScore : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "ball")
        {
            Player.missPoint += 1;
        }

    }
}
