using UnityEngine;

public class CameraResolution2 : MonoBehaviour
{
    /// <summary>
    /// 해당 스크립트를 각각의 카메라에 추가
    /// 에디터 Screen Match Mode 를 Expand로 해줘야함
    /// </summary>
    private void Awake()
    {
        Screen.SetResolution((int)((1080f / 1920) * Screen.height), Screen.height, false); // false시 전체화면 x

    }
}