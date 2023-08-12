using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    [SerializeField]
    private Button continueButton;

    public void Start()
    {
        EventManager.main.enemyKillEvent += EnableButton;
    }

    public void EnableButton()
    {
        continueButton.interactable = true;
    }

    private void OnDestroy()
    {
        EventManager.main.enemyKillEvent -= EnableButton;
    }


}
