// Shake.cs
using UnityEngine;
using System.Collections;

public class Shake : MonoBehaviour
{

    private static Transform m_transform = null;

    private static Transform tranform
    {
        get
        {
            if (m_transform == null)
            {
                //m_transform = (new GameObject("AutoFade")).AddComponent<AutoFade>();
            }
            return m_transform;
        }
    }
}
