using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{
    [SerializeField] GameObject towerPrefab;
    [SerializeField] GameObject towerEmpty;
    [SerializeField] int towerLimit = 5;

    Queue<Tower> myTowers = new Queue<Tower>();
    Tower tower;
    
    public void AddTower(Waypoint waypoint)
    {
        int towerCount = myTowers.Count;

        if (towerCount < towerLimit)
        {
            tower = Instantiate(towerPrefab, waypoint.transform.position, Quaternion.identity).GetComponent<Tower>();
            tower.transform.parent = towerEmpty.transform;
            //waypoint.isPlaceable = false;
            myTowers.Enqueue(tower);

            tower.baseWayPoint = waypoint;
            waypoint.isPlaceable = false;
            
        }
        else
        {
            Tower oldTower = myTowers.Dequeue();

            oldTower.baseWayPoint.isPlaceable = true;

            waypoint.isPlaceable = false;
            oldTower.baseWayPoint = waypoint;

            oldTower.transform.position = waypoint.transform.position;

            myTowers.Enqueue(oldTower);
        }
    }
}
