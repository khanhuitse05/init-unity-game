using UnityEngine;

namespace Ping
{
    public class GSGamePlay : GSTemplate
    {
        public static GSGamePlay Instance { get; private set; }
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

        //
        public void SwitchStateActive()
        {
            GameStatesManager.SwitchState(GSHome.Instance, SwipeEffect.Active);
        }
        public void SwitchStateFade()
        {
            GameStatesManager.SwitchState(GSHome.Instance, SwipeEffect.Fade);
        }
        public void SwitchStateZome()
        {
            GameStatesManager.SwitchState(GSHome.Instance, SwipeEffect.Zome);
        }
        public void SwitchStateSlide()
        {
            GameStatesManager.SwitchState(GSHome.Instance, SwipeEffect.Slide);
        }
    }
}