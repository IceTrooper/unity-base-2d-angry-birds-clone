using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using TMPro;

namespace DG.Tweening
{
    public static class TMPTextExtensions
    {
        public static TweenerCore<int, int, NoOptions> DOCounter(
            this TMP_Text target, int fromValue, int endValue, float duration
            )
        {
            int v = fromValue;
            TweenerCore<int, int, NoOptions> t = DOTween.To(() => v, x =>
            {
                v = x;
                target.text = v.ToString();
            }, endValue, duration);
            t.SetTarget(target);
            return t;
        }
    }
}
