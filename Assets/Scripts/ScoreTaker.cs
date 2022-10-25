using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTaker : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
        FindObjectOfType<Spawn>().TakeScore(100);
    }
}
