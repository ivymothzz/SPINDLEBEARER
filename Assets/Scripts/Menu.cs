using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public bool close;
    public bool openByDefault;

    public void OpenButtonMenu(Menu menu)
    {
        GetComponent<RoundMenuRotator>().interactable = false;
        menu.GetComponent<RoundMenuRotator>().interactable = true;

        menu.gameObject.SetActive(true);
        
        GameObject[] buttons = GetComponent<RoundMenuRotator>().buttons;
        
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponent<Button>().interactable = false;
        }

        for (int i = 0; i < menu.GetComponent<RoundMenuRotator>().buttons.Length; i++)
        {
            menu.GetComponent<RoundMenuRotator>().buttons[i].GetComponent<Button>().interactable = true;
        }

        if (close) gameObject.SetActive(false);
    }
}
