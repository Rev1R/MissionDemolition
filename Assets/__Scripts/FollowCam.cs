using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    static public GameObject POI;  //Ссылка на интересующтй объект    //a
    [Header("Set In Inspector")]
    public float easing = 0.05f;
    public Vector2 minXY = Vector2.zero;

    [Header("Set Dynamically")]
    public float camZ;              //желаемая координа Z камеры

    void Awake()
    {
        camZ = this.transform.position.z;
    }

    void FixedUpdate()
    {
        // Однострочная версия if не требует фигурных скобок
        //if (POI == null) return;  //Выйти, если нет интересующего объекта  //b
        //
        //получить позицю интересующего объекта
        // Vector3 destination = POI.transform.position;


        Vector3 destination;
        //если нет интересующего объекта, вернуть P:[0 ,0 ,0 ]
        if (POI == null)
        {
            destination = Vector3.zero;
        }
        else
        {
            //получить позицию интересующего объекта 
            destination = POI.transform.position;
            //если интересующий объект - снаряд, убедиться, что он отановился
            if (POI.tag == "Projectile")
            {
                //если он стоит на месте (то есть не двигается)
                if (POI.GetComponent<Rigidbody>().IsSleeping())
                {
                    //вернуть исходные настройки поля зрения камеры
                    POI = null;
                    //в следующем кадре 
                    return;
                }
            }
        }
        //Ограничить X и Y минимальными значениями
        destination.x = Mathf.Max(minXY.x, destination.x);
        destination.y = Mathf.Max(minXY.y, destination.y);
        //Определить точку между текущим местоположением камеры и destination
        destination = Vector3.Lerp(transform.position, destination, easing);
        //Принудительно установить значение destination.z равным camZ, чтобы отодвинуть камеру подальше
        destination.z = camZ;
        //Поместить камеру в позицию destination
        transform.position = destination;
        //Изменить размер orthographicSize камеры, чтобы земля оставалась в поле зрения
        Camera.main.orthographicSize = destination.y + 10;
    }
}
