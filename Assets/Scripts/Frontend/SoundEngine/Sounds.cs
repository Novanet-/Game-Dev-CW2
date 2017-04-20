using UnityEngine;

namespace Frontend.SoundEngine
{
    internal class Sounds : MonoBehaviour
    {
        public static Sounds Instance { get; set; }

        [SerializeField] public AudioClip ExampleSoundClip;

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
