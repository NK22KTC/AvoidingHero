using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreatheFire : MonoBehaviour
{
    DragonSetting dragonSetting;

    [SerializeField]
    GameObject firePrehab;

    List<GameObject> fireObjects = new List<GameObject>();

    public enum dragonPos { Right, Left, Up, Down }
    public dragonPos pos = dragonPos.Right;

    int breathTime = 0;

    void Start()
    {
        dragonSetting = GameObject.FindWithTag("GameManager").GetComponent<DragonSetting>();

        StartCoroutine("GetEmitFire");
    }

    IEnumerator GetEmitFire()
    {
        breathTime += 1;

        GameObject fire = Instantiate(firePrehab, transform.position, firePrehab.transform.rotation);
        fireObjects.Add(fire);

        FireMove fireMove = fire.GetComponent<FireMove>();
        switch (pos)
        {
            case dragonPos.Right:
                fireMove.moving = FireMove.moveDirection.Left;
                break;
            case dragonPos.Left:
                fireMove.moving = FireMove.moveDirection.Right;
                break;
            case dragonPos.Up:
                fireMove.moving = FireMove.moveDirection.Down;
                break;
            case dragonPos.Down:
                fireMove.moving = FireMove.moveDirection.Up;
                break;
        }

        if(breathTime < 6)
        {
            yield return new WaitForSeconds(0.3f);
            StartCoroutine("GetEmitFire");
        }
        else
        {
            yield return new WaitForSeconds(3);

            for(int i = fireObjects.Count - 1; i > 0; i--)
            {
                Destroy(fireObjects[i]);
                fireObjects[i] = null;
            }
            dragonSetting.endPhase = true;

            Destroy(gameObject);
        }
    }
}
