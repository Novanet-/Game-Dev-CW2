using System;
using JetBrains.Annotations;
using UnityEngine;
using Utils;

namespace Backend.EntityEngine
{
    public class TrollAIController
    {
        #region Private Fields

        private readonly float _distanceThreshold;
        private readonly Transform _trollTransform;
        private Transform _closestGoatTransform;

        #endregion Private Fields

        #region Private Properties

        private GoatMovementController[] GoatControllerArray { get; set; }
        private TrollMovementController Troll { get; set; }

        #endregion Private Properties

        #region Public Constructors

        public TrollAIController([NotNull] TrollMovementController troll, [NotNull] GoatMovementController[] goatControllerArray, float distanceThreshold)
        {
            if (troll == null) throw new ArgumentNullException("troll");
            if (goatControllerArray == null) throw new ArgumentNullException("goatControllerArray");
            if (distanceThreshold <= 0) throw new ArgumentOutOfRangeException("distanceThreshold");

            Troll = troll;
            GoatControllerArray = goatControllerArray;
            _distanceThreshold = distanceThreshold;
            _trollTransform = Troll.gameObject.transform;
        }

        #endregion Public Constructors

        #region Public Methods

        public float NextMove()
        {
            var closestGoat = GetClosestGoat();
            _closestGoatTransform = closestGoat.transform;

            bool isClosestGoatOutsideDistanceThreshold = Vector3.Distance(_trollTransform.position, _closestGoatTransform.position) > _distanceThreshold;
            return isClosestGoatOutsideDistanceThreshold ? 0 : NextMoveDirection();
        }

        #endregion Public Methods

        #region Private Methods

        private static float TransformDistance([NotNull] Transform troll, [NotNull] Transform goat)
        {
            if (troll == null) throw new ArgumentNullException("troll");
            if (goat == null) throw new ArgumentNullException("goat");

            var trollPosition = troll.position;
            var goatPosition = goat.position;
            return Vector3.Distance(trollPosition, goatPosition);
        }

        private GoatMovementController GetClosestGoat()
        {
            //            float minDistance = GoatControllerArray.Min(goat => TransformDistance(_trollTransform, goat.gameObject.transform));
            //
            //            var closestGoat = GoatControllerArray.Where(goat => TransformDistance(_trollTransform, goat.gameObject.transform) == minDistance);
            //            return closestGoat.First();

            return GoatControllerArray.MinElement(goat => TransformDistance(_trollTransform, goat.gameObject.transform));
        }

        private float NextMoveDirection()
        {
            var closestGoat = GetClosestGoat();
            _closestGoatTransform = closestGoat.transform;

            return _closestGoatTransform.position.x > _trollTransform.position.x ? 1 : -1;
        }

        #endregion Private Methods
    }
}