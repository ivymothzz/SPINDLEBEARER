using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Alchemy.Inspector;
using System.Linq;

public class RoundMenuRotator : MonoBehaviour
{
    public Transform buttonPointsParent;

    public float offset;
    public float speed;

    public int emptyIterations;

    public bool interactable;

    [Space(25)]

    public GameObject[] buttons;
    public GameObject[] buttonPoints;
    [HelpBox("Must start from the rightmost button", UnityEngine.UIElements.HelpBoxMessageType.Info)]

    [Space(25)]

    public GameObject defaultSelectedButton;

    float targetRotation;

    Button currentButton;
    int currentButtonIndex = 1; // THIS MIGHT CAUSE ISSUES IF THE DEFAULTSELECTEDBUTTON IS NOT THE 2ND BUTTON IN THE BUTTONS ARRAY

    private void Update()
    {
        Quaternion target = Quaternion.AngleAxis(targetRotation, Vector3.forward);
        buttonPointsParent.transform.rotation = Quaternion.Slerp(buttonPointsParent.transform.rotation, target, Time.deltaTime * speed);

        for (int i = 0; i < buttons.Length; i++) buttons[i].transform.position = buttonPoints[i].transform.position;
    }

    private void Start()
    {
        ArrangeButtons();

        currentButton = defaultSelectedButton.GetComponent<Button>();
        currentButton.Select();
    }

    [Button]
    public void ArrangeButtons()
    {
        int iterationCount = buttons.Length + emptyIterations;
        for (int i = 0; i < iterationCount; i++)
        {
            /* Distance around the circle */
            var radians = 2 * Mathf.PI / iterationCount * i;

            /* Get the vector direction */
            var vertical = Mathf.Sin(radians);
            var horizontal = Mathf.Cos(radians);

            var spawnDir = new Vector3(horizontal, vertical, 0);

            /* Get the spawn position */
            var spawnPos = buttonPointsParent.transform.position + spawnDir * offset;

            if (i < buttons.Length)
            {
                buttons[i].transform.position = spawnPos;
                buttonPoints[i].transform.position = spawnPos;
            }
        }
    }

    public void RotateInput(InputAction.CallbackContext context)
    {
        if (!context.performed || !interactable) return;

        int iterationCount = buttons.Count() + emptyIterations; // the amount to rotate, number of buttons + empty iterations taken while arranging buttons

        if (context.ReadValue<float>() == -1 && currentButtonIndex != buttons.Length - 1) // if user pressed left and the selected button is not the last button in index
        {
            targetRotation += 360 / -iterationCount;
            currentButtonIndex++;
        }
        else if (context.ReadValue<float>() == 1 && currentButtonIndex != 0) // if the player pressed right and the selected button is not the first in index
        {
            targetRotation += 360 / iterationCount;
            currentButtonIndex--;
        }

        currentButton = buttons[currentButtonIndex].GetComponent<Button>();
        currentButton.Select();

    }
}
