using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Cursor = UnityEngine.Cursor;

public class FactoryController : MonoBehaviour
{
    public GameObject[] workers;
    public float difficultyProgressRate = 0.25f;
    public float productionRate = 100;
    public float activeWorkers = 0;
    public PowerBarController powerBar;
    private readonly List<WorkerController> _workers = new();
    
    void Start()
    {
        Cursor.visible = false;
        foreach (GameObject worker in workers)
        {
            _workers.Add(worker.gameObject.GetComponentInChildren<WorkerController>());
            print(_workers.Count);
        }
    }

    void Update()
    {
        activeWorkers = _workers.Sum(worker =>
        {
            return worker.status switch
            {
                WorkerController.Status.Awake => 1f,
                WorkerController.Status.Exhausted => 0.5f,
                _ => 0
            };
        });
        productionRate = 100 / (float) workers.Length * activeWorkers;
        powerBar.setPower(productionRate);
    }
}
