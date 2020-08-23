using UnityEngine;

namespace Shared
{
    [System.Serializable]
    public class Sound
    {
        public string name;

        public AudioClip clip;

        [HideInInspector]
        public AudioSource source;

        [Range(0.0f, 1.0f)]
        public float volume = 0.5f;

        public bool loop;
    }
}
