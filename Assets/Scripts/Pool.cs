using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    //스택
    public Stack<GameObject> inactive = new Stack<GameObject>();

    //생성된 객체들을 한 곳으로 모으기 위한 parent 설정
    public GameObject parent;

    //생성자
    public Pool(GameObject parent){
        this.parent = parent;
    }
}
