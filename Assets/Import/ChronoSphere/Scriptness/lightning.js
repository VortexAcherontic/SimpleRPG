var lightningSphere : UnityEngine.GameObject;

function Start ()
{
	lightning();
}

function lightningFlashes()
{
	iTween.lightTo(gameObject,{"r" : 1, "g" : 1, "b" : 1, "time" : 0.01});
	iTween.lightTo(gameObject, {"r" : 0, "g" : 0, "b" : 0, "time" : 0.2, "delay" : 0.02});
	iTween.fadeTo(lightningSphere,{"alpha" : 0.5, "time" : 0.01});
	iTween.fadeTo(lightningSphere,{"alpha" : 0, "time" : 0.2, "delay" : 0.02});
	
}

function lightning()
{
	var random = 0.0;
	//random = Random.Range(0, 20);
	
	while(true)
	{
		random = Random.Range(0, 20);
		lightningFlashes();
        yield WaitForSeconds (random);
    } 
}

function Update () 
{
	
}