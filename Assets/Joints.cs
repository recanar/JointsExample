using System;
using UnityEngine;

public class Joints : MonoBehaviour
{
    GameObject[] cubes;

    [SerializeField] private int n;//k�p say�s�

    LineRenderer lr;
    bool isLineRendererOn;

    void Start()
    {
        //Soru1();

        //Soru2();

        Soru4_JointGenericMethod<CharacterJoint>();

    }



    private void Update()
    {
        DrawLineRenderer();
        
    }
    private void Soru1()
    {
        GameObject go1 = new GameObject("go1");//obje1 olu�turuldu
        go1.AddComponent<Rigidbody>();//obje1 rb. eklendi

        GameObject go2 = new GameObject("go2");//obje2 olu�turuldu 
        Rigidbody go2Rb = go2.AddComponent<Rigidbody>();//obje2ye rb eklendi

        HingeJoint joint = go1.AddComponent<HingeJoint>();//1.objeye joint eklendi
        joint.connectedBody = go2Rb;//2.objeye joint ba�land�
    }
    private void Soru2()
    {
        cubes = new GameObject[n];
        HingeJoint joint;
        Rigidbody rb;
        cubes[0] = GameObject.CreatePrimitive(PrimitiveType.Cube);//zincirin ilk k�b� d�ng� d���nda olu�turuldu
        cubes[0].AddComponent<Rigidbody>();
        for (int i = 1; i < n; i++)
        {
            cubes[i] = GameObject.CreatePrimitive(PrimitiveType.Cube);//k�p� olu�tur
            cubes[i].transform.position = Vector3.right * i * 1.5f;//offset
            rb = cubes[i].AddComponent<Rigidbody>();
            joint = cubes[i - 1].AddComponent<HingeJoint>();
            joint.connectedBody = rb;
        }
        //LineRendereri aktif etmek i�in
        Soru3_AddLineRenderer();

    }
    private void Soru3_AddLineRenderer()
    {
        lr = gameObject.AddComponent<LineRenderer>();
        lr.positionCount = n;
        isLineRendererOn = true;//ko�ul sa�lan�rsa update methodunda DrawLineRenderer methodunu �al��t�r
    }
    private void DrawLineRenderer()
    {
        if (isLineRendererOn)
        {
            for (int i = 0; i < cubes.Length; i++)
            {
                lr.SetPosition(i, cubes[i].transform.position);
            }
        }
    }
    public void Soru4_JointGenericMethod<T>() where T : Joint
    {
        print("test");
        gameObject.GetComponent(typeof(T));

        cubes = new GameObject[n];

        Rigidbody rb;
        cubes[0] = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cubes[0].AddComponent<Rigidbody>();
        for (int i = 1; i < n; i++)
        {
            cubes[i] = GameObject.CreatePrimitive(PrimitiveType.Cube);//k�p� olu�tur
            cubes[i].transform.position = Vector3.right * i * 1.5f;//offset
            rb = cubes[i].AddComponent<Rigidbody>();
            cubes[i - 1].AddComponent(typeof(T));
            cubes[i - 1].GetComponent<Joint>().connectedBody = rb;//jointin hangi joint oldu�u bilinmese bile joint t�r�nden oldu�u biliniyor bu nedenle ba�lant� kurulabilir
        }
        //LineRendereri aktif etmek i�in
        Soru3_AddLineRenderer();
    }
}
