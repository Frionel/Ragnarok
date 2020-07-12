using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] private MapController _defaultMapController;
    
    private MapController _currentMap;

    void Awake()
    {
        ReplaceCurrentMap(_defaultMapController);
    }
    
    public void WarpTo(MapController mapPrefab, int warpId)
    {
        ReplaceCurrentMap(mapPrefab);
        _currentMap.WarpTo(warpId);
    }

    void ReplaceCurrentMap(MapController mapPrefab)
    {
        Destroy(_currentMap);
        _currentMap = Instantiate(mapPrefab);
        _currentMap.EnteredWarp += WarpTo;
    }
}
