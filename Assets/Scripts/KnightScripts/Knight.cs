using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour
{

    public float speed = 2;
    Animator animator;
    SpriteRenderer spRenderer;
    [SerializeField] AudioClip[] stepSounds;
    AudioSource audSource;
    [SerializeField] ParticleSystem stepParticle;

    public bool canRun = true;

    // Start is called before the first frame update
    void Start()
    {

        animator = GetComponent<Animator>();
        spRenderer = GetComponent<SpriteRenderer>();
        audSource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {

        float direction = Input.GetAxisRaw("Horizontal");
        spRenderer.flipX = direction < 0;

        animator.SetFloat("speed", Mathf.Abs(direction));

        if (Input.GetMouseButtonDown(0))
        {

            animator.SetTrigger("attack");
            canRun = false;

        }

        if (canRun)
        {

            transform.position += transform.right * direction * speed * Time.deltaTime;

        }

        
        
    }

    public void AttackHasFinished()
    {

        canRun = true;
        Debug.Log("Attack done.");

    }

    public void HeroStep()
    {

        int clipToPlay = Random.Range(0, stepSounds.Length);

        audSource.PlayOneShot(stepSounds[clipToPlay]);
        stepParticle.Emit(5);

    }

}
