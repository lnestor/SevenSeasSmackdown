using System;
using UnityEngine;

namespace Shared
{
    public class Audio : Singleton<Audio>
    {
        [SerializeField]
        private Sound[] sounds = default;

        public float Volume { get; set; }

        protected override void Awake()
        {
            base.Awake();

            this.Volume = 1.0f;

            foreach(Sound s in this.sounds)
            {
                GameObject go = new GameObject(s.name);
                go.transform.parent = this.transform;

                s.source = go.AddComponent<AudioSource>();
                s.source.clip = s.clip;
                s.source.loop = s.loop;
            }

            this.Play("Menu Music");
        }

        public void Play(string name)
        {
            Sound sound = Array.Find(this.sounds, s => s.name == name);

            if(sound == null)
            {
                Debug.Log("Sound " + name + "not found");
                return;
            }

            sound.source.volume = sound.volume * this.Volume;
            sound.source.Play();
        }
    }
}
