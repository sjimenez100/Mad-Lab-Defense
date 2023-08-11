using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DoubleDoorController : MonoBehaviour
{
    [SerializeField]
    private Animator animatorLeft;
    [SerializeField]
    private Animator animatorRight;

    private bool insideCollider;

    // [SerializeField]
    //private float speedMultiple;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OpenDoors(true);

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        OpenDoors(false);

    }
    private void OpenDoors(bool open)
    {
        animatorLeft.SetBool("doorOpen", open);
        animatorRight.SetBool("doorOpen", open);
    }

}


