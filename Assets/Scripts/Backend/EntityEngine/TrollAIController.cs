using System;
using System.Linq;
using UnityEngine;

namespace Backend.EntityEngine
{
    internal class TrollAIController
    {
        #region Public Properties

        public TrollMovementController Troll { get; set; }

        #endregion Public Properties

        #region Private Properties

        private GoatMovementController[] GoatControllerArray { get; set; }

        #endregion Private Properties

        #region Public Constructors

        public TrollAIController(TrollMovementController troll, GoatMovementController[] goatControllerArray)
        {
            Troll = troll;
            GoatControllerArray = goatControllerArray;
        }

        #endregion Public Constructors

        #region Private Methods

        private GoatMovementController GetClosestGoat()
        {
            float minDistance = GoatControllerArray.Min(goat => TransformDistance(Troll, goat));

            return GoatControllerArray.First(goat => Math.Abs(TransformDistance(Troll, goat) - minDistance) < 0.01);
        }

        private static float TransformDistance(Component troll, Component goat)
        {
            var trollPosition = troll.gameObject.transform.position;
            var goatPosition = goat.gameObject.transform.position;
            return Vector3.Distance(trollPosition, goatPosition);
        }

        #endregion Private Methods
    }
}