using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraControl : MonoBehaviour {

	public GameObject player;
	public GameObject cameraVertical;

	public float horizontalRotateSpeed = 1.0f;
	public float verticalRotateSpeed = 1.0f;

	public float playerMoveSpeed = 3.0f;
	public float crouchSpeed = 2.0f;

	public float playerRadius = 1.0f;
	public float playerHeight = 2.0f;

	public float jumpVelocity = 6.0f;

	public KeyCode crouchButton = KeyCode.C;

	public float vertRotateClampMin = -60.0f;
	public float vertRotateClampMax =  60.0f;

	public Sprite forward;
	public Sprite backward;
	public Sprite forwardCrouch;
	public Sprite backwardCrouch;

	public LayerMask cameraCheckMask;

	private bool isGrounded = false;
	private float yVelocity = 0.0f;

	private bool isCrouching = false;

	private float cameraDist;
	private Vector3 cameraOffset;

	private Vector3 movementVec;
	
	private float vertRotate = 0.0f;

	// Use this for initialization
	void Start () {
		cameraOffset = transform.localPosition;
		cameraDist = cameraOffset.magnitude;
	}
	
	// Update is called once per frame
	void Update () {
		float horizontal = Input.GetAxis("Mouse X") * horizontalRotateSpeed * Time.deltaTime;
		float vertical = Input.GetAxis("Mouse Y") * verticalRotateSpeed * Time.deltaTime * -1.0f;

		player.transform.parent.Rotate(Vector3.up, horizontal);

		vertRotate += vertical;
		vertRotate = Mathf.Clamp(vertRotate, vertRotateClampMin, vertRotateClampMax);
		cameraVertical.transform.localRotation = Quaternion.AngleAxis(vertRotate, Vector3.right);

		float vertAxis = Input.GetAxis("Vertical");
		float horizAxis = Input.GetAxis("Horizontal");

		if (vertAxis < 0.0f)
		{
			player.GetComponent<SpriteRenderer>().sprite = isCrouching ? backwardCrouch : backward;
		}
		else if (vertAxis > 0.0f)
		{
			player.GetComponent<SpriteRenderer>().sprite = isCrouching ? forwardCrouch : forward;
		}
		else
		{
			bool isForward = (player.GetComponent<SpriteRenderer>().sprite == forward) 
						  || (player.GetComponent<SpriteRenderer>().sprite == forwardCrouch);
			player.GetComponent<SpriteRenderer>().sprite = 
				isCrouching ? 
				  (isForward ? forwardCrouch : backwardCrouch) 
				: (isForward ? forward : backward);
		}

		{
			bool isForward = (player.GetComponent<SpriteRenderer>().sprite == forward)
							  || (player.GetComponent<SpriteRenderer>().sprite == forwardCrouch);
			if (horizAxis > 0.0f)
			{
				player.GetComponent<SpriteRenderer>().flipX = !isForward;
			}
			else if (horizAxis < 0.0f)
			{
				player.GetComponent<SpriteRenderer>().flipX = isForward;
			}
		}

		Vector3 forwardMotion = vertAxis * player.transform.forward ;
		Vector3 sideMotion = horizAxis * player.transform.right;

		Vector3 desiredMove = forwardMotion + sideMotion;

		desiredMove = desiredMove.normalized * (isCrouching ? crouchSpeed : playerMoveSpeed);

		if (isGrounded && !isCrouching && Input.GetKeyDown(KeyCode.Space))
		{
			yVelocity = jumpVelocity;
		}

		movementVec.x = desiredMove.x;
		movementVec.z = desiredMove.z;

		if (Input.GetKeyDown(crouchButton))
		{
			isCrouching = true;
			player.transform.parent.GetComponent<CharacterController>().height = 1;
			Vector3 center = player.transform.parent.GetComponent<CharacterController>().center;
			center.y = -0.5f;
			player.transform.parent.GetComponent<CharacterController>().center = center;
		}
		else if(Input.GetKeyUp(crouchButton) || (!Input.GetKey(crouchButton) && isCrouching))
		{
			if (!Physics.Linecast(player.transform.parent.position, player.transform.parent.position + Vector3.up, 
				Physics.AllLayers & ~LayerMask.NameToLayer("Minimap"), QueryTriggerInteraction.Ignore)){
				isCrouching = false;
				player.transform.parent.GetComponent<CharacterController>().height = 2;
				Vector3 center = player.transform.parent.GetComponent<CharacterController>().center;
				center.y = 0.0f;
				player.transform.parent.GetComponent<CharacterController>().center = center;
			}
		}
	}

	void FixedUpdate()
	{
		isGrounded = player.transform.parent.GetComponent<CharacterController>().isGrounded;

		if (isGrounded)
		{
			if (yVelocity <= 0.0f)
			{
				yVelocity = 0.0f;
			}
		}
		else
		{
			yVelocity += Physics.gravity.y * Time.fixedDeltaTime;
		}

		movementVec.y = yVelocity;

		float oldY = player.transform.parent.position.y;
		player.transform.parent.GetComponent<CharacterController>().Move(movementVec * Time.fixedDeltaTime);
		float newY = player.transform.parent.position.y;

		RaycastHit info;
		Vector3 castDir = (transform.position - player.transform.parent.position).normalized;
		if (Physics.SphereCast(player.transform.parent.position, 0.1f, castDir, out info, cameraDist, cameraCheckMask, QueryTriggerInteraction.Ignore))
		{
			transform.position = player.transform.parent.position + Mathf.Max(0.1f, info.distance - 0.2f) * castDir;
		}
		else if (Physics.Raycast(player.transform.parent.position, castDir, out info, cameraDist, cameraCheckMask, QueryTriggerInteraction.Ignore))
		{
			transform.position = player.transform.parent.position + Mathf.Max(0.1f, info.distance - 0.1f) * castDir;
		}
		else
		{
			transform.localPosition = cameraOffset;
		}

		if (newY == oldY)
		{
			isGrounded = true;
			yVelocity = 0.0f;
		}
	}
}
