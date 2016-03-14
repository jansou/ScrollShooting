using UnityEngine;
using System.Collections;

public class BulletArch : MonoBehaviour {
    //Transform pt;
	//Rigidbody2D rigid;
	//float spd;

    /// <summary>
    public Transform Target;
    public float firingAngle = 45.0f;
    public float gravity = 9.8f;
    public Transform Projectile;
    //private Transform myTransform;
    /// </summary>

    public float Vx;
    public float Vy;

    void Awake()
    {
        //myTransform = transform;
    }

	// Use this for initialization
    void Start()
    {
        //pt = FindObjectOfType<Party>().transform;
        //rigid = GetComponent<Rigidbody2D>();
        //spd = GetComponent<Bullet>().speed;

        Target = FindObjectOfType<Party>().transform;
        Projectile = transform;

        //myTransform = transform;
        StartCoroutine("Arch");
    }

    IEnumerator Arch()//プレイヤーの位置まで放物線を描きたいhttp://hakuhin.jp/as/shot.html#SHOT_02_02
    {
        
        // Short delay added before Projectile is thrown
        yield return new WaitForSeconds(0f);
        /*
        // Move Projectile to the position of throwing object + add some offset if needed.
        //Projectile.position = myTransform.position + new Vector3(0, 0.0f, 0);
        Debug.LogError("Projectile.position:" + Projectile.position);
        // Calculate distance to target
        float target_Distance = Vector3.Distance(Projectile.position, Target.position);
        Debug.Log("target_Distance:"+ target_Distance);
        // Calculate the velocity needed to throw the object to the target at specified angle.
        float Projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);
        Debug.Log("Projectile_Velocity:"+Projectile_Velocity);
        // Extract the X  Y componenent of the velocity
//        float 
         Vx = Mathf.Sqrt(Projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        //float 
        Vy = Mathf.Sqrt(Projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

        // Calculate flight time.
        float flightDuration = target_Distance / Vx;

        // Rotate Projectile to face the target.
        //Projectile.rotation = Quaternion.LookRotation(Target.position - Projectile.position);
        
        float z = Projectile.localrotation.z;
        Projectile.localrotation.z = Projectile.localrotation.y;
        Projectile.localrotation.y=z;
        
        float elapse_time = 0;

        while (elapse_time < flightDuration)
        {
            Projectile.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);

            elapse_time += Time.deltaTime;

            yield return null;
        }
        */
    }  

	// Update is called once per frame
	void Update () {
	
	}
}
