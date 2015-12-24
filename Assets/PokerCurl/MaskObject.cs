using UnityEngine;

namespace Rodger
{
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

		public void DoMask(Vector2 pos, float theta_pos, float theta_rotate)
		{
            /*
            string str = "";
            str += "pos " + pos + "\n";
            str += "theta_pos " + theta_pos + "\n";
            str += "theta_rotate " + theta_rotate + "\n";*/
            //print(str);

            Vector2 offset = Vector2.zero;

            offset.x = pos.x + 1000 * Mathf.Cos(theta_pos * Mathf.Deg2Rad);
            offset.y = pos.y + 1000 * Mathf.Sin(theta_pos * Mathf.Deg2Rad);

            //m_Panel.clipOffset = offset;
            //print("offset " + offset);
            transform.localRotation = Quaternion.Euler(0,0,theta_rotate);
            transform.localPosition = offset;
            
            m_usemaskObj.Cooperation();
        }
        
        public void ResetMask(Vector2 pos)
        {
            m_Panel.gameObject.transform.localPosition = pos;

            transform.localRotation = Quaternion.Euler(0, 0, 0);
            m_usemaskObj.Cooperation();
        }
	}
}
