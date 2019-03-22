﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_BanditAttack : AI_EnemyAttack_Melee {
    [Header("Mandatory Filled (in editor/script)")]
    [SerializeField] Rigidbody rb;
    [Space]
    [Header("Attacking Parameters")]
    [SerializeField] float dashInForce;
    [SerializeField] float dashOutForce;
    [SerializeField] float jumpHeight;
    [SerializeField] float firstStrikeTime;
    [SerializeField] float startUpTime;
    [SerializeField] float intervalTime;
    [SerializeField] AnimationClip attackAnim;
    [SerializeField] AnimationClip backOutAnim;
    [SerializeField] bool forceDashOut;
    private bool isDashIn, isDashOut;
    private bool isFirstStrike;
    private float currTimer;
    private bool attackHasHit;
    private void Start(){
        InitializeParameters();
    }
    private void OnEnable(){
        InitializeParameters();
    }
    public void InitializeParameters(){
        isFirstStrike = true;
        currTimer = 0;
        base.SetIsEngaging(false);
        isDashIn = false;
        isDashOut = false;
        attackHasHit = false;
        base.SetIsAttacking(false);
        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@RESET THE HP OF THE BANDITS@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
    }
    private void Update(){
        if(base.GetIsEngaging()){
            if(!base.GetIsAttacking()){
                if(isFirstStrike){
                    if(currTimer<firstStrikeTime){
                        currTimer+=Time.deltaTime;
                    }else{
                        //attacks
                        base.SetIsAttacking(true);
                        currTimer=0;
                        isFirstStrike=false;
                    }
                }else{
                    if(currTimer<intervalTime){
                        currTimer+=Time.deltaTime;
                    }else{
                        //attacks
                        base.SetIsAttacking(true);
                        currTimer=0;
                    }
                }
            }else{
                    currTimer+=Time.deltaTime;
                if(currTimer<startUpTime){
                    attackHasHit = false;
                    //AI is preparing attack
                    StartPreparingAttack();
                }else if (currTimer<startUpTime+attackAnim.length){
                    //AI is attacking
                    StartAttackStrike();
                }else if (currTimer<startUpTime+attackAnim.length+backOutAnim.length){
                    //AI is backing off
                    if(forceDashOut||!attackHasHit){
                        StartDashOut();
                    }else{
                        StartDashReturn();
                    }

                }else{
                    base.SetIsAttacking(false);
                    currTimer = 0;
                    isDashIn=false;
                    isDashOut=false;
                    base.GetAnimator().SetInteger("ActID",0);
                    //Debug.Log("Attacking is false");
                }
            }
        }else{
            isFirstStrike = true;
        }
    }
    private void StartPreparingAttack(){
        //set animator to prep
        base.GetAnimator().SetInteger("ActID",1);
    }
    private void StartAttackStrike(){
        //set animator to prep
        base.GetAnimator().SetInteger("ActID",2);
        rb.AddForce(GetDashInVect());
    }
    private void StartDashOut(){
        //set animator to prep
        base.GetAnimator().SetInteger("ActID",3);
        rb.AddForce(GetDashOutVect());
    }
    private void StartDashReturn(){
        //set animator to prep
        base.GetAnimator().SetInteger("ActID",3);
        rb.AddForce(-1*GetDashInVect()*(attackAnim.length/backOutAnim.length));
    }
    public override void StartAttack()
    {
		base.SetIsEngaging(true);
    }

    public override void StopAttack()
    {
        base.SetIsEngaging(false);
        base.SetIsAttacking(false);
    }
    public void ReportHit(){
        attackHasHit = true;
    }
    private Vector3 GetDashInVect(){
        Vector3 launchDir = new Vector3(base.GetTarget().position.x,transform.position.y+jumpHeight,base.GetTarget().position.z)-transform.position;
        float distance = Vector3.Distance(transform.position,base.GetTarget().transform.position);
        return launchDir.normalized*dashInForce*distance;
    }
    private Vector3 GetDashOutVect(){
        Vector3 launchDir = transform.position-new Vector3(base.GetTarget().position.x,transform.position.y-jumpHeight,base.GetTarget().position.z);
        return launchDir.normalized*dashOutForce;
    }

    public void SetBanditParam(float atkInterval, float startUpTime){
        intervalTime = atkInterval;
        this.startUpTime = startUpTime;
    }

    public float GetAtkInterval(){
        return intervalTime;
    }
    public float GetStartUpTime(){
        return startUpTime;
    }
}
