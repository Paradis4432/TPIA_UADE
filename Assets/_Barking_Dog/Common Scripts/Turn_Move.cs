using UnityEngine;

namespace _Barking_Dog.Common_Scripts {
	public class Turn_Move : MonoBehaviour {
		public int TurnX;
		public int TurnY;
		public int TurnZ;

		public int MoveX;
		public int MoveY;
		public int MoveZ;

		public bool World;

		// Use this for initialization
		void Start () {
		
		}
	
		// Update is called once per frame
		void Update () {
			if (this.World == true) {
				this.transform.Rotate(this.TurnX * Time.deltaTime,this.TurnY * Time.deltaTime,this.TurnZ * Time.deltaTime, Space.World);
				this.transform.Translate(this.MoveX * Time.deltaTime, this.MoveY * Time.deltaTime, this.MoveZ * Time.deltaTime, Space.World);
			}else{
				this.transform.Rotate(this.TurnX * Time.deltaTime,this.TurnY * Time.deltaTime,this.TurnZ * Time.deltaTime, Space.Self);
				this.transform.Translate(this.MoveX * Time.deltaTime, this.MoveY * Time.deltaTime, this.MoveZ * Time.deltaTime, Space.Self);
			}
		}
	}
}
