using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PlayButton : MonoBehaviour
{
    public static bool interactable = true;

    private void Awake()
    {
        GetComponent<Button>().interactable = interactable;
    }

}
