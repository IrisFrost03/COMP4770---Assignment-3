using UnityEngine;

public class CleanNearbyTile : MonoBehaviour {
    public void CleanClosestTile() {
        // Find all objects tagged DirtyTile
        GameObject[] dirtyTiles = GameObject.FindGameObjectsWithTag("DirtyTile");

        float closestDistance = Mathf.Infinity;
        GameObject closestTile = null;

        // Find the closest one
        foreach (GameObject tile in dirtyTiles) {
            float distance = Vector3.Distance(transform.position, tile.transform.position);
            if (distance < closestDistance && distance < 2f) // Within 2 units
            {
                closestDistance = distance;
                closestTile = tile;
            }
        }

        // Clean it
        if (closestTile != null) {
            closestTile.name = "Clean_Tile";
            closestTile.tag = "CleanTile";

            MeshRenderer renderer = closestTile.GetComponent<MeshRenderer>();
            if (renderer != null) {
                renderer.material.color = Color.white;
            }
        }
    }
}