using System;
using System.Collections.Generic;
using System.Linq;
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

        #endregion Private Fields

        #region Private Properties

        private GoatMovementController[] GoatControllerArray { get; set; }
        private TrollMovementController Troll { get; set; }

        #endregion Private Properties

        #region Public Constructors

        public TrollAIController([NotNull] TrollMovementController troll, [NotNull] GoatMovementController[] goatControllerArray,
            float distanceThreshold)
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
            //            GoatMovementController closestGoat = GetClosestGoat();
            //            _closestGoatTransform = closestGoat.transform;

			var hooks = GameController.Instance.Hooks;
			var leftBoundX = hooks.LeftBound.transform.position.x;
			var rightBoundX = hooks.RightBound.transform.position.x;

			IEnumerable<GoatMovementController> goatsByDistanceAscending = SortGoatsByDistanceAscending().Where(goat => goat.transform.position.x > leftBoundX && goat.transform.position.x < rightBoundX);
			IEnumerable<GoatMovementController> goatsByMassDescending = SortGoatsByMassDescending().Where(goat => goat.transform.position.x > leftBoundX && goat.transform.position.x < rightBoundX);

			if (!goatsByDistanceAscending.Any()) return 0;

            GoatMovementController closestGoat = goatsByDistanceAscending.First();
            GoatMovementController heaviestGoat = goatsByMassDescending.First();

            GoatMovementController targetGoat = heaviestGoat;

            bool isGoatOutsideThreshold = Vector3.Distance(_trollTransform.position, targetGoat.transform.position) > _distanceThreshold;
            return isGoatOutsideThreshold ? 0 : NextMoveDirection(targetGoat);
        }

        #endregion Public Methods

        #region Private Methods

        public static float TransformDistance([NotNull] Transform troll, [NotNull] Transform goat)
        {
            if (troll == null) throw new ArgumentNullException("troll");
            if (goat == null) throw new ArgumentNullException("goat");

            Vector3 trollPosition = troll.position;
            Vector3 goatPosition = goat.position;
            return Vector3.Distance(trollPosition, goatPosition);
        }

        [NotNull]
        public GoatMovementController GetClosestGoat()
        {
            return GoatControllerArray.MinElement(goat => TransformDistance(_trollTransform, goat.gameObject.transform));
        }

        [NotNull]
        private IEnumerable<GoatMovementController> SortGoatsByDistanceAscending()
        {
            return GoatControllerArray.OrderBy(goat => TransformDistance(_trollTransform, goat.gameObject.transform));
        }

        [NotNull]
        private IEnumerable<GoatMovementController> SortGoatsByMassDescending()
        {
            return GoatControllerArray.OrderByDescending(goat => goat.gameObject.GetComponent<Rigidbody>().mass);
        }

        private float NextMoveDirection(GoatMovementController targetGoat)
        {
//            GoatMovementController closestGoat = GetClosestGoat();
//            _closestGoatTransform = closestGoat.transform;

            var targetGoatTransform = targetGoat.transform;

            return targetGoatTransform.position.x > _trollTransform.position.x ? 1 : -1;
        }

        #endregion Private Methods
    }
}