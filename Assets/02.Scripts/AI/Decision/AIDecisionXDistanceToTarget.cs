using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoreMountains.CorgiEngine
{
    /// <summary>
    /// This Decision will return true if the current Brain's Target is within the specified range, false otherwise.
    /// </summary>
    [AddComponentMenu("Corgi Engine/Character/AI/Decisions/AI Decision X Distance to Target")]
    public class AIDecisionXDistanceToTarget : AIDecision
    {
        /// The possible comparison modes
        public enum ComparisonModes { StrictlyLowerThan, LowerThan, Equals, GreatherThan, StrictlyGreaterThan }
        /// the method to use to compare the distance (StrictlyLowerThan, LowerThan, Equals, GreatherThan or StrictlyGreaterThan)
        [Tooltip("the method to use to compare the distance (StrictlyLowerThan, LowerThan, Equals, GreatherThan or StrictlyGreaterThan)")]
        public ComparisonModes ComparisonMode = ComparisonModes.GreatherThan;
        /// the distance to compare with
        [Tooltip("the X distance to compare with")]
        public float Distance;

        /// <summary>
        /// On Decide we check our distance to the Target
        /// </summary>
        /// <returns></returns>
        public override bool Decide()
        {
            return EvaluateDistance();
        }

        /// <summary>
        /// Returns true if the distance conditions are met
        /// </summary>
        /// <returns></returns>
        protected virtual bool EvaluateDistance()
        {
            if (_brain.Target == null)
            {
                return false;
            }

            float distance = Mathf.Abs(this.transform.position.x - _brain.Target.position.x);

            if (ComparisonMode == ComparisonModes.StrictlyLowerThan)
            {
                return (distance < Distance);
            }
            if (ComparisonMode == ComparisonModes.LowerThan)
            {
                return (distance <= Distance);
            }
            if (ComparisonMode == ComparisonModes.Equals)
            {
                return (distance == Distance);
            }
            if (ComparisonMode == ComparisonModes.GreatherThan)
            {
                return (distance >= Distance);
            }
            if (ComparisonMode == ComparisonModes.StrictlyGreaterThan)
            {
                return (distance > Distance);
            }
            return false;
        }
    }
}