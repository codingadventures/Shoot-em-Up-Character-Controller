using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public GameObject ObjectToFollow;

	public float Height = 10.0f;
	public float Distance = 5.0f;
	public float RotateSpeed = 10.0f;

	private float _angleY;

	// Use this for initialization
	void Start () {
		if (ObjectToFollow != null) {
			UpdateCamera ();
		}
	}

	// Update is called once per frame
	void LateUpdate () {

		if (ObjectToFollow != null) {
			float rotationDelta = 0f;
			_angleY += rotationDelta * RotateSpeed * Time.smoothDeltaTime;

			UpdateCamera ();
		}
	}

	private void UpdateCamera() {
		Vector3 positionOffset = Quaternion.AngleAxis(_angleY, Vector3.up) * new Vector3(0,0,-Distance) + Vector3.up * Height;
		Vector3 targetPosition = ObjectToFollow.transform.position + positionOffset;
		transform.position = targetPosition;

		Quaternion targetRotation = Quaternion.LookRotation (ObjectToFollow.transform.position - targetPosition);
		transform.rotation = targetRotation;
	}
}