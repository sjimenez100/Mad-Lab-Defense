using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PlayButton : MonoBehaviour
{
    public static bool interactable = false;

    private void Awake()
    {
        GetComponent<Button>().interactable = interactable;
    }

}
