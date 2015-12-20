using UnityEngine;
using System.Collections;

namespace Rodger{
	public class MaskObject : MonoBehaviour {

	    UIPanel m_Panel;
	    UseMaskObject m_usemaskObj;

	    float m_width ;
	    float m_height;

		public float tradius;
		public float trotate;
		public bool thidedown;

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

        void OnGUI()
        {
            if (GUILayout.Button ("DoMask")) 
			{
				DoMask(tradius,trotate,thidedown);
			}
        }

		void DoMask(float radius,float rotate,bool hidedown)
		{

			//m_Panel.clipOffset = 

		}
	}
}
