using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "ball")
        {
            Player.numberOfPoint += 1;
            Player.instance.ballAnim.enabled = false; 
            Player.instance.missArea.gameObject.SetActive(false);
        }

    }
}
