using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CombatManager : MonoBehaviour
{
    public GameObject tempEnemy;
    public GameObject bashAttack;

    public CombatButtonManager buttonManager;

    public GameObject screenGFX;

    AttackGFX currentPlayerAttack;

    Turn currentTurn;
    PlayerTurn playerTurn = new PlayerTurn();

    bool buttonsOpen = true;

    public void StartPlayerTurn()
    {
        playerTurn.manager = this;
        playerTurn.target = tempEnemy.transform;

        ChangeTurn(playerTurn);
    }

    public void ChangeTurn(Turn turn)
    {
        if (currentTurn != null) currentTurn.EndTurn();
        currentTurn = turn;
        currentTurn.StartTurn();
    }

    public void InstantianteAttack(AttackGFX attack, Transform target, Turn origin)
    {
        currentPlayerAttack = Instantiate(attack, target.transform.position, attack.transform.rotation);
        currentPlayerAttack.origin = origin;
    }

    public void ToggleScreenFade()
    {
        screenGFX.GetComponent<Animator>().SetTrigger("Toggle Focus");
    }

    public void ToggleButtons()
    {
        if (buttonsOpen)
        {
            buttonManager.CloseAttacksMenu();
            buttonManager.CloseButtonMenu(buttonManager.mainMenu);
        }
        else buttonManager.OpenButtonMenu(buttonManager.mainMenu);

        buttonsOpen = !buttonsOpen;
    }

    public void WaitForAttack()
    {
        Invoke(nameof(CurrentTurnAttack), 0.25f);
    }

    void CurrentTurnAttack()
    {
        currentTurn.Attack();
    }

    public void Input(InputAction.CallbackContext context)
    {
        if (currentPlayerAttack == null) return;
        currentPlayerAttack.AttackInput(context);
    }
}

public abstract class Turn
{
    public Transform target;

    public abstract void StartTurn();
    public abstract void EndTurn();
    public abstract void Attack();
}

public class PlayerTurn : Turn
{
    public CombatManager manager;

    public override void StartTurn()
    {
        manager.ToggleScreenFade();
        manager.ToggleButtons();

        manager.WaitForAttack();

    }

    public override void EndTurn()
    {
        manager.ToggleScreenFade();
        manager.ToggleButtons();
    }

    public override void Attack()
    {
        manager.InstantianteAttack(manager.bashAttack.GetComponent<AttackGFX>(), target, this);
    }
}