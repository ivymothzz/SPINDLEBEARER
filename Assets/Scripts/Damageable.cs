using UnityEngine;

public class Damageable : MonoBehaviour
{
    public GameObject hitEffect;
    public void TakeDamage()
    {
        Instantiate(hitEffect, transform.position, hitEffect.transform.rotation);
    }
}
