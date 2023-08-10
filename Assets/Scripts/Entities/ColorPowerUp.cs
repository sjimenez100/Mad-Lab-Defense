using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ColorPowerUp : PowerUp, IColorable, ILethal
{

    [InspectorName("Color")]
    [SerializeField]
    private Color.ColorType setColor;
    public Color.ColorType color { get; set; }

    [InspectorName("Damage")]
    [SerializeField]
    private float setDamage;
    public float damage { get; set; }

    private void Awake()
    {
        damage = setDamage;
        color = setColor;
    }

    public override void Kill()
    {
        EventManager.main.OnPowerUpKill();
        base.Kill();
    }

    public override void Execute()
    {
        Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);

        KillEntitiesWithColor(enemies);

        
        ColorPowerUp[] colorPowerUps = FindObjectsByType<ColorPowerUp>(FindObjectsSortMode.None);

        KillEntitiesWithColor(colorPowerUps);
        
    }

    private void KillEntitiesWithColor(Entity[] entities)
    {
        IColorable[] colorables = entities as IColorable[];

        if (colorables is not null)
        {
            foreach (Entity entity in entities)
            {
                
                if (entity == this)
                    continue;

                if (((IColorable)entity).color == color)
                {
                    entity.Kill();
                }
                
            }
        }
    }
}

