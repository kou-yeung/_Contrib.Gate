// 検証用
using UnityEngine;
using Entity;

public class Ball : MonoBehaviour
{
    [SerializeField] string Id;
    public IdWithType idWithType { get; set; }

    void Awake()
    {
        IdWithType res;
        IdWithType.TryParse(Id, out res);
        idWithType = res;
    }

	void Update ()
    {
	}
}
