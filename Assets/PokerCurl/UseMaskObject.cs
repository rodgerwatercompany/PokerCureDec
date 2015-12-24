using UnityEngine;
using System.Collections;


namespace Rodger{
	public class UseMaskObject : MonoBehaviour
    {
        Vector3 transferVec;
         
        void Awake()
        {
            transferVec = GameObject.Find("UI Root").transform.localScale;
        }

		public void Cooperation(Vector2 pos)
		{
            transform.position = new Vector3(pos.x * transferVec.x, pos.y * transferVec.y, 0);
            transform.rotation = Quaternion.Euler(0, 0, 0);
		}
	}
}