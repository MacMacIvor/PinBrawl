#pragma once
#ifndef  __WRAPPER__
#define __WRAPPER__

#include "PluginSettings.h"
#include "saveLoad.h"

#ifdef __cplusplus

extern "C"
{

#endif // 
	//PLUGIN_API int GetID();
	//
	//PLUGIN_API void SetID(int id);
	//
	//PLUGIN_API Vector2D GetPosition();
	//
	//PLUGIN_API void SetPosition(float x, float y);

	PLUGIN_API void saveObjects(char* name, float posX, float posY, float posZ);
	PLUGIN_API void saveObjectsToFile(char* filePath);
	PLUGIN_API void loadObjectData(char* filePath);
	PLUGIN_API GameForSendingToUnityObject getLoadedObjects();
	PLUGIN_API int popObject();

#ifdef  __cplusplus
}

#endif //  _cplusplus


#endif // ! __WRAPPER__