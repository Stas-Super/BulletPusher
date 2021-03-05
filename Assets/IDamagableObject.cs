public interface IDamagableObject 
{
    int health { get; set; }

    void OnGetDamage(int damage);
    void OnDestroyed();
    event Destroyed OnDestroy;
}

public delegate void Destroyed();
