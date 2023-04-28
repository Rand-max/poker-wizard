using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;
 
public class PlayerController : MonoBehaviour
{
    public int playerNumber;
    public int TeamNumber;
    public Texture playerTexture;
    public Texture playerfastenTexture;
    public GameObject playerCursor;
    [SerializeField] float currentSpeed;
    [SerializeField] public VisualEffect driftPar;
    float speed;
    float rotate;
    [SerializeField] float currentRotate;
    int driftDirection;
    float driftPower;
    public float driftSpeed;
    int driftMode = 0;
    public Transform cam;
    public Transform playerModel;
    public Transform Normal;
    public Rigidbody rb;
    [Header("Bools")]
    public bool drifting;
    [Header("Parameters")]
    public List<float> multipliers;
    public List<float> multipliers_timer;
    public float acceleration = 10f;
    public float steering = 1f;
    public float gravity = 10f;
    public LayerMask layerMask;
    private Vector2 inputDirection;
    private bool inputDrift;
    public LayerMask playerlayer;
    public bool isdrift;
    public bool isJoked;
    public float joking;
    public int JokeStrength=0;
    void Start ()
    {
        DontDestroyOnLoad(this.transform.parent);
        driftPar.Stop();
    }
    public void OnTurn(InputAction.CallbackContext ctx){
        inputDirection=ctx.ReadValue<Vector2>();
        if(playerCursor)playerCursor.GetComponent<CursorKey>().movecursor(inputDirection);
    }
    public void OnDrift(InputAction.CallbackContext ctx){
        inputDrift=ctx.ReadValueAsButton();
    }
    void Update()
    {
        transform.position = rb.transform.position + new Vector3(0, 0.4f, 0);
        if(inputDirection.sqrMagnitude > 1)
        {
            inputDirection = inputDirection.normalized;
        }
        if (inputDirection.y != 0)
        {
            speed = Mathf.Abs(inputDirection.y)*acceleration;
        }
        for(int i=0;i<multipliers.Count;i++){
            speed*=multipliers[i];
            if(speed<0){
                inputDirection=-inputDirection;
            }
            if(speed==0){
                inputDirection.x=0f;
            }
            multipliers_timer[i]-=Time.deltaTime;
            if(multipliers_timer[i]<=0){
                multipliers_timer.RemoveAt(i);
                multipliers.RemoveAt(i);
            }
        }
        if (driftPower > 0)
            {
                speed+=currentSpeed*driftSpeed/3;
                driftPower-=1;
            }
        
        //drift?
        if (inputDrift && !drifting && Mathf.Abs(inputDirection.y)>= 0.1)
        {
            if(!isdrift){
                isdrift=true;
                if(inputDirection.x > 0.1){
                    GetComponentInChildren<Animator>().Play("Armature_right");
                    driftPar.Play();
                }
                else if(inputDirection.x < -0.1){
                    GetComponentInChildren<Animator>().Play("Armature_left");
                    driftPar.Play();
                }
            }
            
            drifting = true;
            driftDirection = inputDirection.x > 0.1 ? 1 : inputDirection.x < -0.1?-1:0;
            

            //playerModel.parent.DOComplete();psa
            //playerModel.parent.DOPunchPosition(transform.up * .2f, .3f, 5, 1);psa

        }
        //steer?
        if (inputDirection.x != 0&&!drifting)
        {
            //driftPar.Stop();
            int dir = inputDirection.x > 0 ? 1 : -1;
            float amount = Mathf.Abs(inputDirection.x);
            Steer(dir, amount*1.6f);
        }
        
        if (Mathf.Abs(inputDirection.x) >= 0.1&&drifting)
        {
            //driftPar.Play();
            float control = (driftDirection == 1) ? Remap(inputDirection.x, -1, 1, 0, 2) : Remap(inputDirection.x, -1, 1, 2, 0);
            float powerControl = (driftDirection == 1) ? Remap(inputDirection.x, -1, 1, .2f, 1) : Remap(inputDirection.x, -1, 1, 1, .2f);
            Steer(driftDirection, control);
            driftPower += powerControl;

            //ColorDrift();
        }
        if (inputDrift && drifting)
        {
            //Boost();
            drifting=false;
        }
        
        //Animations    

        //a) Kart
        if (!drifting)
        {
            if(isdrift){
                isdrift=false;
                if(inputDirection.x > 0.1){
                    GetComponentInChildren<Animator>().Play("Armature_right");
                }
                else if(inputDirection.x < -0.1){
                    GetComponentInChildren<Animator>().Play("Armature_left");
                }
            }
            playerModel.localEulerAngles = Vector3.Lerp(playerModel.localEulerAngles, new Vector3(0, (inputDirection.x * 15), playerModel.localEulerAngles.z), 10f/Time.deltaTime);
        }
        else
        {
            float control = (driftDirection == 1) ? Remap(inputDirection.x, -1, 1, .5f, 2) : Remap(inputDirection.x, -1, 1, 2, .5f);
            //playerModel.parent.localRotation = Quaternion.Euler(0, Mathf.LerpAngle(playerModel.parent.localEulerAngles.y,(control * 15) * driftDirection, .2f), 0);
        }
        /*
        //b) Wheels
        frontWheels.localEulerAngles = new Vector3(0, (Input.GetAxis("Horizontal") * 15), frontWheels.localEulerAngles.z);
        frontWheels.localEulerAngles += new Vector3(0, 0, sphere.velocity.magnitude/2);
        backWheels.localEulerAngles += new Vector3(0, 0, sphere.velocity.magnitude/2);

        //c) Steering Wheel
        steeringWheel.localEulerAngles = new Vector3(-25, 90, ((Input.GetAxis("Horizontal") * 45)));
        */
        if(isJoked){
            joking-=Time.deltaTime;
            if(joking<0){
                joking=0;
                isJoked=false;
                JokeStrength=0;
            }
        }
    }
    void FixedUpdate ()
    {
        // CHANGED -- This limits movement speed so you won't move faster when holding a diagonal. It's just a pet peeve of mine
        if(inputDirection.sqrMagnitude > 1)
        {
            inputDirection = inputDirection.normalized;
        }
        
        // CHANGED -- This takes the camera's facing into account and flattens the controls to a 2-D plane
        
        //Vector3 newRight = Vector3.Cross(Vector3.up, cam.forward);
        //Vector3 newForward = Vector3.Cross(newRight, Vector3.up);
        //Vector3 movement = (newRight * inputDirection.x) + (newForward * inputDirection.y);
        Vector3 movement = (transform.right * inputDirection.x) + (transform.forward * inputDirection.y);
        //rb.AddForce(movement * speed);
        
        //Forward Acceleration
        if(!drifting){
            rb.AddForce(movement * currentSpeed, ForceMode.Acceleration);
        }
        else{
            rb.AddForce(transform.forward * inputDirection.y * currentSpeed, ForceMode.Acceleration);
        }
        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(transform.eulerAngles.x, transform.eulerAngles.y+ currentRotate, transform.eulerAngles.z), 10f/Time.deltaTime);
        //Gravity
        rb.AddForce(Vector3.down * gravity, ForceMode.Acceleration);

        //Steering
        currentSpeed = Mathf.SmoothStep(currentSpeed, speed,12f/Time.deltaTime); speed = 0f;
        currentRotate = Mathf.Lerp(currentRotate, rotate,4f/Time.deltaTime); rotate = 0f;

        RaycastHit hitOn;
        RaycastHit hitNear;

        Physics.Raycast(transform.position + (transform.up*.1f), Vector3.down, out hitOn, 1.1f,layerMask);
        Physics.Raycast(transform.position + (transform.up * .1f)   , Vector3.down, out hitNear, 2.0f, layerMask);

        //Normal Rotation
        Normal.up = Vector3.Lerp(Normal.up, hitNear.normal, Time.deltaTime * 8.0f);
        Normal.Rotate(0, transform.eulerAngles.y, 0);
    }
    public void Boost()
    {
        drifting = false;

        if (driftMode > 0)
        {
            //DOVirtual.Float(currentSpeed * 3, currentSpeed, .3f * driftMode, Speed);psa
            //DOVirtual.Float(0, 1, .5f, ChromaticAmount).OnComplete(() => DOVirtual.Float(1, 0, .5f, ChromaticAmount));
            //playerModel.Find("Tube001").GetComponentInChildren<ParticleSystem>().Play();
            //playerModel.Find("Tube002").GetComponentInChildren<ParticleSystem>().Play();
        }

        driftPower = 0;
        driftMode = 0;

        /*foreach (ParticleSystem p in primaryParticles)
        {
            p.startColor = Color.clear;
            p.Stop();
        }
        */
        //playerModel.parent.DOLocalRotate(Vector3.zero, .5f).SetEase(Ease.OutBack);psa
    }
    public void Steer(int direction, float amount)
    {
        rotate = (steering * direction) * amount*3;
    }
    private void Speed(float x)
    {
        speed = x;
    }
    public float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
    public void MultiplySpeed(float rate,float time){
        multipliers.Add(1+rate);
        multipliers_timer.Add(time);
    }
    public void SingleDirection(float force,Vector3 forward){
        Debug.Log("boost"+forward*force);
        rb.AddForce(forward*force*1000);
    }
    public void InvertAxis(float time){
        multipliers.Add(-1);
        multipliers_timer.Add(time);
    }
    public void ShockedAni(){
        this.gameObject.GetComponent<Animator>().Play("Armature_panic",0,0f);
        FindObjectOfType<AudioManager>().Play("shocked");
    }
    public void Joke(float time,int jokeStrength){
        isJoked=true;
        joking=time;
        JokeStrength+=1;
    }
}
