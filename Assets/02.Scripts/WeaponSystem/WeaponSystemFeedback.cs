using UnityEngine;
using MoreMountains.Tools;
using MoreMountains.CorgiEngine;

namespace MoreMountains.Feedbacks
{
    [AddComponentMenu("")]
    [FeedbackHelp("처음 시작할 때 테크트리를 적용시켜주는 피드백")]
    [FeedbackPath("WeaponSystem/LoadWeaponTechTree")]
    public class MMF_DebugLog : MMF_Feedback
    {
        /// a static bool used to disable all feedbacks of this type at once
        public static bool FeedbackTypeAuthorized = true;
        /// use this override to specify the duration of your feedback (don't hesitate to look at other feedbacks for reference)
        public override float FeedbackDuration { get { return 0f; } }
        /// pick a color here for your feedback's inspector
#if UNITY_EDITOR
        public override Color FeedbackColor { get { return MMFeedbacksInspectorColors.DebugColor; } }
#endif

        public Weapon weaponInfo;
        public GameObject player;

        protected override void CustomInitialization(MMF_Player owner)
        {
            base.CustomInitialization(owner);
            // your init code goes here

            weaponInfo = WeaponSystemManager.Instance.GetWeapon(WeaponSystemManager.Instance.currentWeaponName);
            player = LevelManager.Instance.Players[0].gameObject;
        }

        protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1.0f)
        {
            if (!Active || !FeedbackTypeAuthorized)
            {
                return;
            }
            // your play code goes here
            MoreMountains.CorgiEngine.Weapon weapon = player.GetComponent<CharacterHandleWeapon>().CurrentWeapon;
            
            if(weapon is ProjectileWeapon)
            {
                //나중에
            }
            else if(weapon is MeleeWeapon)
            {
                MeleeWeapon melee = (MeleeWeapon)weapon;
                melee.MinDamageCaused += weaponInfo.cache.damage;
                if(melee.MaxDamageCaused != 0)
                {
                    melee.MaxDamageCaused += weaponInfo.cache.damage;
                }
                melee.DelayBeforeUse /= weaponInfo.cache.damage;
                melee.TimeBetweenUses = melee.DelayBeforeUse;
            }
        }

        protected override void CustomStopFeedback(Vector3 position, float feedbacksIntensity = 1)
        {
            if (!FeedbackTypeAuthorized)
            {
                return;
            }
            // your stop code goes here
        }
    }
}