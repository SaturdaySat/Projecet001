using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    public Actor hostActor;
    public LevelDesign curLevel;

	void Start () {
        //先清除场景里所有的actor
        ClearScene();

        DontDestroyOnLoad(this);
        Instance = this;
        //EventManager.GetInstance().InitManager();
        CGameEventManager.GetInstance().InitManager();
        ActorManager.GetInstance().InitManager();
        LevelManager.GetInstance().InitManager();
        
        //生成关卡
        SceneManager.LoadScene(LevelConfig.Level01);
	}

    void Update()
    {
        if (hostActor == null)
        {
            return;
        }
        hostActor.Update(Time.deltaTime);
    }

    private void ClearScene()
    {
        GameObject[] actors = GameObject.FindGameObjectsWithTag("Actor");
        for (int i = 0; i < actors.Length; i++)
        {
            GameObject.Destroy(actors[i]);
        }
    }
 


}
