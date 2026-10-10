using System;
using System.Collections;
using UnityEngine;
using Cinemachine;
using Unity.Profiling;

namespace Scripts.CameraExtras
{
    public class CinemachineEffects : MonoBehaviour
    {
        public static CinemachineEffects instance { get; private set; }

        [SerializeField] private CinemachineImpulseListener impulseListener;
        [SerializeField] private float globalShakeForce = 1f;

        private CinemachineImpulseDefinition impulseDefinition;

        private CinemachineVirtualCamera cinemachine;

        public float startSize;

        private Coroutine zoomCoroutine;

        private void Awake()
        {
            cinemachine = GetComponent<CinemachineVirtualCamera>();

            startSize = cinemachine.m_Lens.OrthographicSize;

            instance = this;
        }


        public void CameraShake(CinemachineImpulseSource impulseSource)
        {
            impulseSource.GenerateImpulseWithForce(globalShakeForce);
        }
        private bool shaking;

        public void ScreenShakeFromProfile(ScreenShakeProfile profile, CinemachineImpulseSource impulseSource)
        {
            if (shaking) return;
            shaking = true;
            impulseSource.GenerateImpulseWithForce(profile.impactForce);
            Invoke(nameof(ResetShake), profile.listenerDuration);
        }

        private void ResetShake()
        {
            shaking = false;
        }


        private void SetupScreenShakeSettings(ScreenShakeProfile profile, CinemachineImpulseSource impulseSource)
        {
            impulseDefinition = impulseSource.m_ImpulseDefinition;

            impulseDefinition.m_ImpulseDuration = profile.impactTime;

            impulseSource.m_DefaultVelocity = profile.defautlVelocity;
            impulseDefinition.m_CustomImpulseShape = profile.impulseCurve;

            impulseListener.m_ReactionSettings.m_AmplitudeGain = profile.listenerAmplitude;
            impulseListener.m_ReactionSettings.m_FrequencyGain = profile.listenerFrequency;
            impulseListener.m_ReactionSettings.m_Duration = profile.listenerDuration;
        }

        public void Zoom(float targetSize, float duration, Action onComplete = null)
        {
            if (zoomCoroutine != null)
            {
                StopCoroutine(zoomCoroutine);
            }
            zoomCoroutine = StartCoroutine(DoZoom(targetSize, duration, onComplete));
        }

        private IEnumerator DoZoom(float targetSize, float duration, Action onComplete)
        {
            float currentSize = cinemachine.m_Lens.OrthographicSize;
            print("current size: " + currentSize);

            float t = 0f;
            while (t < duration)
            {
                t += Time.deltaTime;
                cinemachine.m_Lens.OrthographicSize = Mathf.Lerp(currentSize, targetSize, t / duration);
                yield return null;
            }

            zoomCoroutine = null;
            onComplete?.Invoke();
        }
    }
}