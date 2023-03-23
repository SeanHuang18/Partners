using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    
    private const float PIECE_SURFACE_OFFSET = 1f;

    public Controller.Color color;
    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

    public void toTile(Tile t){
        this.transform.position = new Vector3(t.transform.position.x, PIECE_SURFACE_OFFSET, t.transform.position.z);
    }
}
