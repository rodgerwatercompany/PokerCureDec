using UnityEngine;
using System.Collections;


namespace Rodger{
	public class UseMaskObject : MonoBehaviour {


		void Awake(){
		}

		// Use this for initialization
		void Start () {
		
		}

		public void Cooperation()
		{
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.Euler(0, 0, 0);
		}
	}
}