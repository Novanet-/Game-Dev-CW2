﻿using UnityEngine;

namespace Frontend.SoundEngine
{
    internal class Sounds : MonoBehaviour
    {
        public static Sounds Instance { get; set; }

        [SerializeField] public AudioClip ExampleSoundClip;
        [SerializeField] public AudioClip GoatSwitchSwooshClip;
        [SerializeField] public AudioClip FollowingDrumHitClip;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else if (Instance != this)
                Destroy(gameObject);
            DontDestroyOnLoad(gameObject);
        }
    }
}
