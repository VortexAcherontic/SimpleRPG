class Shift
{
	/*A Shift is a period of time where certain
	settings are used. By setting up multiple 
	Shifts, you can turn off the Sun when it dips
	under the horizon, or change the color of the 
	clouds from an errie white at night to a more
	realistic dark. Make sure to create shifts in
	chronological order, or else the initialization
	may not work as expected!
	*/
	private var subject : UnityEngine.GameObject;
	var targetAlpha = 0.0;
	var targetR = 0.0;
	var targetG = 0.0;
	var targetB = 0.0;
	var startHour = 0;
	var isThisALight = true;
	
	function setSubject(remoteSubject)
	{
		subject = remoteSubject;
	}
	
	function shiftChange(hourLength)
	{
		if (isThisALight)
			iTween.lightTo(subject,{"r" : targetR, "g" : targetG, "b" : targetB, "time" : (hourLength / 2)});
		else{
			iTween.fadeTo(subject,{"alpha" : targetAlpha, "time" : (hourLength / 2)});
			iTween.colorTo(subject,{"r" : targetR, "g" : targetG, "b" : targetB, "time" : (hourLength / 2)});
			}

	}
	
	
	
}

class CloudShift
{
	private var lightClouds : UnityEngine.GameObject;
	private var heavyClouds : UnityEngine.GameObject;
	var targetR = 0.0;
	var targetG = 0.0;
	var targetB = 0.0;
	var startHour = 0;

	function cloudSet(light, heavy)
	{
		lightClouds = light;
		heavyClouds = heavy;
	}

	function shiftChange(hourLength)
	{
		iTween.colorTo(lightClouds,{"r" : targetR, "g" : targetG, "b" : targetB, "time" : (hourLength * 1.25)});
		iTween.colorTo(heavyClouds,{"r" : targetR, "g" : targetG, "b" : targetB, "time" : (hourLength * 1.25)});
	}
}


class WeatherType 
{
	private var lightClouds : UnityEngine.GameObject;
	var lightCloudsAlpha = 0.0;
	private var heavyClouds : UnityEngine.GameObject;
	var heavyCloudsAlpha = 0.0;
	private var sun : UnityEngine.GameObject;
	var sunIntensity = 0.0;
	private var rain : UnityEngine.GameObject;
	var rainAlpha = 0.0;
	private var lightning : UnityEngine.GameObject;
	var lightningIntensity = 0.0;
	
	function setSubjects(remoteSun,remoteLight,remoteHeavy,remoteRain,remoteLightning)
	{
		lightClouds = remoteLight;
		heavyClouds = remoteHeavy;
		sun = remoteSun;
		rain = remoteRain;
		lightning = remoteLightning;
		
	}
	
	function weatherChange(hourlength)
	{
		iTween.fadeTo(lightClouds,{"alpha" : lightCloudsAlpha, "time" : hourlength});
		iTween.fadeTo(heavyClouds,{"alpha" : heavyCloudsAlpha, "time" : hourlength});
		iTween.fadeTo(rain,{"alpha" : rainAlpha, "time" : hourlength});
		iTween.intensityTo(sun,{"intensity" : sunIntensity, "time" : hourlength});
		iTween.intensityTo(lightning,{"intensity" : lightningIntensity, "time" : hourlength});
	}
	
	


}


function Update () {}