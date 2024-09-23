using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setAttributes : MonoBehaviour
{
    private static setAttributes _instance;
    public static setAttributes instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<setAttributes>();
            return _instance;
        }
    }
    //Movement Factors
    [RangeAttribute(0, 2)]
    public int Locomotion = 0;
    private int _Locomotion = -1;
    [RangeAttribute(0, 2)]
    public int Motion = 0;
    [RangeAttribute(0, 2)]
    public int Closeness = 0;

    // Apearance Factors
    [RangeAttribute(0, 2)]
    public int Largness = 0;
    [RangeAttribute(0, 1)]
    public int Hairiness = 0;
    [RangeAttribute(0, 2)]
    public int Color = 0;

    float waitingTime = 0f;
    float walkingDuration = 10f;
    private float _startTime;

    private SkinnedMeshRenderer renderer;
    private Animator animator;

    private Locomotion locomotion;
    private SpiderDriver spiderDriver;
    /*
    private int _locomotionState;
    public int locomotionState
    {
        get
        {
            return _locomotionState;
        }
        set
        {
            if (value != _locomotionState)
            {
                _locomotionState = value;
                setLocomotion();
            }
        }
    }
    */
    private IEnumerator ieStanding;

    //public Material material;
    private void setLargness()
    {
        float scale = 1f;
        switch (this.Largness)
        {
            case 0: //small
                scale = 0.25f;
                break;
            case 1: //meduim
                scale = 0.5f;
                break;
            case 2: //large
                scale = 0.75f;
                break;
            default:
                scale = 1f;
                break;
        }
        gameObject.transform.localScale = new Vector3(scale, scale, scale);
    }

    private void setColor()
    {
        renderer.material = Resources.Load("FurMaterial", typeof(Material)) as Material;
        //renderer.material = material;
        Color newColor;
        Color newSpecular;

        newColor = new Color32(255, 255, 255, 255);
        switch (this.Color)
        {
            case 0: //Gray
                newSpecular = new Color32(147, 137, 137, 255);
                break;
            case 1: //Brown
                newSpecular = new Color32(118, 33, 38, 255);
                break;
            case 2: //Black
                newSpecular = new Color32(0, 0, 0, 255);
                break;
            default:
                newSpecular = new Color32(0, 0, 0, 255);
                break;
        }
        renderer.material.SetColor("_Color", newColor);
        renderer.material.SetColor("_Specular", newSpecular);

    }

    void setHairiness()
    {

        float LengthHair = 0.08f;
        switch (this.Hairiness)
        {
            case 0: //without hair
                renderer.material.SetFloat("_FurLength", 0.0f);
                break;
            case 1: //with hair
                renderer.material.SetFloat("_FurLength", LengthHair);
                break;
            default:
                renderer.material.SetFloat("_FurLength", 0.0f);
                break;
        }
    }

    public void Jump()
    {
        if (Locomotion == 2)
        {
            locomotion.Jump();
        }
    }

    public void OnLandCallback()
    {
        _startTime = Time.time;
    }

    public IEnumerator IEStanding(float delay)
    {
        locomotion.isStanding = true;
        yield return new WaitForSeconds(delay);
        locomotion.isStanding = false;
        Debug.Log("It should jump!");
        _startTime = Time.time;
        Jump();
    }

    public void Standing(float delay)
    {
        if (ieStanding != null)
        {
            StopCoroutine(ieStanding);
        }
        ieStanding = IEStanding(delay);
        StartCoroutine(ieStanding);
    }
    void setLocomotion()
    {
        // changeeeeee
        if (Locomotion != _Locomotion)
            _Locomotion = Locomotion;
        else
            return;
        switch (this.Locomotion)
        {
            case 0: //Standing
                locomotion.isStanding = true;
                break;
            case 1: //walking
                locomotion.isStanding = false;
                break;
            case 2: //jumping
                locomotion.isStanding = false;
                break;
            default:
                    
                break;    
        }
        


    }


    void setMotion()
    {
        
        switch (this.Motion)
        {
            case 0: //Slightly
                locomotion.moveSpeed = 1f;
                waitingTime = 5f;
                walkingDuration = 8f;
                break;
            case 1: //moderate
                locomotion.moveSpeed = 1.5f;
                waitingTime = 3f;
                walkingDuration = 10f;
                break;
            case 2: //too much
                locomotion.moveSpeed = 3f;
                waitingTime = 1f;
                walkingDuration = 12f;
                break;
            default:
                break;
        }

    }

    void MotionHandler()
    {
        if (Locomotion != 0 && locomotion.isWalking)
        {
            if (Time.time - _startTime >= walkingDuration)
            {
                //locomotion.isStanding = !locomotion.isStanding;
                Standing(waitingTime);
                _startTime = Time.time;
            }
        }
    }

    void setCloseness()
    {

        switch (this.Closeness)
        {
            case 0: //far away
                spiderDriver.radius1 = 7f;
                spiderDriver.radius2 = 9f;
                break;
            case 1: //in the middle
                spiderDriver.radius1 = 5f;
                spiderDriver.radius2 = 7f;
                break;
            case 2: //very close
                spiderDriver.radius1 = 3f;
                spiderDriver.radius2 = 5f;
                break;
            default:
                break;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        renderer = gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
        animator = GetComponent<Animator>();
        locomotion = GetComponent<Locomotion>();
        spiderDriver = GetComponent<SpiderDriver>();
        setValues();

    }

    private void setAnimationParameters()
    {
        locomotion.jumpIntensity = 200f + (Largness * 100f);
        locomotion.animatorWalkSpeed = 0.9f - (Largness * 0.2f) + (Motion * 0.3f);
    }
    private void setValues()
    {
        setLargness();
        setColor();
        setHairiness();

        setLocomotion();
        setCloseness();
        setMotion();

        setAnimationParameters();
    }
    // Update is called once per frame
    void Update()
    {
        MotionHandler();
        setValues();
    }
}
