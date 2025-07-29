using UnityEngine;

public class CubeDestroy : MonoBehaviour
{
    [SerializeField] public float cubesSize = 0.2f;
    [SerializeField] public int cubesInRow= 5;

    float cubesPivotDistance;
    Vector3 cubesPivot;

    private Rigidbody rb;
    public float explosionForce = 50f;
    public float explosionRadius = 4f;
    public float explosionUpward = 0.4f;
    void Start()
    {
        //calculation pivot distance(keisann)
        cubesPivotDistance = cubesSize * cubesInRow / 2;
        //use this value to create pivot vector
        cubesPivot = new Vector3(cubesPivotDistance, cubesPivotDistance, cubesPivotDistance);

        rb = GetComponent<Rigidbody>();
        //rb.isKinematic = true;//Don't move
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.isKinematic = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Floor")
        {
           Explod();
        }
    }

    public void Explod()
    {
        gameObject.SetActive(false);

        // loop 3 to create 5*5*5 pieces  in x,y,z coordinate
        for(int x = 0; x < cubesInRow; x++){
            for(int y = 0; y < cubesInRow; y++){
                for(int z = 0; z < cubesInRow; z++){
                    CreatePiece(x, y, z);
                }
            }
        }

        //get explosion position
        Vector3 explosionPos = transform.position;
        //get colliders in that position and radius
        Collider[] colliders = Physics.OverlapSphere(explosionPos,explosionRadius);

        //add explosion force to all colliders in that overlap spher
        foreach (Collider hit in colliders)
        {
            //get rigidbody from collider object
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                //add explosion force to this body with given parameters
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, explosionUpward);
            }
        }
    }

    public void CreatePiece (int x, int y, int z)
    {
        GameObject piece;
        piece = GameObject.CreatePrimitive(PrimitiveType.Cube);

        // set piece position and scale
        piece.transform.position = transform.position + new Vector3(cubesSize * x, cubesSize * y, cubesSize  * z) - cubesPivot;
        piece.transform.localScale = new Vector3(cubesSize, cubesSize, cubesSize);

        //add rigidbody and set mass
        piece.AddComponent<Rigidbody>();
        piece.GetComponent<Rigidbody>().mass = cubesSize;
    }
}
