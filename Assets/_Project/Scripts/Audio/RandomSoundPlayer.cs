using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AE
{
    public class RandomSoundPlayer : MonoBehaviour
    {
        [Header("Audio Clips")]
        [SerializeField] private List<AudioClip> audioClips;

        [Header("Timing (seconds)")]
        [SerializeField] private float minDelay = 2f;
        [SerializeField] private float maxDelay = 5f;

        [Header("Audio Spawn Radius")]
        [SerializeField] private float radius = 5f;

        [Header("Audio Source Settings")]
        [SerializeField] private float spatialBlend = 1f; 
        [SerializeField] private float volume = 1f;

        private void Start()
        {
            StartCoroutine(PlayRandom3DSounds());
        }

        private IEnumerator PlayRandom3DSounds()
        {
            while (true)
            {
                float delay = Random.Range(minDelay, maxDelay);
                yield return new WaitForSeconds(delay);

                if (audioClips.Count == 0) continue;

                AudioClip clip = audioClips[Random.Range(0, audioClips.Count)];
                Vector3 randomOffset = Random.onUnitSphere * radius;
                randomOffset.y = Mathf.Abs(randomOffset.y);

                Vector3 spawnPosition = transform.position + randomOffset;

                PlayClipAtPoint3D(clip, spawnPosition);
            }
        }

        private void PlayClipAtPoint3D(AudioClip clip, Vector3 position)
        {
            GameObject tempGO = new GameObject("TempAudio");
            tempGO.transform.position = position;

            AudioSource aSource = tempGO.AddComponent<AudioSource>();
            aSource.clip = clip;
            aSource.volume = volume;
            aSource.spatialBlend = spatialBlend;
            aSource.minDistance = 1f;
            aSource.maxDistance = 10f;
            aSource.Play();

            Destroy(tempGO, clip.length + 0.5f);
        }
    }
}
