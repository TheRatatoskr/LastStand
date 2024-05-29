using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float lifeTime = 5f;

    private void Start()
    {
        Destroy(this.gameObject, lifeTime);
    }
    void Update()
    {
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"I hit a thing named {other.gameObject.name}");
        if(other.gameObject.tag != "Player")
        {
            Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"I hit a thing named from collision {collision.gameObject.name}");
    }
}
