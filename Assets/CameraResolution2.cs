using UnityEngine;

public class CameraResolution2 : MonoBehaviour
{
    /// <summary>
    /// �ش� ��ũ��Ʈ�� ������ ī�޶� �߰�
    /// ������ Screen Match Mode �� Expand�� �������
    /// </summary>
    private void Awake()
    {
        Screen.SetResolution((int)((1080f / 1920) * Screen.height), Screen.height, false); // false�� ��üȭ�� x

    }
}