using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class initizlizatoin : MonoBehaviour
{

    public Image image;
    public float showDuration = 5f;
    public float fadeDuration = 1f;

    private void Start()
    {
        // 开始协程
        StartCoroutine(ShowAndFadeRoutine());
    }

    private IEnumerator ShowAndFadeRoutine()
    {
        // 显示图像
        image.gameObject.SetActive(true);

        // 等待展示时间
        yield return new WaitForSeconds(showDuration);

        // 开始逐渐消失
        float timer = 0f;
        Color imageColor = image.color;
        float startAlpha = imageColor.a;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float targetAlpha = Mathf.Lerp(startAlpha, 0f, timer / fadeDuration);
            imageColor.a = targetAlpha;
            image.color = imageColor;
            yield return null;
        }

        // 完成逐渐消失后禁用图像
        image.gameObject.SetActive(false);
    }
}