using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ProjectileSelectionUI : MonoBehaviour, IColorable
{
    [InspectorName("Color")]
    public Color.ColorType setColor;
    public Color.ColorType color { get; set; }

    private Animator animator;
    private void Awake()
    {
        color = setColor;
        animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        EventManager.main.changeColorEvent += Selection;
    }

    void Selection(int id)
    {
        bool popUp = (Color.ColorType)id == color;
        animator.SetBool("popUp", popUp);
  
    }

    private void OnDisable()
    {
        EventManager.main.changeColorEvent -= Selection;
    }
}
