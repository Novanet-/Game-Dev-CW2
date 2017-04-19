using Frontend.StoryEngine;
using UnityEngine;

namespace Frontend.StoryEngine
{
    public class EventTriggerController : MonoBehaviour
    {

        [SerializeField] private string _targetEventName;
        [SerializeField] private int _triggerAmountLimit;

        public int TriggerCount { get; set; }

        private void Start()
        {
            TriggerCount = 0;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (TriggerCount >= _triggerAmountLimit) return;

            var targetEvent = EventTriggers.Instance.EventTriggerDictionary[_targetEventName];
            targetEvent.Invoke();

            ++TriggerCount;
        }
    }
}
