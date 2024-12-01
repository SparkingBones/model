using UnityEngine;
public class GameController : MonoBehaviour
{
    public PositionRecorder positionRecorder; // 记录位置的组件
    public PositionReplayer positionReplayer; // 重放位置的组件
    public Transform startPoint; // 起点位置
    public ColorChanger cubeColorChanger; // 用来引用 ColorChanger 脚本

    public GameObject shadowObject;
    public GameObject playerObject;
    private bool RecordingMode = true; // 判断是否为第一条命
   


    void Start()
    {
        // 游戏开始时，玩家回到起点并进入记录模式
        playerObject.transform.position = startPoint != null ? startPoint.position : Vector3.zero;
        positionRecorder.isRecording = true; // 开启记录模式
        positionReplayer.StopReplay(); // 确保播放模式为关闭
        if (cubeColorChanger != null)
        {
            cubeColorChanger.ChangeColor(Color.red); // 第一条命是红色
        }
        shadowObject.SetActive(false);  // 禁用物体，使其完全不可见且停用
    }

    void Update()
    {
        // 如果按下 R 键，切换模式
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (RecordingMode)
            {
                // 进入第二条命：回到起点，停止记录，进入播放模式
                shadowObject.SetActive(true);  // 禁用物体，使其完全不可见且停用
                RecordingMode = false;
                playerObject.transform.position = startPoint != null ? startPoint.position : Vector3.zero;
                positionRecorder.isRecording = false; // 停止记录
                positionReplayer.StartReplay(); // 启动播放
                if (cubeColorChanger != null)
                {
                    cubeColorChanger.ChangeColor(Color.blue); // 第一条命是红色
                }
            }
            else
            {
                // 如果是第二条命，重置回到记录模式，并清空记录
                RecordingMode = true;
                positionReplayer.StopReplay(); // 停止播放
                positionRecorder.recordedActions = new PlayerAction[10000]; // 清空记录
                playerObject.transform.position = startPoint != null ? startPoint.position : Vector3.zero;
                positionRecorder.StartRecording();// 启动记录模式
                if (cubeColorChanger != null)
                {
                    cubeColorChanger.ChangeColor(Color.red); // 第一条命是红色
                }
                shadowObject.SetActive(false);  // 禁用物体，使其完全不可见且停用
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (RecordingMode == false) 
            {
                SwapPositions(shadowObject, playerObject);
            }
        }
    }

    void SwapPositions(GameObject obj1, GameObject obj2)
    {
        // 临时存储 obj1 的位置
        Vector3 tempPosition = obj1.transform.position;

        // 将 obj1 的位置赋给 obj2
        obj1.transform.position = obj2.transform.position;

        // 将 obj2 的位置赋给 obj1
        obj2.transform.position = tempPosition;
    }
}
