#pragma once
#ifndef __GAMEOBJECT__
#define __GAMEOBJECT__

#include <string>

struct GameObject {
	std::string name;
	float x;
	float y;
	float z;
};

struct GameForSendingToUnityObject {
	char* name;
	float x;
	float y;
	float z;
};



#endif