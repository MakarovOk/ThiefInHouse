using UnityEngine;

namespace Enemy
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
    public abstract class EnemyMoverBase : MonoBehaviour
    {
        [SerializeField] protected float _baseSpeed;
        [SerializeField] private bool _moveOnAwake;
        protected Rigidbody _rigidbody;
        protected Vector3 _moveDirection;
        public bool CanMove { get; private set; }

        protected virtual void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            GetComponent<CapsuleCollider>();
            _moveDirection = Vector3.zero;
            if(_moveOnAwake) HandleStateMoving(true);
        }
        
        private void Update()
        {
            if (CanMove)
                _rigidbody.velocity = transform.forward * _baseSpeed;
        }
        
        private void HandleStateMoving(bool value)
        {
            _moveDirection = value ? Vector3.forward : Vector3.zero;
            CanMove = value;
            _rigidbody.velocity = _moveDirection * _baseSpeed;
        }
    }
}