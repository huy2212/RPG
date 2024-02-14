using RPG.Combat;
using UnityEngine;

[RequireComponent(typeof(Fighter))]
public class AttackAnimationController : MonoBehaviour
{
    [SerializeField] private int attackTypes = 3;
    private Animator animator;
    private float attackParameterAddAmount;
    private float currentAmount;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void OnEnable()
    {
        GetComponent<Fighter>().OnAttackCancel += ResetAttackType;
    }

    void OnDisable()
    {
        GetComponent<Fighter>().OnAttackCancel -= ResetAttackType;
    }

    void Start()
    {
        if (attackTypes <= 1)
        {
            attackParameterAddAmount = 0;
            return;
        }
        attackParameterAddAmount = (float)1 / (attackTypes - 1);
    }

    public void ChangeAttackType()
    {
        CalculateCurrentAttackAmount();
        animator.SetFloat("attackType", currentAmount);
    }

    private void CalculateCurrentAttackAmount()
    {
        currentAmount += attackParameterAddAmount;
        if (currentAmount > 1)
        {
            currentAmount = 0;
        }
    }

    private void ResetAttackType()
    {
        currentAmount = 0;
        animator.SetFloat("attackType", currentAmount);
    }
}