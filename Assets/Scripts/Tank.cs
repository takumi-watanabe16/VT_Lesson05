using UnityEngine;
using UnityEngine.InputSystem;

//戦車のコントロールクラス
public class Tank : MonoBehaviour
{
    [Header("==Tank Joint==")]
    [SerializeField] private Transform topJoint;//上部のポイントの参照
    [SerializeField] private Transform cannonJoint;//砲身のジョイントの参照

    [Header("==弾丸設定==")]
    [SerializeField] private GameObject bulletPrefabs;
    [SerializeField] private Transform shotPoint;

    private Vector3 topAngles = Vector3.zero;//上部ジョイントの角度
    private Vector3 cannonAngles = Vector3.zero;//砲身のジョイントの角度

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current.wKey.isPressed == true)
        {
            transform.Translate(transform.forward * 5 * Time.deltaTime);
        }
        else if(Keyboard.current.sKey.isPressed == true)
        {
            transform.Translate(transform.forward * -5 * Time.deltaTime);
        }
        else if(Keyboard.current.aKey.isPressed == true)
        {
            transform.Rotate(Vector3.up * -90 * Time.deltaTime);
        }
        else if(Keyboard.current.dKey.isPressed == true)
        {
            transform.Rotate(Vector3.up * 90 * Time.deltaTime);
        }
        //マウスの移動量を取得する
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();
        Debug.Log($"マウスの移動量:{mouseDelta}");
        //角度の増減の計算
        topAngles.y += mouseDelta.x * 0.1f;
        cannonAngles.x -= mouseDelta.y * 0.1f;
        cannonAngles.x = Mathf.Clamp(cannonAngles.x, -10f, 30f);//砲身の角度を制限する

        //各ジョイントに角度を反映する
        topJoint.localEulerAngles = topAngles;
        cannonJoint.localEulerAngles = cannonAngles;
        //弾丸の発射
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            GameObject bullet = Instantiate(bulletPrefabs, shotPoint.position, shotPoint.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(shotPoint.forward * 25f, ForceMode.Impulse);

            Destroy(bullet,80000000f);
        }
    }
    
}
