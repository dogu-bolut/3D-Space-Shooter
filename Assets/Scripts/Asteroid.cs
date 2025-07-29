using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] public GameObject Vfx;
    [SerializeField] float rotateSpeed = 1f;
    [SerializeField] float moveSpeed = 1f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.angularVelocity = Random.insideUnitSphere * rotateSpeed;
        rb.linearVelocity = transform.forward * moveSpeed;
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag != "Enemy")
        {            
            Destroy(gameObject);
            Instantiate(Vfx, transform.position, transform.rotation);
            FindObjectOfType<Spawn>().MakeScore(10);
        }
    }

}
