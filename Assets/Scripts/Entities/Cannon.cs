using System.Collections;
using UnityEngine;

public class Cannon : Entity, IDamageable
{
    
    [SerializeField]
    private Transform turnable;

    [SerializeField]
    private Transform firingTransform;

    [SerializeField]
    private Sprite[] directionalSprites;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private GameObject[] projectiles = new GameObject[Color.numColors];

    private Color.ColorType currentColor;

    [SerializeField]
    private float rotationSpeed;

    [SerializeField]
    private float initialHealth;

    public float Health { get; set; }

    private bool killed;

    void Awake()
    {
        Health = initialHealth;
    }

    
    private void OnEnable()
    {
        EventManager.main.singleInputMoveEvent += RotateCannon;
        EventManager.main.singleInputOptionEvent += ChangeProjectiles;
        EventManager.main.singleInputFireEvent += Fire;
        EventManager.main.gameOverEvent += FreezeCannon;
        EventManager.main.gamePauseEvent += FreezeCannon;
        EventManager.main.gamePlayEvent += UnFreezeCannon;
       
    }

    private void Start()
    {
        ChangeProjectiles((int)Color.ColorType.Red);
    }

    public void FreezeCannon()
    {
        EventManager.main.singleInputMoveEvent -= RotateCannon;
        EventManager.main.singleInputOptionEvent -= ChangeProjectiles;
        EventManager.main.singleInputFireEvent -= Fire;
    }

    public void UnFreezeCannon()
    {
        EventManager.main.singleInputMoveEvent += RotateCannon;
        EventManager.main.singleInputOptionEvent += ChangeProjectiles;
        EventManager.main.singleInputFireEvent += Fire;
    }


    public void TakeDamage(float damage)
    {

        if (killed)
            return;

        AudioManager.main.PlaySound("PlayerDamaged");
        EventManager.main.OnPlayerDamaged(damage);

        float newHealth = Health - damage;

        if (newHealth > 0f)
            Health = newHealth;
        else
        {
            Health = 0f;
            Kill();
        }

    }

    public override void Kill()
    {
        killed = true;

        EventManager.main.OnPlayerKill();

        AudioManager.main.PlaySound("PlayerDeath");

        EventManager.main.OnGameOver();
        
    }

    private void RotateCannon(Direction.Directions direction)
    {
        turnable.rotation = Quaternion.LookRotation(transform.forward, Direction.ToVector(direction));
        //transform.rotation = Quaternion.FromToRotation(transform.up, Direction.ToVector(direction));

        Sprite sprite = directionalSprites[(int)direction];
        spriteRenderer.sprite = sprite;
    }

    private void ChangeProjectiles(int id)
    {
        // check if id is not valid
        if (id > Color.numColors - 1)
            return;

        EventManager.main.OnChangeColor(id);

        AudioManager.main.PlaySound("ChangeAmmo");
        //Debug.Log($"{(Color.ColorType)id} Selected");

        currentColor = (Color.ColorType)id;

    }

    private void Fire()
    {
        
        GameObject projectile = Instantiate(projectiles[(int)currentColor],
            firingTransform.position, firingTransform.rotation, transform);

        projectile.GetComponent<Projectile>().Push(firingTransform.right);

        AudioManager.main.PlaySound("CannonFire");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Entity entity))
        {

            ILethal lethal = entity as ILethal;

            if (lethal is not null)
                TakeDamage(lethal.damage);

            Destroy(entity.gameObject);

        }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawWireSphere(damageSpherePosition, damageRadius);
    //}

    private void OnDisable()
    {
        EventManager.main.singleInputMoveEvent -= RotateCannon;
        EventManager.main.singleInputOptionEvent -=  ChangeProjectiles;
        EventManager.main.singleInputFireEvent -= Fire;
    }

}



//private IEnumerator CheckDamageSphere(float waitTime)
//{
//    while (true)
//    {

//        yield return StartCoroutine(CheckDamageSphereSubtask());

//        yield return new WaitForSeconds(waitTime);
//    }
//}

//private IEnumerator CheckDamageSphereSubtask()
//{

//    Collider[] colliders = Physics.OverlapSphere(damageSpherePosition,
//            damageRadius);

//    for (int i = 0; i < colliders.Length; i++)
//    {
//        Collider collider = colliders[i];

//        if (collider.TryGetComponent(out IKillable killable))
//        {
//            ILethal lethal = killable as ILethal;

//            if (lethal is not null)
//            {
//                TakeDamage(lethal.Damage);
//                Debug.Log(lethal.Damage);
//            }

//            killable.Kill();

//        }
//    }

//    yield break;
//}
