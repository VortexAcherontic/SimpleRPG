//Set the length of a full day, measured in minutes
var daySpeed = 24.0 ;  
//Tracks what the current hour is
var hourTrack = 12.0; 
//Track the legnth of a single hour in seconds
private var hourLength = 60.0;
//Tell the Chronosphere at which hour to begin
var hourStart = 12; 
private var latestShift = 0;

//Define the parts of the Chronosphere we will manipulate.
var sun : UnityEngine.GameObject;
var moon : UnityEngine.GameObject;
var nightSphere : UnityEngine.GameObject;
var lightClouds : UnityEngine.GameObject;
var heavyClouds : UnityEngine.GameObject;
var rainEffect : UnityEngine.GameObject;
var lightning : UnityEngine.GameObject;
var sunShifts : Shift[];
var moonShifts : Shift[];
var nightSphereShifts : Shift[];
var cloudShifts : CloudShift[];
var forecasts : WeatherType[];

var random = 0.0;
var weather = 0;
var weatherTypes = 0;




function Start ()
{
	initializeHour(); 
	//initializeSubjects();
	solRotation(); 
	weatherMan();
	
}

private function solRotation ()
{
	
	hourLength = (daySpeed * 60) / 24; //determines the legnth in seconds of an in-game hour
	orbitSun(); // orbit the sun around the level
	InvokeRepeating("hourCount", hourLength, hourLength); //tracks the ongoing hour

}

private function initializeHour ()
{
	while(hourStart > 23){
		hourStart = hourStart - 24;
		}
	
	sun.transform.eulerAngles.x = (270 + (hourStart * 15));
	hourTrack = hourStart;
	initializeShift();

}

private function hourCount()
{
		
	if (hourTrack < 23)
		hourTrack = hourTrack + 1;
	else
		hourTrack = 0;
		
	updateShift();

}

private function orbitSun () 
{
	iTween.rotateBy(sun,{"x" : 1 , "y" : 0, "z" : 0, "time" : (daySpeed * 60), "transition" : "linear", "loopType" : "loop"});	
	iTween.rotateBy(nightSphere,{"x" : 1 , "y" : 0, "z" : 0, "time" : (daySpeed * 60), "transition" : "linear", "loopType" : "loop"});	
}

function updateShift()
{
	for (var i=0; i < sunShifts.GetLength(0); i++)
	{
	
		if (sunShifts[i].startHour == hourTrack)
			sunShifts[i].shiftChange(hourLength);
	
	}
	
	for (var j=0; j < nightSphereShifts.GetLength(0); j++)
	{
	
		if (nightSphereShifts[j].startHour == hourTrack)
			nightSphereShifts[j].shiftChange(hourLength*3);
	
	}
	
	for (var k=0; k < moonShifts.GetLength(0); k++)
	{
	
		if (moonShifts[k].startHour == hourTrack)
			moonShifts[k].shiftChange(hourLength);
	
	}
	
	for (var l=0; l < cloudShifts.GetLength(0); l++)
	{
		if (cloudShifts[l].startHour == hourTrack)
			cloudShifts[l].shiftChange(hourLength);
	}
	
}

function initializeShift()
{
	//automatically configure the shifts' subjects into
	//their arrays so they don't need to be manually set
	var y;
	for(y = 0; y < (sunShifts.GetLength(0)); y++) 
		sunShifts[y].setSubject(sun);
	for(y = 0; y < (nightSphereShifts.GetLength(0)); y++)
		nightSphereShifts[y].setSubject(nightSphere);	
	for(y = 0; y < (moonShifts.GetLength(0)); y++) 
		moonShifts[y].setSubject(moon);
	for(y = 0; y < (cloudShifts.GetLength(0)); y++) 
	{
		cloudShifts[y].cloudSet(lightClouds,heavyClouds);
	}
	for(y = 0; y < (forecasts.GetLength(0)); y++) 
	{
		forecasts[y].setSubjects(sun,lightClouds,heavyClouds,rainEffect,lightning);
	}
	
	//bubble sort the shifts to order them
	sortShifts(moonShifts);
	sortShifts(cloudShifts);
	sortShifts(sunShifts);
	sortShifts(nightSphereShifts);
	//iterates through the various lights and spheres to find
	//the latest shifts and initializes the objects to them
	for (var i=0; i < sunShifts.length; i++)
	{
	
		if (sunShifts[i].startHour <= hourStart){
			latestShift = i;
		}
	}
	sunShifts[latestShift].shiftChange(0.05);
	
	for (var j=0; j < nightSphereShifts.length; j++)
	{
	
		if (nightSphereShifts[j].startHour <= hourStart){
			latestShift = j;
		}
	}
	nightSphereShifts[latestShift].shiftChange(0.05);

	for (var k=0; k < moonShifts.length; k++)
	{
	
		if (moonShifts[k].startHour <= hourStart){
			latestShift = k;
		}
	}
	moonShifts[latestShift].shiftChange(0.05);
	
	for (var l=0; l < cloudShifts.GetLength(0); l++)
	{
		if (cloudShifts[l].startHour <= hourStart)
			latestShift = l;
	}
	cloudShifts[latestShift].shiftChange(0.05);

}

function sortShifts(shiftArray)
	{
		var x;
		var y;
		var shiftholder;
		// The Bubble Sort method.
		for(x = 0; x < shiftArray.GetLength(0); x++) {
			for(y = 0; y < (shiftArray.GetLength(0) - 1); y++) {
				if(shiftArray[y].startHour > shiftArray[y+1].startHour) {
					shiftholder = shiftArray[y+1];
					shiftArray[y+1] = shiftArray[y];
					shiftArray[y] = shiftholder;
				}
			}
		}
	}

function weatherMan()
{
	random = Random.Range(0, 24);
	weather = Random.Range(0,weatherTypes-1);
	forecasts[weather].weatherChange(0.05);
	
	weatherTypes = forecasts.GetLength(0);
	while(true)
	{
		random = Random.Range(0, 24);
		weather = Random.Range(0,weatherTypes-1);
		forecasts[weather].weatherChange(hourLength);
        yield WaitForSeconds (random*hourLength);
    } 


}

function Update ()
{
	
	
	
}