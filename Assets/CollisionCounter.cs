using UnityEngine;
using UnityEngine.UI;

public class CollisionCounter : MonoBehaviour
{
    public int collisionCount = 0; // 存储碰撞次数
    public Text collisionCountText; // 引用UI文本元素
    public string targetTag = "hand"; // 设置目标物体的标签

    private void Start()
    {
        UpdateCollisionCountDisplay();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 检查碰撞物体是否有正确的标签
        if (collision.collider.CompareTag(targetTag))
        {
            // 如果是特定物体，增加计数
            collisionCount ++;
            // 更新UI上显示的碰撞次数
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