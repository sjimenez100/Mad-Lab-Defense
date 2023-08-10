using TMPro;
using UnityEngine.UI;
using UnityEngine;  

public class InputChecker : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField inputName;

    [SerializeField]
    private Button button;

    public void CheckName()
    {

        button.interactable = inputName.text.Length > 0;

    }

}
