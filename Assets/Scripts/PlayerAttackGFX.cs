using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackGFX : MonoBehaviour
{
    bool early;
    bool late;

    bool attacked;

    [HideInInspector] public CombatManager combatManager;

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
        combatManager.EndTurn();

        if (!attacked) Debug.Log("did not attack");

        Destroy(gameObject);
    }

    public void AttackInput(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        Debug.Log("bloing");

        attacked = true;

        if (early) Debug.Log("early!");
        else if (late) Debug.Log("late!");
        else Debug.Log("perfect!");
    }
}
