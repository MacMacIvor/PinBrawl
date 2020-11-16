#pragma once
#ifndef __SAVELOAD__
#define __SAVELOAD__
#include "PluginSettings.h"
#include "GameObject.h"
#include <vector>


class PLUGIN_API SaveLoad {
public:
	SaveLoad();

	void saveObjects(char* name, float posX, float posY, float posZ, int questAt);
	void saveObjectsToFile(char* filePath);
	void loadObjectData(char* filePath);
	GameForSendingToUnityObject getLoadedObjects();
	int popObject();
	int getQuestAt();
private:
	std::vector<GameObject> objectHolder;
	std::vector<GameForSendingToUnityObject> objectsToSendBack;
	std::vector<std::string> objectNames;
	int questIsAt = 0;
};







#endif // !SAVE_LOAD
