using UnityEngine;


public class Enemy : SpawnEntity, IColorable, ILethal
{
    [InspectorName("Damage")]
    [SerializeField]
    private float setDamage;
    public float damage { get; set; }

    [InspectorName("Color")]
    [SerializeField]
    private Color.ColorType setColor;
    public Color.ColorType color { get; set; }


    public override void Kill()
    {
        EventManager.main.OnEnemyKill();
        base.Kill();
    }


    private void Awake()
    {
        damage = setDamage;
        color = setColor;
        
    }

}
