using System;
using UnityEngine;

namespace SignalSystem
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(AudioSource))]
    public sealed class SignalSystem : MonoBehaviour
    {
        [SerializeField, Range(0, 1)] private float _minVolume = 0f;
        [SerializeField, Range(0, 1)] private float _maxVolume = 1f;
        private float _volumeStep = 0.05f;
        private AudioSource _audioSource;
        private float _amountTimeActivate;
        private bool _thiefInside;

        private void Awake() => Init();

        private void Update()
        {
            ChangeVolumeSystem();
        }

        private void ChangeVolumeSystem()
        {
            if (_audioSource.volume >= _minVolume && _audioSource.volume <= _maxVolume)
            {
                _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _thiefInside ? _maxVolume : _minVolume,
                    Time.deltaTime * _volumeStep);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out FirstPersonController player))
                _thiefInside = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.TryGetComponent(out FirstPersonController player))
                _thiefInside = false;
        }
        
        private void Init()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.playOnAwake = false;
            _audioSource.loop = true;
            _audioSource.volume = _minVolume;
        }
    }
}