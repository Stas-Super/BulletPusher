using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyAICtrl : MonoBehaviour
{
    public Rigidbody _rigidbody;
    public Transform _target;
    public float _speed;
    public Vector3 _basePosition;
    public bool _move;

    public void Awake()
    {
        _basePosition = transform.position;
        BlocksController.OnStartNewSession += StartNewSession;
        BulletCtrl.OnBulletDestroyed += ResetPosition;
        JoysticController.OnGotDirection += StartMove;
        if (_rigidbody == null) _rigidbody = GetComponent<Rigidbody>();
    }

    private void StartMove(Vector2 direction)
    {
        _move = true;
    }

    private void ResetPosition()
    {
        _move = false;
        transform.position = _basePosition;
        transform.rotation = Quaternion.identity;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
    }

    private void StartNewSession()
    {
        _speed += 0.5f;
        transform.position = _basePosition;
        transform.rotation = Quaternion.identity;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
    }

    public void FixedUpdate()
    {
        if(_target != null && _move)
        {
            transform.LookAt(_target);
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
            _rigidbody.velocity = transform.forward * _speed;
        }
    }
}
