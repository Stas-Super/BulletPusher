using Test.Interfaces;
using UnityEngine;

namespace Test.GameController
{
    public class BlockObject : MonoBehaviour, IDamagableObject
    {
        public int _health;
        public int health { get => _health; set => _health = value; }

        public event Destroyed OnDestroy;

        public void OnDestroyed()
        {
            gameObject.SetActive(false);
            OnDestroy();
        }

        public void OnGetDamage(int damage)
        {
            health -= damage;
            if (health <= 0)
            {
                OnDestroyed();
            }
        }
    }
}