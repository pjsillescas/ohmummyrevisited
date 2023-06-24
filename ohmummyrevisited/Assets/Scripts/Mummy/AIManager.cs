using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class AIManager : MonoBehaviour
{
    [SerializeField] private float AnimatorDelayTime = 2f;
    private Animator animator;
    private NavMeshAgent agent;
    private List<Vector3> positions;
    private Vector3 destination;
    private bool isActive;

	private void Awake()
	{
        isActive = false;
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        animator.enabled = false;
        StartCoroutine(DelayStartAnimator());
	}
    
    public void ActivateAI()
	{
        Debug.Log("activated ai");
        isActive = true;
        agent.destination = destination;
	}

	// Start is called before the first frame update
	void Start()
    {
        positions = FindObjectsByType<Crossroads>(FindObjectsSortMode.None).ToList().Select(o => o.transform.position).ToList();
        destination = GetNewDestination();
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            animator.SetFloat("Speed", agent.speed);
            if (Vector3.SqrMagnitude(transform.position - destination) < 3)
            {
                destination = GetNewDestination();
                agent.destination = destination;
            }
        }
    }

    private Vector3 GetNewDestination()
	{
        return positions[Random.Range(0, positions.Count - 1)];
    }

    private IEnumerator DelayStartAnimator()
	{
        yield return new WaitForSeconds(AnimatorDelayTime);

        animator.enabled = true;
        yield return null;
	}

    public void Die()
	{
        agent.enabled = false;
        animator.SetTrigger("Death");
        GetComponent<BoxCollider>().enabled = false;
	}

    public void Terminate()
	{
        Destroy(gameObject);
	}
}
