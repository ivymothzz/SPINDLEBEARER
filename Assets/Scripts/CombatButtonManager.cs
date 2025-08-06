using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class CombatButtonManager : MonoBehaviour
{
    public RoundMenuRotator mainMenu;
    public RoundMenuRotator attacksMenu;

    public AnimationClip closeAttacks;

    RoundMenuRotator currentMenu;

    private void Start()
    {
        OpenButtonMenu(mainMenu, attacksMenu);
    }

    public void OpenButtonMenu(RoundMenuRotator menuToOpen, RoundMenuRotator menuToClose)
    {
        currentMenu = menuToOpen;

        menuToClose.interactable = false;
        menuToOpen.interactable = true;

        for (int i = 0; i < menuToClose.buttons.Length; i++)
        {
            menuToClose.buttons[i].GetComponent<Button>().interactable = false;
        }

        for (int i = 0; i < menuToOpen.buttons.Length; i++)
        {
            menuToOpen.buttons[i].GetComponent<Button>().interactable = true;
        }
    }

    public void OpenAttacksMenu()
    {
        OpenButtonMenu(attacksMenu, mainMenu);
        attacksMenu.gameObject.SetActive(true);
        GetComponent<Animator>().Play("Attack Options Open");
    }

    public void CloseAttacksMenu()
    {
        GetComponent<Animator>().Play("Attack Options Close");

        OpenButtonMenu(mainMenu, attacksMenu);
        mainMenu.currentButton.Select();
    }

    void DisableAttacksMenu() { attacksMenu.gameObject.SetActive(false); }

    public void RotateMenuInput(InputAction.CallbackContext context)
    {
        currentMenu.RotateInput(context);
    }
}
