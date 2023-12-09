using UnityEngine;

public class Boundaries : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.name != "playerShip")
        {

        }
        Destroy(other.gameObject);
    }
}
