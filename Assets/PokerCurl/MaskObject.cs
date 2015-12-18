using UnityEngine;
using System.Collections;

public class MaskObject : MonoBehaviour {

    UIPanel m_Panel;
    UseMaskObject m_usemaskObj;

    float m_width ;
    float m_height;

    void Awake()
    {
        m_Panel = GetComponent<UIPanel>();
        if (m_Panel == null)
            Debug.LogError("Can't find UIPanel.");

        m_usemaskObj = GetComponentInChildren<UseMaskObject>();
        if(m_usemaskObj == null)
            Debug.LogError("Can't find UseMaskObject.");

    }
	// Use this for initialization
	void Start () {
	
	}

}
