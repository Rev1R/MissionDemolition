using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    //Статистическое поле, доступное любому другому коду
    static public bool goalMet = false;

    void OnTriggerEnter(Collider other)
    {
        //Когда в область действия триггера попадает что-то, проверить, является ли "что-то" снарядом
        if ( other.gameObject.tag == "Projectile")
        {
            //Если это снаряд, присвоить полю goalMet значение true
            Goal.goalMet = true;
            //Также изменить альфа-канал цвет, чтобы увелить непрозрачность
            Material mat = GetComponent<Renderer>().material;
            Color c = mat.color;
            c.a = 1;
            mat.color = c;
        }
    }
}
