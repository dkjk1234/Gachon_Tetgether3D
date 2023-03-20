using UnityEngine;

public class ObjectSticker : MonoBehaviour
{
    public string objectTag = "Sticky"; // ������ ������Ʈ �±�
    public float stickingForce = 100f; // ������ �� ������ ��
    public float stickingDistance = 0.2f; // ���� �Ÿ�
    public bool isStuck = false; // ���� ����

    private Transform stickyObject; // ������ ������Ʈ�� ��ġ

    // Update is called once per frame
    void Update()
    {
     
        if (!isStuck)
        {
            // ����ĳ��Ʈ ����
            RaycastHit hitInfo;
            if (Physics.Raycast(transform.position, transform.forward, out hitInfo, Mathf.Infinity))
            {
                if (hitInfo.collider.tag == objectTag)
                {
                    // ������ ������Ʈ�� ��ġ ����
                    stickyObject = hitInfo.collider.transform;
                    isStuck = true;

                    // ������ �� ������ �� ����
                    Rigidbody rb = stickyObject.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.AddForce(transform.forward * stickingForce, ForceMode.Force);
                    }
                }
            }
        }
        else
        {
            // ������ ���¿����� ������ ������Ʈ�� ���� �̵���Ŵ
            transform.position = stickyObject.position;
        }
    }
}
