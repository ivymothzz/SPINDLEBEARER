using UnityEngine;
using UnityEngine.InputSystem;

public class CombatManager : MonoBehaviour
{
    public GameObject tempEnemy;
    public GameObject bashAttack;

    public CombatButtonManager buttonManager;

    PlayerAttackGFX currentPlayerAttack;

    public void StartPlayerTurn()
    {
        buttonManager.CloseAttacksMenu();
        buttonManager.CloseButtonMenu(buttonManager.mainMenu);

        Invoke(nameof(BashAttack), 0.25f);
    }

    public void EndTurn()
    {
        buttonManager.OpenButtonMenu(buttonManager.mainMenu);
    }

    public void BashAttack()
    {
        currentPlayerAttack = Instantiate(bashAttack, tempEnemy.transform.position, bashAttack.transform.rotation).GetComponent<PlayerAttackGFX>();
        currentPlayerAttack.combatManager = this;
    }

    public void Input(InputAction.CallbackContext context)
    {
        if (currentPlayerAttack == null) return;
        currentPlayerAttack.AttackInput(context);
    }
}
