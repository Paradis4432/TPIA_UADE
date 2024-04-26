using UnityEngine;

// Daniel Flanigan, 2014
// This is a combined mouse look and camera move script.
// The cam move script is by: Francis R. Griffiths-Keam

namespace _Barking_Dog.Common_Scripts {
	public class MouseLook : MonoBehaviour
	{
		Vector2 _mouseAbsolute;
		Vector2 _smoothMouse;
		[Space (20)]
		[Header ("Mouse Look Settings :")]
		public Vector2
			clampInDegrees = new Vector2 (360, 180);
	
		//public bool lockCursor;
		public CursorLockMode lockCursor;
	
		public Vector2 sensitivity = new Vector2 (2, 2);
		public Vector2 smoothing = new Vector2 (3, 3);
		public Vector2 targetDirection;
		public Vector2 targetCharacterDirection;
	
		// Assign this if there's a parent object controlling motion, such as a Character Controller.
		// Yaw rotation will affect this object instead of the camera if set.
		public GameObject characterBody;
	
		[Space (20)]
		[Header ("Camera Move Settings :")]
	
		public float acceleration = 1.0f;
		public float maxSpeed = 5;
		public float dampingSpeed = 0.2f;
	
		public KeyCode fwdKey = KeyCode.W;
		public KeyCode leftKey = KeyCode.A;
		public KeyCode backKey = KeyCode.S;
		public KeyCode rightKey = KeyCode.D;
		private float speedX, speedZ=0;

		void Start ()
		{
			// Set target direction to the camera's initial orientation.
			this.targetDirection = this.transform.localRotation.eulerAngles;
		
			// Set target direction for the character body to its inital state.
			if (this.characterBody)
				this.targetCharacterDirection = this.characterBody.transform.localRotation.eulerAngles;
		}
	
		void Update ()
		{
			// Ensure the cursor is always locked when set
			//Screen.lockCursor = lockCursor;
			Cursor.lockState = this.lockCursor;
		
			// Allow the script to clamp based on a desired target value.
			var targetOrientation = Quaternion.Euler (this.targetDirection);
			var targetCharacterOrientation = Quaternion.Euler (this.targetCharacterDirection);
		
			// Get raw mouse input for a cleaner reading on more sensitive mice.
			var mouseDelta = new Vector2 (Input.GetAxisRaw ("Mouse X"), Input.GetAxisRaw ("Mouse Y"));
		
			// Scale input against the sensitivity setting and multiply that against the smoothing value.
			mouseDelta = Vector2.Scale (mouseDelta, new Vector2 (this.sensitivity.x * this.smoothing.x, this.sensitivity.y * this.smoothing.y));
		
			// Interpolate mouse movement over time to apply smoothing delta.
			this._smoothMouse.x = Mathf.Lerp (this._smoothMouse.x, mouseDelta.x, 1f / this.smoothing.x);
			this._smoothMouse.y = Mathf.Lerp (this._smoothMouse.y, mouseDelta.y, 1f / this.smoothing.y);
		
			// Find the absolute mouse movement value from point zero.
			this._mouseAbsolute += this._smoothMouse;
		
			// Clamp and apply the local x value first, so as not to be affected by world transforms.
			if (this.clampInDegrees.x < 360)
				this._mouseAbsolute.x = Mathf.Clamp (this._mouseAbsolute.x, -this.clampInDegrees.x * 0.5f, this.clampInDegrees.x * 0.5f);
		
			var xRotation = Quaternion.AngleAxis (-this._mouseAbsolute.y, targetOrientation * Vector3.right);
			this.transform.localRotation = xRotation;
		
			// Then clamp and apply the global y value.
			if (this.clampInDegrees.y < 360)
				this._mouseAbsolute.y = Mathf.Clamp (this._mouseAbsolute.y, -this.clampInDegrees.y * 0.5f, this.clampInDegrees.y * 0.5f);
		
			this.transform.localRotation *= targetOrientation;
		
			// If there's a character body that acts as a parent to the camera
			if (this.characterBody) {
				var yRotation = Quaternion.AngleAxis (this._mouseAbsolute.x, this.characterBody.transform.up);
				this.characterBody.transform.localRotation = yRotation;
				this.characterBody.transform.localRotation *= targetCharacterOrientation;
			} else {
				var yRotation = Quaternion.AngleAxis (this._mouseAbsolute.x, this.transform.InverseTransformDirection (Vector3.up));
				this.transform.localRotation *= yRotation;
			}
		}

		void FixedUpdate(){

			if (Input.GetKey (this.rightKey)) {
				this.speedX += this.acceleration * Time.deltaTime;
			}
			else if (Input.GetKey (this.leftKey)) {
				this.speedX -= this.acceleration * Time.deltaTime;
			}
			if (Input.GetKey (this.backKey)) {
				this.speedZ -= this.acceleration * Time.deltaTime;
			} else if (Input.GetKey (this.fwdKey)) {
				this.speedZ += this.acceleration * Time.deltaTime;
			}

			this.speedX = Mathf.Lerp( this.speedX,0,this.dampingSpeed * Time.deltaTime);
			this.speedZ = Mathf.Lerp( this.speedZ,0,this.dampingSpeed * Time.deltaTime);

			this.speedX = Mathf.Clamp( this.speedX,-this.maxSpeed*Time.deltaTime, this.maxSpeed*Time.deltaTime);
			this.speedZ = Mathf.Clamp( this.speedZ,-this.maxSpeed*Time.deltaTime, this.maxSpeed*Time.deltaTime);

			this.transform.position = this.transform.TransformPoint( new Vector3( this.speedX,0,this.speedZ) );
		}

	}
}
