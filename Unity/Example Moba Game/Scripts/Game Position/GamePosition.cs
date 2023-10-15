using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GamePosition : MonoBehaviour
{
    public static List<GamePosition> gamePositions;

    public enum Type { TeamSpawn, MinionSpawn }

    public enum Team { Blue, Red, Neutral }

    public Type type;
    public Team team;

    private void Awake()
    {
        if(gamePositions == null) gamePositions = new List<GamePosition>();
        gamePositions.Add(this);
    }

    private void OnValidate() => gameObject.name = team.ToString() + " " + type.ToString();

    private void OnDrawGizmos()
    {
        Color color;
        switch(team)
        {
            case Team.Blue: color = Color.cyan; break;
            case Team.Red: color = Color.red; break;
            default: color = Color.yellow; break;
        }
        Gizmos.color = color;
        switch (type)
        {
            case Type.TeamSpawn: Gizmos.DrawWireCube(transform.position, new Vector3(8f, 2f, 8f)); break;
            case Type.MinionSpawn: Gizmos.DrawWireSphere(transform.position, 2f); break;
            default: Gizmos.DrawWireCube(transform.position, new Vector3(2f, 2f, 2f)); break;
        }
        #if UNITY_EDITOR
        Handles.Label(transform.position, team.ToString() + " " + type.ToString(), new GUIStyle()
        {
            normal = new GUIStyleState() { textColor = color },
            alignment = TextAnchor.MiddleCenter,
            fontStyle = FontStyle.Bold
        });
        #endif
    }

    public static Vector3 GetRandomPosition(Type type, Team team)
    {
        foreach(GamePosition gamePosition in gamePositions)
            if(gamePosition.type == type && gamePosition.team == team)
                return new Vector3(gamePosition.transform.position.x + Random.Range(-1, 1), gamePosition.transform.position.y - 0.2f, gamePosition.transform.position.z + Random.Range(-1, 1));
        return Vector3.zero;
    }
}
