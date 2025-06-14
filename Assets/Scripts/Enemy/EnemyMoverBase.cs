using System;
using UnityEngine;

namespace Enemy
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
    public abstract class EnemyMoverBase : MonoBehaviour, IMover
    {
        [SerializeField] protected float _baseSpeed;
        [SerializeField] private bool _moveOnAwake;
        protected Rigidbody _rigidbody;
        protected CapsuleCollider _capsuleCollider;
        protected Vector3 _defaultPosition;
        protected Vector3 _moveDirection;
        public bool CanMove { get; private set; }

        protected virtual void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _capsuleCollider = GetComponent<CapsuleCollider>();
            _defaultPosition = transform.position;
            _moveDirection = Vector3.zero;
            if(_moveOnAwake) HandleStateMoving(true);
        }
        
        private void Update()
        {
            if (CanMove)
                _rigidbody.velocity = transform.forward * _baseSpeed;
        }
        
        public void HandleStateMoving(bool value)
        {
            _moveDirection = value ? Vector3.forward : Vector3.zero;
            CanMove = value;
            _rigidbody.velocity = _moveDirection * _baseSpeed;
        }

        public void ResetPos()
        {
            transform.position = _defaultPosition;
            CanMove = false;
        }
        
        
    }
}