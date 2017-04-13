using System.Collections;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace Utils
{
    internal static class HelperFunctions
    {

        #region Internal Methods

        internal static IEnumerator FadeIn(CanvasGroup canvasGroupComponent, float fadeTime, float maxAlpha)
        {

            while (canvasGroupComponent.alpha < maxAlpha)
            {
                canvasGroupComponent.alpha += Time.deltaTime / fadeTime;
                yield return null;
            }
        }

        internal static IEnumerator FadeOut(CanvasGroup canvasGroupComponent, float fadeTime)
        {

            while (canvasGroupComponent.alpha > 0)
            {
                canvasGroupComponent.alpha -= Time.deltaTime / fadeTime;
                yield return null;
            }
        }

        [CanBeNull]
        internal static GameObject GetChildUITextboxFromPanel(GameObject panel)
        {
            return (from Transform child in panel.GetComponent<Transform>()
                where child.tag.Equals("UITextbox")
                select child.gameObject).FirstOrDefault();
        }

        #endregion Internal Methods

        internal static IEnumerator Delay(float delay)
        {
            yield return new WaitForSeconds(delay);
        }
    }
}