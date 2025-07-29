using UnityEngine;
using System;

public class CrackCube : MonoBehaviour
{
    public float maxHP  = 100f;
    public float currentHP;
    [SerializeField] public Material cubeMaterial;

    [SerializeField]public float crackStage = 0f;
    [SerializeField]public float crackSpeed = 0.2f;

    private CubeDestroy cubeDestroy;
    void Start()
    {
        cubeMaterial.SetFloat("_CrackStage", 0);
        currentHP = maxHP;
        cubeDestroy = GetComponent<CubeDestroy>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            crackStage += crackSpeed;
            crackStage = Mathf.Min(crackStage, 5.0f);//Max = 5
            cubeMaterial.SetFloat("_CrackStage", crackStage);

            TakeDamage(10f);
        }

        if(currentHP <= 0)
        {
            cubeDestroy.Explod();
        }
    }

    public void TakeDamage( float damage)
    {
        //HPDown
        currentHP -= damage;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);

        //+Crack
        float crack = 1f;// - (currentHP / maxHP);
     // cubeMaterial.SetFloat("_CracksStrength", crack);

        float radius = 1f - (currentHP / maxHP);
       // cubeMaterial.SetFloat("_CrackRadius", radius);
    }
}
