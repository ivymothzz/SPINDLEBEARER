using UnityEngine;
using UnityEngine.InputSystem;

public class AttackGFX : MonoBehaviour
{
    public GameObject starsParticles;

    bool early;
    bool late;

    bool attacked;

    [HideInInspector] public Turn origin;

    public void StartEarly()
    {
        early = true;
    }

    public void Impact()
    {
        early = false;
    }

    public void StartLate()
    {
        late = true;
    }

    public void End()
    {
        origin.EndTurn();

        if (!attacked) Debug.Log("did not attack");

        Destroy(gameObject);
    }

    public void AttackInput(InputAction.CallbackContext context)
    {
        if (!context.performed || attacked) return;

        attacked = true;

        if (early) Debug.Log("early!");
        else if (late) Debug.Log("late!");
        else
        {
            Debug.Log("perfect!");
            Instantiate(starsParticles, transform.position, starsParticles.transform.rotation);
        }

        origin.target.GetComponent<Damageable>().TakeDamage();
    }
}
