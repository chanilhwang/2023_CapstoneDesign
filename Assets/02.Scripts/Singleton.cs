using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    // destroy Ȯ��
    private static bool _ShuttingDown = false;
    private static T _Instance;
    private static object _Lock = new object();


    public static T Instance
    {
        get 
        { 
            // ���� ���� �� object ���� �̱��� Ondestroy�� ���� ����ɼ� �ִ�.
            // �ش� �̱����� gameObject.Ondestory() ���� ������� �ʰų� ����Ѵٸ� null üũ
            if(_ShuttingDown)
            {
                Debug.Log("[Singleton] Instance '" + typeof(T) + "' already destroyed. Returning null");
                return null;
            }

            lock(_Lock)
            {
                if(_Instance == null )
                {
                    // �ν��Ͻ� ���� ���� Ȯ��
                    _Instance = (T)FindObjectOfType(typeof(T));

                    // ���ٸ� �ν��Ͻ� ����
                    if(_Instance == null)
                    {
                        // ���ο� ���� ������Ʈ�� ���� �̱��� attach
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


