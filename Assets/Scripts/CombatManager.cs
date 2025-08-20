using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CombatManager : MonoBehaviour
{
    public GameObject tempEnemy;
    public GameObject bashAttack;
    public GameObject targetIndicator;

    public CombatButtonManager buttonManager;

    public GameObject screenGFX;

    AttackGFX currentPlayerAttack;

    Turn currentTurn;
    PlayerTurn playerTurn = new PlayerTurn();

    GameObject currentTargetIndicator;

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

        //Destroy(currentTargetIndicator);
    }

    public void InstantianteAttack(AttackGFX attack, Transform target, Turn origin)
    {
        currentPlayerAttack = Instantiate(attack, target.transform.position, attack.transform.rotation);
        currentPlayerAttack.origin = origin;
    }

    public void InstantiateTargetIndicator(Transform target)
    {
        currentTargetIndicator = Instantiate(targetIndicator, target.transform.position, targetIndicator.transform.rotation);
    }

    public void ToggleScreenFade()
    {
        screenGFX.GetComponent<Animator>().SetTrigger("Toggle Focus");
    }

    public void EnableButtons()
    {
        buttonManager.OpenButtonMenu(buttonManager.mainMenu);
    }

    public void DisableButtons()
    {
        buttonManager.CloseAttacksMenu();
        buttonManager.CloseButtonMenu(buttonManager.mainMenu);
    }

    public void WaitForAttack()
    {
        Invoke(nameof(CurrentTurnAttack), 0.4f);
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
        manager.DisableButtons();
        manager.InstantiateTargetIndicator(target);

        manager.WaitForAttack();
    }

    public override void EndTurn()
    {
        manager.ToggleScreenFade();
        manager.EnableButtons();

        
    }

    public override void Attack()
    {
        manager.InstantianteAttack(manager.bashAttack.GetComponent<AttackGFX>(), target, this);
    }
}