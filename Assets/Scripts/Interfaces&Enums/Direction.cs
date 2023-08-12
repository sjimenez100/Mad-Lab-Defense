using UnityEngine;

public static class Direction
{
    public enum Directions {Up, Right, Down, Left}
    private static Vector3[] vectors = { Vector3.up, Vector3.right, Vector3.down, Vector3.left};

    public static Vector3 ToVector(Directions direction)
    {
        return vectors[(int)direction];
    }

}

