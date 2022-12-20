using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Remove MonoBehaviour and make class static .. MonoBehaviour는 무슨 의미?
public static class Pooler 
{
    //All Pools should be Contained to static Dictionary type, Name as pools
    //redPool, yelloPool, etc.. can be contained
    //Dictionary <key, value>
    private static Dictionary<string, Pool> pools = new Dictionary<string, Pool>();


    //Func can be used outside so public
    //Look Similar as instantiate
    //key is name of objects / replace name - Object Pooling을 사용하면, 프리팹을 사용해서인지 뒤에 clone이 붙는데 
    //해당 (clone)을 "" 즉 공백으로 치환하여 객체이름만 깔끔하게 받아오게 하기 위함, 굳이 하지 않아도 상관은 없다.
    public static void Spawn(GameObject gameObject, Vector3 pos, Quaternion rot)
    {
        GameObject obj;
        string key = gameObject.name.Replace("(Clone)", "");

        if (pools.ContainsKey(key))
        {
            //pool 내부에 있는 실제 pool인 inactive stack / transform은 왜 해준건지 모르겠다. - 링크 참고
            //굳이 stack을 하지 않고 queue로 했어도 됬겠다. pop
            //public static Object Instantiate(Object original, Vector3 position, Quaternion rotation, Transform parent);
            if (pools[key].inactive.Count == 0)
            {
                Object.Instantiate(gameObject, pos, rot, pools[key].parent.transform);
            }
            //else 해당설정 그대로 spawn하면 된다. //GET
            else
            {
                obj = pools[key].inactive.Pop();
                obj.transform.position = pos;
                obj.transform.rotation = rot;
                obj.SetActive(true);
            }
        }
        //there is no pool for this key
        else
        {
            //생성된 게임오브젝트는 어디에 저장??? > 
            //부모-게임오브젝트 / 객체-그냥오브젝트, 부모에 할당 / 그러면 그냥 부모에 딸린 자식 오브젝트임
            //이제 pool 클래스 생성자에 parent를 넣어 만들고, pool화 시킴
            //해당 pool을 관리하는 pools 딕셔너리에 만든 pool을 할당
            GameObject newParent = new GameObject($"{key}_POOL");
            GameObject.Instantiate(gameObject, pos, rot, newParent.transform);
            //initialize pool and add pool to Dic.
            Pool newPool = new Pool(newParent);
            pools.Add(key, newPool);
        }
    }

    public static void Despawn(GameObject gameObject)
    {
        string key = gameObject.name.Replace("(Clone)","");

        //RETURN USED
        //gameObject.transform.position = pools[key].parent.transform.position; 무슨말이지

        if(pools.ContainsKey(key)){
            pools[key].inactive.Push(gameObject);
            gameObject.transform.position = pools[key].parent.transform.position;
            gameObject.SetActive(false);
        }
      
    }

}

//먼저 spawn에서 게임 오브젝트, pos, rot을 받게 되고,
//자동으로 key값이 게임 오브젝트의 이름.replace되어 저장이 된다.
//해당 키가 처음엔 없으므로.
//해당 키에 대한 풀을 생성해야하는데, 이때의 부모는 key에 의해서 알아서 만들어진다.
//키와 부모는 플레이어가 설정하는 것이 아니라 그냥 변수의 이름에 의해서 자동으로 결정되는 코드이다.
//부모가 instantiate할 때 쓰이는데, 
//Pool의 객체를 사용하지 않는다면, pool에 잠자고 있어야 하므로, 해당 키값을 가진 pool의 parent의 위치를 찾아서 들어가게 된다.
//일단 작동을 봐야 할 것 같다. 
//새로만들때, Object.Instantiate(gameObject, pos, rot, pools[key].parent.transform);
//다시넣을때, gameObject.transform.position = pools[key].parent.transform.position; 부모의 위치로 가는건 같은데, 사실 그냥 pool의 부모만 쓰면 되는게 아닐가
//pool 부모 내에서 꺼졌다 켜졌다 작동하면 내 생각이 맞았다는것..


//instantinate구문 참고 - 부모의 transform
//https://docs.unity3d.com/ScriptReference/Object.Instantiate.html