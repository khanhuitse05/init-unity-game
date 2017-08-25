using UnityEngine;

namespace Ping
{
    public class GSHome : GSTemplate
    {
        public static GSHome Instance { get; private set; }
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
        public void SwitchStateActive()
        {
            GameStatesManager.SwitchState(GSSetting.Instance, SwipeEffect.Active);
        }
        public void SwitchStateFade()
        {
            GameStatesManager.SwitchState(GSSetting.Instance, SwipeEffect.Fade);
        }
        public void SwitchStateZome()
        {
            GameStatesManager.SwitchState(GSSetting.Instance, SwipeEffect.Zome);
        }
        public void SwitchStateSlide()
        {
            GameStatesManager.SwitchState(GSSetting.Instance, SwipeEffect.Slide);
        }
    }
}