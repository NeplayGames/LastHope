
using lastHope.movement;
using UnityEngine;
using lastHope.combat;
using System.Collections.Generic;
namespace lastHope.core {
    public class CommuncateWithPC : MonoBehaviour, ChangeAction
    {


        Health targeEnemy;
        GameObject player;
        Movement movement;
        Message_UI message_UI;
        Vector3 pos;
        Animator animator;
        OrbitController cam;
        public List<string> message = new List<string>();       
        HealthBarScript health;
        
        private void Start()
        {
            cam = Camera.main.transform.GetComponent<OrbitController>();
            movement = GetComponent<Movement>();
            animator = transform.GetChild(0).GetComponent<Animator>();
            message_UI = FindObjectOfType<Message_UI>();
           // missionHelper = FindObjectOfType<GameData>().GetComponent<GameData>();
          //  mission = FindObjectOfType<MissionHelper>().GetComponent<MissionHelper>();
            health = FindObjectOfType<HealthBarScript>();
        }  
        public void CancelAction()
        {
            message_UI.HideUI();
            targeEnemy = null;
        }
       
        //public bool StartConversation(GameObject target)
        //{
        //    missionHelper.HideAreaMarker();
        //    mission.placeNext = true;
        //    transform.rotation = new Quaternion(0, transform.rotation.y, transform.rotation.z, transform.rotation.w);
        //    player = target;
        //    pos = target.transform.position;
        //    targeEnemy = target.GetComponent<Health>();
        //    player.transform.LookAt(transform.position+transform.up);
        //    transform.LookAt(player.transform.position - transform.up);
        //    GetComponent<Scheduler>().StartAction(this);
        //    return conversationDone;
        //}
        bool IsInRange()
        {
            return Vector3.Distance(pos, transform.position) < 4f;
        }
        int i = 0;
        public void MessageUpdate()
        {
            player.GetComponent<WeaponHolder>().HideInfo();
            if(health!=null)
            health.HideBar();
            cam.pitchLock = true;
            player.transform.Find("main").GetComponent<Animator>().SetFloat("speed", 0f);
            player.GetComponent<PlayerMoment>().enabled = false;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                i++;

            }
            //if (i == message.Count)
            //{
            //    mission.placeNext = false;
            //    missionHelper.ShowAreaMarker(objectiveDone);
            //    cam.playerTarget = player.transform;
            //    cam.cameraTargetHeightAdd = 0f;
            //    conversationDone = true;
            //    cam.pitchLock = false;
            //    CancelAction();
            //    player.GetComponent<PlayerMoment>().enabled = true;


            //    player.GetComponent<WeaponHolder>().ShowInfo();
            //    health.ShowBar();

            //}
            if (!conversationDone)
            {
                if (i % 2 != 0)
                {
                    cam.cameraTargetHeightAdd = 1.2f;
                    cam.playerTarget = transform;
                 

                }
                else
                {
                   
                    cam.cameraTargetHeightAdd = 0f;
                    cam.playerTarget = player.transform;
                }
                message_UI.Update_UI(message[i]);
                   
            }
          
        }
      
     
       public  bool conversationDone = false;
        private void Update()
        {
            if (targeEnemy == null) return;

            if (targeEnemy.IsDead()) return;

            if (IsInRange())
            {
                MessageUpdate();
                movement.CancelAction();

            }
            else
            {
                animator.SetFloat("speed", 1f);

                movement.MoveToDestination(targeEnemy.transform.position,2f,false);
                CancelAction();
            }
        }
    }

}

