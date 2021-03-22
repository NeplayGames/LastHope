
using lastHope.movement;


using UnityEngine;

public class Zipline : MonoBehaviour
{
    Transform player;
    [SerializeField]Transform zipline;
    PlayerMoment playerMoment;
    bool isZipling = false;
    GameInfoProvider gameInfoProvider;
    Animator controller;
    bool fall = false;
    bool gravityChange = false;
float timeChange = 0f;
    float gravityValue = 0;
    Vector3 direct = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMoment>().transform;
      
        playerMoment = player.GetComponent<PlayerMoment>();
        gameInfoProvider = FindObjectOfType<GameInfoProvider>();
        controller = player.GetChild(0).GetComponent<Animator>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if(gameInfoProvider==null)
            gameInfoProvider = FindObjectOfType<GameInfoProvider>();



        if (Vector3.Distance(player.position, transform.position) < 3f)
        {

            gameInfoProvider.showInfo++;
            gameInfoProvider.info = "Zipline";
            gameInfoProvider.button = "E";
            if (Input.GetKeyDown(KeyCode.E))

            {
                controller.SetTrigger("Zipline");
                isZipling = true;
                player.position = transform.position + Vector3.up * 4.5f + Vector3.forward;
                direct = (transform.position - zipline.transform.position).normalized;
            }
        }
       

        if (isZipling && !fall)
        {
            PlayerTransport(direct);

        }
        if(isZipling && !playerMoment.grounded && fall)
        {
            controller.SetFloat("Ziplining", 0);
	if(gravityChange == true){
                gravityValue = playerMoment.gravity * 2;
//  playerMoment.gravity = playerMoment.gravity* 2;
gravityChange = false;



}timeChange +=Time.deltaTime;
            playerMoment.gravity = Mathf.Lerp(playerMoment.gravity, gravityValue, 0.3f);
if(timeChange >=1){
timeChange = 0;
gravityChange = true;
}
        
	
        }
        else if(isZipling && fall)
        {

           
            controller.SetFloat("Ziplining", 1);
            isZipling = false;
            playerMoment.pState = PlayerMoment.PlayerState.idle;
            fall = false;
            controller.SetTrigger("ZiplineToMoment");
            playerMoment.gravity = 2.5f;


        }





    }

    private void PlayerTransport(Vector3 Direction)
    {
        playerMoment.pState = PlayerMoment.PlayerState.zipline;
        playerMoment.gravity = 0f;

  
        player.transform.position = player.transform.position - Direction * 70  * Time.deltaTime;
        if (Vector3.Distance(zipline.position, player.position) < 5 || Input.GetKeyDown(KeyCode.Space))
        {
            fall = true;
            controller.SetTrigger("ZiplineFall");
            gravityValue = 2.5f;
            playerMoment.gravity = 2.5f;
        }
    }

}
