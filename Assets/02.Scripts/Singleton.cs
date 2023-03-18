using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    // destroy 확인
    private static bool _ShuttingDown = false;
    private static T _Instance;
    private static object _Lock = new object();


    public static T Instance
    {
        get 
        { 
            // 게임 종료 시 object 보다 싱글톤 Ondestroy가 먼저 실행될수 있다.
            // 해당 싱글톤을 gameObject.Ondestory() 에서 사용하지 않거나 사용한다면 null 체크
            if(_ShuttingDown)
            {
                Debug.Log("[Singleton] Instance '" + typeof(T) + "' already destroyed. Returning null");
                return null;
            }

            lock(_Lock)
            {
                if(_Instance == null )
                {
                    // 인스턴스 존재 여부 확인
                    _Instance = (T)FindObjectOfType(typeof(T));

                    // 없다면 인스턴스 생성
                    if(_Instance == null)
                    {
                        // 새로운 게임 오브젝트를 만들어서 싱글톤 attach
                        var singletonObject = new GameObject();
                        _Instance = singletonObject.AddComponent<T>();
                        singletonObject.name = typeof(T).ToString() + " (Singleton)";

                        //Make instance persistent
                        DontDestroyOnLoad(singletonObject);
                    }
                }
                return _Instance;
            }
        }
    }

    private void OnApplicationQuit()
    {
        _ShuttingDown = true;
    }

    private void OnDestroy()
    {
        _ShuttingDown = true;
    }

}


