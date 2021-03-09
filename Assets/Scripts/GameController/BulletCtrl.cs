using Test.Interfaces;
using UnityEngine;

namespace Test.GameController
{
    [RequireComponent(typeof(Rigidbody))]
    public class BulletCtrl : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        private Vector3 _defaultPosition;
        public int _damage = 1;

        public delegate void BulletDestroyed();
        public static event BulletDestroyed OnBulletDestroyed;

        void OnEnable()
        {
            _defaultPosition = transform.position;
            _rigidbody = GetComponent<Rigidbody>();
            JoysticController.OnGotDirection += SetVelocity;
        }

        private void OnDestroy()
        {
            JoysticController.OnGotDirection -= SetVelocity;
        }

        private void SetVelocity(Vector2 direction)
        {
            _rigidbody.velocity = new Vector3(direction.x, 0, direction.y) * -1;
        }

        public void DestroyBullet()
        {
            _rigidbody.velocity = Vector3.zero;
            transform.position = _defaultPosition;
            transform.rotation = Quaternion.identity;
            OnBulletDestroyed();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.tag != "Field")
            {
                if (collision.transform.tag != "Enemy")
                {
                    var ctrl = collision.transform.GetComponent<IDamagableObject>();
                    ctrl.OnGetDamage(_damage);
                }
                DestroyBullet();
            }
        }
    }
}