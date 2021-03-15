using System;

using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
// need use DoTween
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;

namespace Extensions
{
    public static class ToolsExtensions
    {
        public static float GameScreenWidth
        {
#if UNITY_EDITOR
            get { return Single.Parse(UnityStats.screenRes.Split('x')[0]); }
#else
        get { return Screen.width; }
#endif
        }

        public static float GameScreenHeight
        {
#if UNITY_EDITOR
            get { return Single.Parse(UnityStats.screenRes.Split('x')[1]); }
#else
             get { return Screen.height; }
#endif
        }

        public static float GameScreenWidthRatio
        {
#if UNITY_EDITOR
            get { return GameScreenWidth / 1920f; }
#else
        get { return GameScreenWidth / 1920f; }
#endif
        }

        public static float GameScreenHeightRatio
        {
#if UNITY_EDITOR
            get { return GameScreenHeight / 1080f; }
#else
        get { return GameScreenHeight / 1080f; }
#endif
        }

        public static bool Overlaps(this RectTransform a, RectTransform b)
        {
            var aa = a.WorldRect();
            var bb = b.WorldRect();
            return aa.Overlaps(bb);
        }

        public static Rect WorldRect(this RectTransform rectTransform)
        {
            Vector2 sizeDelta = rectTransform.rect.size;
            float rectTransformWidth = sizeDelta.x * rectTransform.lossyScale.x;
            float rectTransformHeight = sizeDelta.y * rectTransform.lossyScale.y;

            Vector3 position = rectTransform.position;
            return new Rect(position.x - rectTransformWidth / 2f, position.y - rectTransformHeight / 2f,
                rectTransformWidth, rectTransformHeight);
        }

        public static Vector3 WorldToCanvasPosition(this Canvas canvas, Vector3 worldPosition, Camera camera)
        {
            var viewportPosition = camera.WorldToViewportPoint(worldPosition);
            return canvas.ViewportToCanvasPosition(viewportPosition);
        }

        public static Vector3 ViewportToCanvasPosition(this Canvas canvas, Vector3 viewportPosition)
        {
            var centerBasedViewPortPosition = viewportPosition - new Vector3(0.5f, 0.5f, 0);
            var canvasRect = canvas.GetComponent<RectTransform>();
            var scale = canvasRect.sizeDelta;
            return Vector3.Scale(centerBasedViewPortPosition, scale);
        }

        public static void SetAlpha(this Graphic graphic, float alpha)
        {
            var c = graphic.color;
            c.a = alpha;
            graphic.color = c;
        }

// need use DoTween

        public static TweenerCore<Color, Color, ColorOptions> AlphaOut(this Graphic target, float duration,
            float toAlpha)
        {
            return ChangeAlpha(target, 1, toAlpha, duration);
        }

        public static TweenerCore<Color, Color, ColorOptions> AlphaOut(this Graphic target, float duration)
        {
            return ChangeAlpha(target, 1, 0, duration);
        }

        public static TweenerCore<Color, Color, ColorOptions> AlphaIn(this Graphic target, float duration)
        {
            return ChangeAlpha(target, 0, 1, duration);
        }

        public static TweenerCore<Color, Color, ColorOptions> AlphaIn(this Graphic target, float duration,
            float toAlpha)
        {
            return ChangeAlpha(target, 0, toAlpha, duration);
        }

        public static TweenerCore<Color, Color, ColorOptions> ChangeAlpha(this Graphic target, float startValue,
            float endValue, float duration)
        {
            Color startColor = target.color;
            startColor.a = startValue;

            Color endColor = target.color;
            endColor.a = endValue;

            target.color = startColor;
            return target.DOColor(endColor, duration);
        }

    }
}