using System.Collections.Generic;
using UnityEngine;

public class PathBuilder : MonoBehaviour, IEnemyPath
{
    [SerializeField] GameObject[] _pathElements;

    private void Awake()
    {
        for (int i = 0; i < _pathElements.Length; i++)
        {
            if (_pathElements[i].GetComponent<IEnemyPath>() == null)
            {
                throw new MissingComponentException(string.Format("Объект {0} не имеет компонента, реализующего интерфейс IEnemyPath", _pathElements[i]));
            }
        }        
    }


    public Vector3[] GetPath()
    {
        List<Vector3[]> paths = new List<Vector3[]>();        

        int totalWaypointsAmount = 0;

        for (int i = 0; i < _pathElements.Length; i++)
        {
            paths.Add(_pathElements[i].GetComponent<IEnemyPath>().GetPath());
            totalWaypointsAmount += paths[i].Length;
        }
        
        totalWaypointsAmount -= _pathElements.Length - 1;
        Vector3[] finalPath = new Vector3[totalWaypointsAmount];


        int finalPathPointIndex = 0;

        for (int i = 0; i < paths.Count; i++)
        {
            //Length - 1, чтобы последняя точка предыдущего элемента
            //не совпадала с первой точкой следующего
            for (int j = 0; j < paths[i].Length - 1; j++)
            {
                finalPath[finalPathPointIndex++] = (paths[i][j]);
            }
        }

        //Добавляем последнюю точку
        int finalPointIndex = paths[paths.Count - 1].Length - 1;
        finalPath[finalPathPointIndex] = paths[paths.Count - 1][finalPointIndex];

        return finalPath;
    }


}
