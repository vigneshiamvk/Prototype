 var playerObject : GameObject;
 var canClimb = false;
 var speed : float = 1;
 
 function Start () {
     playerObject = gameObject.Find("Ninja");
 }
 
 function OnCollisionEnter (coll : Collision){
     if(coll.gameObject == playerObject){
         canClimb = true;
     }
 }
 
 function OnCollisionExit (coll2 : Collision){
     if(coll2.gameObject == playerObject){
         canClimb = false;
     }
 }
 function Update () {
     if(canClimb){
         if(Input.GetKey(KeyCode.Y)){
             playerObject.transform.Translate (Vector3(0,1,0) * Time.deltaTime*speed);
         }
         if(Input.GetKey(KeyCode.R)){
             playerObject.transform.Translate (Vector3(0,-1,0) * Time.deltaTime*speed);
         }
     }
}