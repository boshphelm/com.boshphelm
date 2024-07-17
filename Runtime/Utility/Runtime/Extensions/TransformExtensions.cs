using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace boshphelm.Utility
{
    public static class TransformExtensions
    {
        public static void ResetTransform(this Transform transform)
        {
            transform.position = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = new Vector3(1, 1, 1);
        }

        public static IEnumerator MoveToTarget(this Transform transform, Transform target, Vector3 offset, float speed = 2, Action onComplete = null)
        {
            while (Vector3.Distance(transform.position, target.position) > 1)
            {
                if (transform == null) break;
                transform.position = Vector3.MoveTowards(transform.position, target.position + offset, speed * Time.deltaTime);
                speed += Time.deltaTime;
                yield return null;
            }

            if (onComplete != null) onComplete.Invoke();
        }

        public static IEnumerator DistanceMoveToTarget(this Transform transform, Transform target, Vector3 offset, float speed = 2, Action onComplete = null)
        {
            Vector3 startPos = transform.position;
            float timer = 0;
            while (timer < 1f)
            {
                if (transform == null) break;
                timer += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPos, target.position + offset, timer);
                yield return null;
            }

            if (onComplete != null) onComplete.Invoke();
        }

        public static IEnumerator ScaleTo(this Transform transform, Vector3 targetScale, float speed = 2, Action onComplete = null)
        {
            Vector3 startScale = transform.localScale;
            float timer = 0;
            while (timer < 1f)
            {
                if (transform == null) break;
                timer += Time.deltaTime * speed;
                transform.localScale = Vector3.Lerp(startScale, targetScale, timer);
                yield return null;
            }

            if (onComplete != null) onComplete.Invoke();
        }
     
        public static void DOLocalMoveToTarget(this Transform transform, Transform target, float duration, float delay = 0, Ease ease = Ease.Linear, Action onComplete = null)
        {
            transform.DOLocalMove(Vector3.zero, duration)
            .SetEase(ease)
            .SetDelay(delay)
            .OnComplete(() => onComplete?.Invoke());
        }
        public static void DOMoveToTarget(this Transform transform, Transform target, float duration, float delay = 0, Ease ease = Ease.Linear, Action onComplete = null)
        {
            transform.DOMove(Vector3.zero, duration)
            .SetEase(ease)
            .SetDelay(delay)
            .OnComplete(() => onComplete?.Invoke());
        }

        public static void DOTweenScaleTo(this Transform transform, Vector3 targetScale, float duration, Ease ease = Ease.Linear, LoopType loopType = LoopType.Incremental, int loopCount = 0, bool fromZero = false, float delay = 0, Action onComplete = null)
        {
            transform.DOScale(targetScale, duration)
            .SetEase(ease)
            .SetDelay(delay)
            .SetLoops(loopCount, loopType)
            .From(fromZero ? 0 : transform.localScale.x)
            .OnComplete(() => onComplete?.Invoke());
        }
    }
}