using UnityEngine;

namespace Ping
{
    public class GSLoading : GSTemplate
    {
        public static GSLoading Instance { get; private set; }
        protected override void Awake()
        {
            base.Awake();
            Instance = this;
        }
        protected override void init()
        {
        }
        public override void onEnableState(SwipeEffect effect)
        {
            base.onEnableState(effect);
        }
        public override void onDisableState(SwipeEffect effect)
        {
            base.onDisableState(effect);
        }
        public override void onBackKey()
        {

        }
    }
}