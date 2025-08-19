using UnityEngine;
using UnityEngine.InputSystem;

public class CombatManager : MonoBehaviour
{
    public GameObject tempEnemy;
    public GameObject bashAttack;

    public CombatButtonManager buttonManager;

    public GameObject screenGFX;
    public GameObject damageIndicator;

    PlayerAttackGFX currentPlayerAttack;

    Turn currentTurn;

    public void StartPlayerTurn()
    {
        
    }

    public void EndTurn()
    {
        buttonManager.OpenButtonMenu(buttonManager.mainMenu);

        screenGFX.GetComponent<Animator>().SetTrigger("Toggle Focus");

        Instantiate(damageIndicator, tempEnemy.transform.position, damageIndicator.transform.rotation);
    }

    public void BashAttack()
    {
        currentPlayerAttack = Instantiate(bashAttack, tempEnemy.transform.position, bashAttack.transform.rotation).GetComponent<PlayerAttackGFX>();
        currentPlayerAttack.combatManager = this;
    }

    public void ChangeTurn(Turn turn)
    {
        currentTurn.EndTurn();
        currentTurn = turn;
        currentTurn.StartTurn();
    }

    public void Input(InputAction.CallbackContext context)
    {
        if (currentPlayerAttack == null) return;
        currentPlayerAttack.AttackInput(context);
    }
}

public abstract class Turn
{
    public abstract void StartTurn();
    public abstract void EndTurn();
    public abstract void Attack();
}

public class PlayerTurn : Turn
{
    public CombatManager manager;
    public override void StartTurn(CombatManager combatManager)
    {
        manager = combatManager;

        manager.buttonManager.CloseAttacksMenu();
        manager.buttonManager.CloseButtonMenu(manager.buttonManager.mainMenu);

        Invoke(nameof(Attack), 0.25f);

        screenGFX.GetComponent<Animator>().SetTrigger("Toggle Focus");
    }
}