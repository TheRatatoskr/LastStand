using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMuncher : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Hazard")
        {
            Debug.Log("yum yum, i got bullets in my tummy");
            Destroy(other.gameObject);
        }
    }
}
