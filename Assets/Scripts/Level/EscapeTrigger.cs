using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeTrigger: MonoBehaviour
{
    private StageManager stageManager;

    private void Start()
    {
        stageManager = FindObjectOfType<StageManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            stageManager.StageWasCleared();
            Destroy(this.gameObject);
        }
    }
}
