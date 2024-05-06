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
        // ��ʼЭ��
        StartCoroutine(ShowAndFadeRoutine());
    }

    private IEnumerator ShowAndFadeRoutine()
    {
        // ��ʾͼ��
        image.gameObject.SetActive(true);

        // �ȴ�չʾʱ��
        yield return new WaitForSeconds(showDuration);

        // ��ʼ����ʧ
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

        // �������ʧ�����ͼ��
        image.gameObject.SetActive(false);
    }
}