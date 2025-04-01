using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Tilemaps;

public class Knight : MonoBehaviour
{

    public float speed = 2;
    Animator animator;
    SpriteRenderer spRenderer;
    [SerializeField] AudioClip[] stepSounds;
    AudioSource audSource;
    [SerializeField] ParticleSystem stepParticle;

    public CinemachineImpulseSource impulseSource;

    public LineRenderer lineRend;

    public Tilemap tileMap;
    public Tile stone;

    public Vector2 previousPos;
    public Vector2 destinationPos;
    public List<Vector2> posList;

    public bool onStone = false;

    public bool ClickMoveMode = true;

    public float time = 0;

    public bool canRun = true;

    // Start is called before the first frame update
    void Start()
    {

        animator = GetComponent<Animator>();
        spRenderer = GetComponent<SpriteRenderer>();
        audSource = GetComponent<AudioSource>();

        posList = new List<Vector2>();
        lineRend.positionCount = 0;

        lineRend.positionCount++;

        lineRend.SetPosition(lineRend.positionCount - 1, transform.position);

    }

    // Update is called once per frame
    void Update()
    {

        Vector3Int gridPos = tileMap.WorldToCell(transform.position);

        TileBase tileSelect = tileMap.GetTile(gridPos);

        if(tileSelect == stone)
        {

            onStone = true;

        }
        else
        {

            onStone = false;

        }

        float direction = Input.GetAxisRaw("Horizontal");        

        float directionY = Input.GetAxisRaw("Vertical");

        Vector2 playerPos = transform.position;

        if (destinationPos != null)
        {

            time += Time.deltaTime;
            transform.position = Vector2.Lerp(previousPos, destinationPos, time);

            if (previousPos.x < destinationPos.x)
            {

                direction = 1;

            }
            else if(previousPos.x > destinationPos.x)
            {

                direction = -1;

            }
            
            if(time >= 1)
            {

                direction = 0;

            }

            spRenderer.flipX = direction < 0;

        }

        animator.SetFloat("speed", Mathf.Abs(direction));       

        if (Input.GetMouseButtonDown(0))
        {

            animator.SetTrigger("attack");
            canRun = false;

        }

        if (canRun && !ClickMoveMode)
        {

            transform.position += transform.right * direction * speed * Time.deltaTime;
            transform.position += transform.up * directionY * speed * Time.deltaTime;

        }

        
        
    }

    public void AttackHasFinished()
    {

        canRun = true;
        Debug.Log("Attack done.");

    }

    public void HeroStep()
    {

        if (onStone)
        {

            int clipToPlay = Random.Range(0, stepSounds.Length);

            audSource.PlayOneShot(stepSounds[clipToPlay]);
            stepParticle.Emit(5);
            impulseSource.GenerateImpulseWithForce(1);

        }       

    }

    public void UpdateDestination(Vector2 clickPos)
    {

        time = 0;
        posList.Add(clickPos);
        lineRend.positionCount++;

        lineRend.SetPosition(lineRend.positionCount-1, clickPos);

        previousPos = transform.position;
        destinationPos = clickPos;

    }

}
