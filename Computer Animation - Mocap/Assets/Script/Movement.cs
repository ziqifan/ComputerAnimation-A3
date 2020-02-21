using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    private Animator _animator;

    public float MaxSpeed = 1;

    private bool stopMove = false;

	// Use this for initialization
	void Start () {
        _animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        if (_animator == null) return;

        if(!stopMove)
        {
            var x = Input.GetAxis("Horizontal");
            var y = Input.GetAxis("Vertical");

            move(x, y);
        }

        GetInput();
    }

    private void move(float x, float y)
    {
        _animator.SetFloat("VelX", x);
        _animator.SetFloat("VelY", y);

        transform.position += (Vector3.forward * MaxSpeed) * y * Time.deltaTime;
        transform.position += (Vector3.right * MaxSpeed) * x * Time.deltaTime;
    }

    private void GetInput()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            
            LightAttack();
        }
        if(Input.GetButtonDown("Fire2"))
        {
            StrongAttack();
        }
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    private void LightAttack()
    {
        StartCoroutine(LightAttackRoutine());
    }
    IEnumerator LightAttackRoutine()
    {
        stopMove = true;
        _animator.SetInteger("Condition", 2);
        yield return new WaitForSeconds(1);
        _animator.SetInteger("Condition", 0);
        stopMove = false;
    }

    private void StrongAttack()
    {
        StartCoroutine(StrongAttackRoutine());
    }
    IEnumerator StrongAttackRoutine()
    {
        stopMove = true;
        _animator.SetInteger("Condition", 1);
        yield return new WaitForSeconds(1);
        _animator.SetInteger("Condition", 0);
        stopMove = false;
    }

    private void Jump()
    {
        StartCoroutine(JumpRoutine());
    }
    IEnumerator JumpRoutine()
    {
        stopMove = true;
        _animator.SetBool("Grounded", false);
        yield return new WaitForSeconds(1);
        _animator.SetBool("Grounded", true);
        stopMove = false;
    }
}
