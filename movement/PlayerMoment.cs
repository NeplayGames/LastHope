	using lastHope.core;
using Lean.Gui;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace lastHope.movement
{
	
	public class PlayerMoment : MonoBehaviour
	{

		#region Variables
		public bool isKhukuri = false;
		AudioController audioController;
		//OrbitController orbitController;
		[SerializeField] LeanJoystick joystick;
		public bool isSwimming = false;
		Camera cam;
		[Header("Player Options")]
		[SerializeField] bool storyMode = false;
		public float playerHeight = 2f;
		public enum PlayerState { running, walking, jumping, idle, ladderClimbing, aiming,zipline }
		public PlayerState pState;
		[Header("Movement Options")]
		public float movementSpeed = 5f;
		public float increaseSpeed = 1.6f;
		float speedx = 1;
		public float ladderSpeed = 3f;
		public float changeY = 0;
		[Header("Jump Options")]
		
		bool jumpSound = false;
public float jumpForce = 3f;
		public float jumpSpeed = 0f;
		public float jumpDecrease = 2f;
		bool continuePunching = false;
		[Header("Gravity")]
		public float gravity = 2.5f;

		[Header("Physics")]
		public LayerMask discludePlayer;
		public LayerMask ladder;
		public LayerMask stair;
		[Header("References")]
		public SphereCollider sphereCol;
		public SphereCollider sphereColForSliding;
		public CapsuleCollider capsuleCollider;
		public Vector3 vel;
		AudioSource audioSource;
		[SerializeField] AudioClip run;
		[SerializeField] AudioClip jump;
		[SerializeField] AudioClip swift;
		[SerializeField] AudioClip afterShock;
		float horizontal;
		float vertical;
		

		[Header("Sliding")]

		[Header("health")]
		Health health;
		private Vector3 velocity;
		private Vector3 move;
        //<Summary>
        //Player State Check Boolean
        #region player state Check variable
        public bool isCrouching = false;
		public bool aimming = false;
		bool reloading = false;
        #endregion
        AimScheduler aimScheduler;
		private bool maxSlope = false;
		Vector3 pos;
		[SerializeField]public Transform main;

		#endregion

		#region Main Methods
		public bool mainMenu = true;

		private void Start()
		{
			//DontDestroyOnLoad(this.gameObject);
			cam = Camera.main;
			pos = transform.localPosition;
			health = GetComponent<Health>();
			aimScheduler = transform.GetComponent<AimScheduler>();
			audioSource = GetComponent<AudioSource>();
			audioController = FindObjectOfType<AudioController>();
			main = FindObjectOfType<PlayerComponents>().transform;
			animator = main.GetComponent<Animator>();
			main.SetParent(transform);
			FindObjectOfType<Loader>().gameObject.SetActive(false);
		}
		private void Update()
		{
			
			Gravity();
	
			if (health.IsDead()) return;
			SimpleMove();
			LadderCheck();
            if (jumpSound)
            {
				JumpMechanicsRun();
            }

			FinalMove();

			CollisionCheck(sphereCol);

		
			if (sliding)
			{
				main.transform.localPosition = new Vector3(0, -1.4f, 0);
				CollisionCheck(sphereColForSliding);
				t += Time.deltaTime / duration;
				moveAdd = Mathf.Lerp(1f, 1.3f, t);
				if (t >= duration)
				{
					t = 0;
					//pState = PlayerState.sliding;
					sliding = false;

				}
			}
            else
            {
				main.transform.localPosition = new Vector3(0, -1f, 0);
				moveAdd = 0f;

			}
			GroundChecking();
		}

    

       public Animator animator;

		
		#endregion
		public bool sliding = false;
		#region Movement Methods
		private void LateUpdate()
		{
			

			if (isSwimming)
            {
				if(move.magnitude == 0 && !maxSlope)
				{


					animator.SetFloat("speed", 2f);

					main.localPosition = new Vector3(0, -1, 0) ;



				}
				else
                {
			
					animator.SetFloat("speed", 2.5f);

					main.localPosition = new Vector3(0, -0.4f, 0);


				}

			}

			else if(!sliding)
				main.localPosition = new Vector3(0, -1, 0);

			if (aimScheduler.ToAim())
            {
				if(!isKhukuri && !aimming)
                {
					animator.SetTrigger("aim");
					
				}
				else if (isKhukuri && !aimming)
					animator.SetTrigger("khukuri");

				aimming = true;
				

			}
			else if (aimming)
            {
                if (isCrouching)
                {
					animator.SetTrigger("crouch");

				}
                else
                {
					animator.SetTrigger("movement");

				}
				animator.ResetTrigger("aim");

				
				aimming = false;

			}
			if (aimScheduler.ToReload())
            {
				if (!reloading)
				{
					animator.SetTrigger("Reload");

				}
				reloading = true;
				
			}
            else
            {
                if (isCrouching)
                {
					animator.SetTrigger("crouchAim");

				}
				else
				{
					animator.SetTrigger("reloadingDone");

				}
				reloading = false;
				
			}
			
			main.transform.rotation = new Quaternion(0, 0, 0, 0);

			if (Mathf.Abs(changeY) > 5 && !aimming)
            {
				main.Rotate(main.up, changeY);
			
			}
            
		}
		float moveAdd = 0;
		float t = 0;
		float duration = 1f;
			public void Sliding()
        {
			if (pState == PlayerState.zipline || continuePunching)
				return;
			if (pState != PlayerState.idle && !aimming && !isSwimming)
            {
				animator.SetTrigger("slide");
				sliding = true;
			}
        }
		private void SimpleMove()
		{
			if (pState == PlayerState.zipline || continuePunching )
				return;
		
			

          
				horizontal = joystick.ScaledValue.x;
				vertical = joystick.ScaledValue.y;

			move = Vector3.ClampMagnitude(new Vector3(horizontal, 0, vertical), 1);
			changeY = horizontal * vertical  * 45;
			
			velocity += move+Vector3.forward*moveAdd ;
		

		}
		[Header("Step Heights")]
		public float stepHeight = -0.4f;
		public float forwardStep = 0.51f;
		public float requiredMax = 0.6f;

		public void StepOverTest(Vector3 dir)
		{
			Ray ray = new Ray(transform.localPosition + transform.up * stepHeight + dir.normalized * forwardStep, -transform.up);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit, Mathf.Infinity, discludePlayer))
			{
				Debug.DrawLine(ray.origin, hit.point, Color.black);

				float ang = Vector3.Angle(Vector3.up, hit.normal);

				if (hit.distance < requiredMax && !inputJump && ang < 60)
				{

					transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(transform.localPosition.x, hit.point.y + 0f, transform.localPosition.z), .3f);

				}

			}

		}
		public void Crouch()
        {
			isCrouching = !isCrouching;

			capsuleCollider.height = isCrouching ? 1f : 1.99f;
			sphereCol.center = isCrouching ? new Vector3(0, -0.5f, 0) : Vector3.zero;
			if (isCrouching)
			{
				animator.SetTrigger("crouch");

			}
			else if (!isCrouching && aimming)
			{
				animator.SetTrigger("aim");
			}
			else
			{
				animator.SetTrigger("movement");

			}
			PlayerStateCheck();

		}
		private void FinalMove()
        {
			
			
			vel = new Vector3(velocity.x, velocity.y + ladderSpeed, velocity.z) * (movementSpeed * speedx);
            vel = transform.TransformDirection(vel);

            StepOverTest(vel);
            transform.position += vel * Time.deltaTime;
            velocity = Vector3.zero;

        }
		bool sprint = false;
		public void Sprint()
        {
			
					speedx =  increaseSpeed;
			sprint = true;

		}
		public void Walk()
		{
			
			speedx =  1f;

			sprint = false;

		}
		private void PlayerStateCheck()
        {
			if(pState == PlayerState.zipline)
            {
				if (aimming)
				{

					animator.SetFloat("aimingFloat", 0f);


				}
				else
				{
					animator.SetFloat("speed", 0f);


				}
			}
            else if (grounded  && !jumpSound && pState != PlayerState.ladderClimbing  && !isSwimming)
            {
				
				//if (!aimming && Input.GetMouseButtonDown(0)  && !sliding)
				//{
				
				//	animator.SetTrigger("punch");
				
					
					
				//}
               
				//else
				if (move.magnitude == 0 && !maxSlope)
                {

                    pState = PlayerState.idle;
                    if (isCrouching && !aimming)
                    {
						animator.SetFloat("crouchFloat", 0f);
					}

					else if (isCrouching && aimming)
					{
						animator.SetFloat("crouchFloat", 0f);

					}

					else if (aimming)
                    {

						animator.SetFloat("aimingFloat", 0f);
					}
                   else {
                        animator.SetFloat("speed", 0f);
                      

                    }
					if (reloading && !isCrouching)
					{

						animator.SetFloat("reload", 0f);

					}
					else if (reloading && isCrouching)
					{
						animator.SetFloat("reload", 1.5f);

					}

				}

                else if (!sprint  && !maxSlope)
                {


                    pState = PlayerState.walking;
                    if (Vector3.Distance(transform.position, pos) > 2.72f && !sliding)
                    {
                        pos = transform.position;
                        PlayAudio(run);

                    }
                    if (isCrouching && !aimming)
                    {
						
						animator.SetFloat("crouchFloat", 0.5f);
					}else if (isCrouching && aimming)
                    {
						animator.SetFloat("crouchFloat", 0.5f);


					}

					else if (aimming)
                    {
                        animator.SetFloat("aimingFloat", 0.5f);
                     
                    }
					else if (vertical < 0)
                    {
						animator.SetFloat("speed", 1.5f);
					}
					else
					{
						animator.SetFloat("speed", 0.5f);

					}
					if (reloading)
                    {
					
						animator.SetFloat("reload", 0.5f);
					}
					
				
                }else if (velocity.z > 0f)
				{
				//	orbitController.cameraTargetHeightAdd = 0f;
                    if (sprint)
                    {
						if (Vector3.Distance(transform.position, pos) > 3f && !sliding)
						{
							pos = transform.localPosition;
							PlayAudio(run);

						}
						pState = PlayerState.running;
						if (aimming)
						{

							animator.SetFloat("aimingFloat", 1f);

						}

						else
						{
							animator.SetFloat("speed", 1f);

						}
						if (reloading)
						{

							animator.SetFloat("reload", 1f);

						}
					}
					
				}






			}
        }

        public void PlayAudio(AudioClip audio)
		{

			if (audioController.soundPause) return;
			audioSource.clip = audio;
			audioSource.Play();


		}
		#endregion

		#region Gravity/Grounding
		//Gravity Private Variables
		public bool grounded;
		private Vector3 liftPoint = new Vector3(0, 1.2f, 0);
		private RaycastHit groundHit;
		private Vector3 groundCheckPoint = new Vector3(0, -0.87f, 0);

	
		private void Gravity()
		{
			if (grounded == false)
			{
				velocity.y -= gravity;
			}

		}

		private void GroundChecking()
		{
			if (pState == PlayerState.ladderClimbing)
			{
				return;
			}
			Ray ray = new Ray(transform.TransformPoint(liftPoint), Vector3.down);
			if (Physics.SphereCast(ray, 0.17f, out RaycastHit tempHit, 20, discludePlayer))
			{
				GroundConfirm(tempHit);
			}
			else
			{
				grounded = false;
			}

		}


		private void GroundConfirm(RaycastHit tempHit)
		{
			
			Collider[] col = new Collider[3];
			int num = Physics.OverlapSphereNonAlloc(transform.TransformPoint(groundCheckPoint), 0.55f, col, discludePlayer);

			grounded = false;

			for (int i = 0; i < num; i++)
			{

				if (col[i].transform == tempHit.transform)
				{

					if (col[i].gameObject.CompareTag("water"))
					{
						isSwimming = true;
						
					}
					else
						isSwimming = false;
					groundHit = tempHit;
					grounded = true;

					//Snapping 
					if (inputJump == false)
					{



						transform.localPosition = new Vector3(transform.localPosition.x, (groundHit.point.y + playerHeight / 2), transform.localPosition.z);



					}

					break;

				}

			}

			if (num <= 1 && tempHit.distance <= 3.1f && inputJump == false)
			{

				if (col[0] != null)
				{
					Ray ray = new Ray(transform.TransformPoint(liftPoint), Vector3.down);
					RaycastHit hit;

					if (Physics.Raycast(ray, out hit, 3.1f, discludePlayer))
					{
						if (hit.transform != col[0].transform)
						{
							grounded = false;
							return;
						}
					}

				}

			}




		}



        #endregion

        #region Collision
        
		
		private void CollisionCheck(SphereCollider sphereCol)
		{
			Collider[] overlaps = new Collider[4];
			Collider myCollider = new Collider();
			int num = 0;
			if (sphereCol != null)
			{
				num = Physics.OverlapSphereNonAlloc(transform.TransformPoint(sphereCol.center), sphereCol.radius, overlaps, discludePlayer, QueryTriggerInteraction.UseGlobal);
				myCollider = sphereCol;
			}
			for (int i = 0; i < num; i++)
			{

				Transform t = overlaps[i].transform;
				

				if (Physics.ComputePenetration(myCollider, transform.localPosition, transform.rotation, overlaps[i], t.position, t.rotation, out Vector3 dir, out float dist))
				{
					Vector3 penetrationVector = dir * dist;
					Vector3 velocityProjected = Vector3.Project(velocity, -dir);
				//	print(t.rotation);
					transform.localPosition = transform.localPosition + penetrationVector;
					vel -= velocityProjected;
					//print(t.position);
					
				}

				



			}

		}
		

		#endregion

		#region Jumping

		private float jumpHeight = 0;
		private bool inputJump = false;

		public void Jump()
		{
		
			bool canJump = !Physics.Raycast(new Ray(transform.position, Vector3.up), playerHeight, discludePlayer);

			

			if (grounded && canJump && !isSwimming)
			{
				

					pState = PlayerState.jumping;
					animator.SetTrigger("jumping");
					inputJump = true;
					transform.localPosition += Vector3.up * 0.6f * 1;
					jumpHeight += jumpForce;
					jumpSound = true;
				grounded = false;	
			}

			velocity.y += jumpHeight;


		}
		private void JumpMechanicsRun()
		{
			if (jumpSound && grounded)
			{
				pos = transform.position;
				PlayAudio(jump);
				jumpSound = false;

			}
			if (grounded && jumpHeight > 0.2f || jumpHeight <= 0.2f && grounded)
			{
				jumpHeight = 0;
				inputJump = false;
			}
			if (!grounded)
			{
				jumpHeight -= (jumpHeight * jumpDecrease * Time.deltaTime);
			}

			velocity.y += jumpHeight;
		}
		#endregion

		#region ladder
		[SerializeField] float speedOfLadder = 10f;
		bool laddering = false;
		public void LadderCheck()
		{
			Ray ray = new Ray(transform.position, transform.forward);

			if (Physics.SphereCast(ray, 0.2f, 0.7f, ladder))
			{
				grounded = true;
				laddering = true;

				pState = PlayerState.ladderClimbing;

				if (Input.GetKey(KeyCode.W))
				{
					
					ladderSpeed = speedOfLadder;
					animator.SetFloat("speed",4f);
					//animationPoint = 5f;
				}
				else if (Input.GetKey(KeyCode.S))
				{
					pState = PlayerState.idle;
					ladderSpeed = -speedOfLadder;
				}
				else
				{
					if(laddering)
                    {
						laddering = false;
						pState = PlayerState.idle;
                    }

					ladderSpeed = 0f;

				}

			}
			else
			{
			//	pState = PlayerState.idle;
				ladderSpeed = 0f;
				PlayerStateCheck();
			}




		}
		#endregion

	
		
	}	
}

