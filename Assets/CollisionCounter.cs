using UnityEngine;
using UnityEngine.UI;

public class CollisionCounter : MonoBehaviour
{
    public int collisionCount = 0; // �洢��ײ����
    public Text collisionCountText; // ����UI�ı�Ԫ��
    public string targetTag = "hand"; // ����Ŀ������ı�ǩ

    private void Start()
    {
        UpdateCollisionCountDisplay();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // �����ײ�����Ƿ�����ȷ�ı�ǩ
        if (collision.collider.CompareTag(targetTag))
        {
            // ������ض����壬���Ӽ���
            collisionCount ++;
            // ����UI����ʾ����ײ����
            UpdateCollisionCountDisplay();
        }
    }

    private void UpdateCollisionCountDisplay()
    {
        if (collisionCountText != null)
        {
            collisionCountText.text = "Jumping Jacks: " + collisionCount;
            Debug.Log("Jumping Jacks: " + collisionCount);
        }
        else
        {
            Debug.LogError("CollisionCounter: No Text component assigned.");
        }
    }
}