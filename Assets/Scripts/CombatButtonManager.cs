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
        OpenButtonMenu(mainMenu);
        CloseButtonMenu(attacksMenu);
    }

    public void OpenButtonMenu(RoundMenuRotator menu)
    {
        currentMenu = menu;

        menu.interactable = true;

        for (int i = 0; i < menu.buttons.Length; i++)
        {
            menu.buttons[i].GetComponent<Button>().interactable = true;
        }

        currentMenu.defaultSelectedButton.GetComponent<Button>().Select();
    }

    public void CloseButtonMenu(RoundMenuRotator menu)
    {
        menu.interactable = false;

        for (int i = 0; i < menu.buttons.Length; i++)
        {
            menu.buttons[i].GetComponent<Button>().interactable = false;
        }
    }

    public void OpenAttacksMenu()
    {
        OpenButtonMenu(attacksMenu);
        CloseButtonMenu(mainMenu);
        attacksMenu.gameObject.SetActive(true);
        GetComponent<Animator>().Play("Attack Options Open");
    }

    public void CloseAttacksMenu()
    {
        GetComponent<Animator>().Play("Attack Options Close");

        OpenButtonMenu(mainMenu);
        CloseButtonMenu(attacksMenu);
    }

    void DisableAttacksMenu() { attacksMenu.gameObject.SetActive(false); }

    public void RotateMenuInput(InputAction.CallbackContext context)
    {
        currentMenu.RotateInput(context);
    }
}
