using System;
using System.Collections.Generic;
using Backend.StoryEngine;
using Backend.StoryEngine.Events;
using com.kleberswf.lib.core;
using UnityEngine;
using Zios.Event;

namespace Frontend.StoryEngine
{
    internal class EventTriggers : Singleton<EventTriggers>
    {
        private EventController.StoryEvents _storyEvents;

        [SerializeField] private GameObject _storyControllerObject;

        public Dictionary<string, Action> EventTriggerDictionary { get; set; }

        private void Update()
        {
            if (EventTriggerDictionary == null) InitializeDictionary();
        }
        private void InitializeDictionary()
        {
            var storyController = _storyControllerObject.GetComponent<StoryController>();
            var storyControllerEvents = storyController.Events;
            _storyEvents = storyControllerEvents.Story;
            EventTriggerDictionary = new Dictionary<string, Action>()
            {
                {"Intro", _storyEvents.Intro},
                {"Example2", _storyEvents.Example2},
                {"Example3", _storyEvents.Example3},
                {"Example4", _storyEvents.Example4}
            };
        }
    }
}