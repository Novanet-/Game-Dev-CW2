using UnityEngine;

namespace frontend.SoundEngine
{
    internal class Music : MonoBehaviour
    {
        public static Music Instance { get; set; }

        [SerializeField] public AudioClip ExampleMusicClip;

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
