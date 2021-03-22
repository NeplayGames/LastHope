using lastHope.controller;
using lastHope.movement;
using Lean.Gui;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace lastHope.core

{

    public class OrbitController : MonoBehaviour
    {
        [Header("Aiming Vars")]
        public bool isOrbiting = false;
        Transform hand;

        WeaponAttackController weapon;
        [SerializeField] WeaponHolder weaponHolder;
        [SerializeField] PlayerMoment playerMoment;
        PlayerComponents playerComponents;
        //public float distanceFromOffset = 2;

        [Header("General Options")]
        public Transform playerTarget;
        public Transform tempTarget;
        public float cameraTargetHeightAdd = 0f;
        float cameraTargetHeight = 0.7f;
        public float distanceFromTarget = 3;
        public float sideFromTarget = 0.5f;
    
        //public Vector3 offsetOrbitPoint;

        [Header("Speeds")]
        public float rotationSpeed = 5;
        public float cameraSpeed = 5;
        public float moveSpeed = 5;
        public float wallPush = .7f;
        [SerializeField]LeanJoystick leanJoystick;
        public bool touchPhase = false;

        [Header("Senss")]
        public float clampMax = 0.89f;

        public Vector2 sensitivity = new Vector2(10, 10);
        public Vector2 pitchMinMax = new Vector2(10, 60);

        [Header("While Moving")]
 
        public float distanceAdd = 1;

        [Header("Distances")]
        public float closesDistanceToPlayer = 1.5f;
       // public float evenCloserDistanceToPlayer = 1f;
        #region Privates
        Vector3 currentRotation;
        bool point = false;
        float yaw;
        float pitch;
        #endregion
        [Header("mask")]
        public LayerMask notPlayer;
        public bool pitchLock= false;
        bool thirdPerson = true;
        float turnValue = 0;
        private float baseFOV;
        private float recoil;
        [Header("Temp Variable")]

        Vector2 touchPosition;
        public float fovAdd = 5;
      
        private void Start()
        {
          //      DontDestroyOnLoad(this.gameObject);

            Vector3 Angles = transform.eulerAngles;
            yaw = Angles.x;
            pitch = Angles.y;
          //  baseFOV = Camera.main.fieldOfView;
            weaponHolder = playerTarget.GetComponent<WeaponHolder>();
          //  playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
            playerMoment = playerTarget.GetComponent<PlayerMoment>();
          //  leanJoystick = FindObjectOfType<LeanJoystick>();
            playerComponents = FindObjectOfType<PlayerComponents>();
            hand = playerComponents.hand;
        }
        bool sprint = false;
       // RaycastResult raycastResult;
        void CheckUIObjectsInPosition(Touch position)
        {
            
                PointerEventData pointer = new PointerEventData(EventSystem.current);
                pointer.position = position.position;
                List<RaycastResult> raycastResults = new List<RaycastResult>();
                EventSystem.current.RaycastAll(pointer, raycastResults);

                if (raycastResults.Count > 0)
                {
                    if (raycastResults[0].gameObject.CompareTag("Sprint"))
                    {

                        sprint = true;
                        playerMoment.Sprint();
                        return;

                    }
                if (leanJoystick.pointer == null || leanJoystick.pointer.position != position.position)
                {
                    if (raycastResults[0].gameObject.CompareTag("Moment"))
                    {
                        // print(go.gameObject.tag);
                        yaw += position.deltaPosition.x * sensitivity.x;
                        pitch -= position.deltaPosition.y * sensitivity.y;

                        return;
                    }


                    if (raycastResults[0].gameObject.CompareTag("Fire"))
                    {
                        weaponHolder.Shoot();
                        pitch -= recoil;
                        return;
                    }
                }
            

             



            }
        }
            #region Aiming
            private void ThirdPersonAiming()
        {
            
            weapon = weaponHolder.weaponActive;
          

            if (weapon!=null)
            {
                        sideFromTarget =Mathf.Lerp(sideFromTarget, -0.4f,1f);   
                      
                      
                        recoil = weapon.recoil;

                      
                       
                        //if (Input.GetMouseButton(1) && playerMoment.aimming)
                        //{
                        //    {

                        //        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, baseFOV - weapon.addFOV, 0.3f);
                        //    }
                        //}
                        //else
                        //    Camera.main.fieldOfView = baseFOV;
                     
                    }
                    else
                    {
                      
                        sideFromTarget = Mathf.Lerp(sideFromTarget, -0.1f, 0.7f);

                    }


                
            
           
           

        }
        #endregion
        void LateUpdate()
        {
            if (isOrbiting) return;
            if (sprint)
            {
                playerMoment.Walk();
                sprint = false;
            }
            if (Time.timeScale == 0) return;
            //if (temp != null)
            //{
            //    print("looking");
            //    temp.LookAt(Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 100)));

            //}
           // cameraTargetHeight = playerMoment.isCrouching ?  Mathf.Lerp(0.3f, 0.8f, 0.1f):Mathf.Lerp(0.8f, 0.3f, 0.1f);
           
            if (Input.GetKeyDown(KeyCode.V))
            {
                thirdPerson = !thirdPerson;
                distanceFromTarget = thirdPerson ? 3 :playerMoment.isSwimming?3: 1;
                closesDistanceToPlayer = thirdPerson ? 1.5f:2f;
            }
            if (playerTarget.GetComponent<Health>().IsDead())
            {
              //  print("Death");
                DeathCameraSetUp();
                
                return;
            }
            
                ThirdPersonAiming();

            Quaternion rotation;

            if (!pitchLock)
            {
               
               // print(Input.touchCount);
                if (Input.touchCount > 0)
                {
                    //if (leanJoystick.ScaledValue == Vector2.zero)
                    //{
                    //    yaw += pos.deltaPosition.x * sensitivity.x;
                    //    pitch -= pos.deltaPosition.y * sensitivity.y + recoil;
                    //}
                    //  else
                    for (int i = 0; i < Input.touchCount; i++)
                    {
                        // if (leanJoystick.ScaledValue == Vector2.zero && Input.touchCount==1) 
                       
                       // if (leanJoystick.pointer == null )
                       //     CheckUIObjectsInPosition(Input.GetTouch(i));

                       //else if (leanJoystick.pointer.position != Input.GetTouch(i).position)
                            CheckUIObjectsInPosition(Input.GetTouch(i));

                    }
                }



                pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);
                rotation = Quaternion.Euler(pitch, yaw, 0);
            }
            else
                rotation = Quaternion.Euler(22,playerTarget.eulerAngles.y,0);
                 
                  

                 
           
            
                Vector3 position = playerTarget.position - (rotation *( Vector3.forward*distanceFromTarget + Vector3.right*sideFromTarget ) + new Vector3(0, - (cameraTargetHeight+cameraTargetHeightAdd), 0));
                
                transform.rotation = rotation;
                CollisionCheck(position);
           
            float cameraX = transform.rotation.x;
                if(playerMoment.pState == PlayerMoment.PlayerState.idle && !playerMoment.aimming || playerMoment.pState == PlayerMoment.PlayerState.jumping )
            {
               
            }else
            {
                //if (Input.GetKey(KeyCode.S) && !playerMoment.aimming)
                //{
                //    turnValue = 180;
                //    //playerTarget.eulerAngles = new Vector3(cameraX, transform.eulerAngles.y + 180, transform.eulerAngles.z);
                //}
                //else
                //    if(Input.GetKey(KeyCode.W))
                //    turnValue = 0;
                playerTarget.eulerAngles = new Vector3(cameraX, transform.eulerAngles.y + turnValue, transform.eulerAngles.z);

                // transform.position = Vector3.Lerp(transform.position, (playerTarget.position + offsetOrbitPoint) - transform.forward * distanceFromTarget, cameraSpeed);
            }
            tempTarget = playerTarget;



        }

        private void DeathCameraSetUp()
        { 
            transform.LookAt(playerTarget);
            transform.position = Vector3.Lerp(transform.position,playerTarget.position+Vector3.up-Vector3.forward * 4f ,0.1f);
        }
        #region collisioncheck
        void CollisionCheck(Vector3 retPoint)
        {
            if (Physics.Linecast(playerTarget.position, retPoint, out RaycastHit hit, notPlayer))
            {
                Vector3 norm = hit.normal * wallPush;
                Vector3 p = hit.point + norm + transform.up * 0.3f;
                 if (!(Vector3.Distance( p, playerTarget.position) <= 0.2f))
                 {

                transform.position = p;
                 }
                return;
            }
          
           if(playerMoment.sliding)
            {
                transform.position = retPoint + new Vector3(0, -0.4f, 0);
            }
            else if (playerMoment.isCrouching)
            {
                transform.position = retPoint + new Vector3(0, -0.4f, 0);

            }
            else
                transform.position = retPoint;

            pitchLock = false;
        }
        #endregion
       
    
      
    }

}
