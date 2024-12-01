using UnityEngine;
public class GameController : MonoBehaviour
{
    public PositionRecorder positionRecorder; // ��¼λ�õ����
    public PositionReplayer positionReplayer; // �ط�λ�õ����
    public Transform startPoint; // ���λ��
    public ColorChanger cubeColorChanger; // �������� ColorChanger �ű�

    public GameObject shadowObject;
    public GameObject playerObject;
    private bool RecordingMode = true; // �ж��Ƿ�Ϊ��һ����
   


    void Start()
    {
        // ��Ϸ��ʼʱ����һص���㲢�����¼ģʽ
        playerObject.transform.position = startPoint != null ? startPoint.position : Vector3.zero;
        positionRecorder.isRecording = true; // ������¼ģʽ
        positionReplayer.StopReplay(); // ȷ������ģʽΪ�ر�
        if (cubeColorChanger != null)
        {
            cubeColorChanger.ChangeColor(Color.red); // ��һ�����Ǻ�ɫ
        }
        shadowObject.SetActive(false);  // �������壬ʹ����ȫ���ɼ���ͣ��
    }

    void Update()
    {
        // ������� R �����л�ģʽ
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (RecordingMode)
            {
                // ����ڶ��������ص���㣬ֹͣ��¼�����벥��ģʽ
                shadowObject.SetActive(true);  // �������壬ʹ����ȫ���ɼ���ͣ��
                RecordingMode = false;
                playerObject.transform.position = startPoint != null ? startPoint.position : Vector3.zero;
                positionRecorder.isRecording = false; // ֹͣ��¼
                positionReplayer.StartReplay(); // ��������
                if (cubeColorChanger != null)
                {
                    cubeColorChanger.ChangeColor(Color.blue); // ��һ�����Ǻ�ɫ
                }
            }
            else
            {
                // ����ǵڶ����������ûص���¼ģʽ������ռ�¼
                RecordingMode = true;
                positionReplayer.StopReplay(); // ֹͣ����
                positionRecorder.recordedActions = new PlayerAction[10000]; // ��ռ�¼
                playerObject.transform.position = startPoint != null ? startPoint.position : Vector3.zero;
                positionRecorder.StartRecording();// ������¼ģʽ
                if (cubeColorChanger != null)
                {
                    cubeColorChanger.ChangeColor(Color.red); // ��һ�����Ǻ�ɫ
                }
                shadowObject.SetActive(false);  // �������壬ʹ����ȫ���ɼ���ͣ��
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
        // ��ʱ�洢 obj1 ��λ��
        Vector3 tempPosition = obj1.transform.position;

        // �� obj1 ��λ�ø��� obj2
        obj1.transform.position = obj2.transform.position;

        // �� obj2 ��λ�ø��� obj1
        obj2.transform.position = tempPosition;
    }
}
