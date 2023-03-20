using UnityEngine;

public class RaycastOnMouseClick : MonoBehaviour
{
    public float maxDistance = 10f; // ����ĳ��Ʈ �ִ� �Ÿ�

    private Camera cam; // ���� ī�޶�

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main; // ���� ī�޶� ��������
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // ���콺 ���� ��ư�� ������ ��
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition); // ���콺 ��ġ�� �������� ���� ����
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, maxDistance)) // ����ĳ��Ʈ ����
            {
                Debug.Log("Hit: " + hitInfo.collider.gameObject.name); // �浹�� ������Ʈ�� �̸� ���
                // TODO: ����ĳ��Ʈ �浹 �� ó���� �ڵ� �ۼ�
            }
        }
    }
}