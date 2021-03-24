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

	void savePlayerInfo(char* filePath, playerInformation theInfo);
	void saveNewPlayerInfo(char* filePath, playerInformation theInfo);
	void loadPlayerInfo(char* filePath);
	playerInformation getPlayerInfo();

private:
	//Hit accuracy ratio, number of ability uses, death times
	std::vector<GameObject> objectHolder;
	std::vector<GameForSendingToUnityObject> objectsToSendBack;
	std::vector<std::string> objectNames;
	int questIsAt = 0;
	playerInformation infoHolder;
};







#endif // !SAVE_LOAD
