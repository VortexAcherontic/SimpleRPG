// Scroll main texture based on time

var scrollSpeed = 0.1;
function Update () 
{
    var offset = Time.time * scrollSpeed;
    renderer.material.SetTextureOffset ("_BumpMap", Vector2(offset/-0.7, offset));
    
    renderer.material.SetTextureOffset ("_MainTex", Vector2(offset/1.0, offset));
	//renderer.material.renderQueue = 1000;
}