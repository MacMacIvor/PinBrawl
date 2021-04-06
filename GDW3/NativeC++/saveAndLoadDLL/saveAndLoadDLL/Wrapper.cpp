#include "Wrapper.h"
#include "saveLoad.h"

SaveLoad save;


PLUGIN_API void saveObjects(char* name, float posX, float posY, float posZ) {
	save.saveObjects(name, posX, posY, posZ);
}
PLUGIN_API void saveObjectsToFile(char* filePath) {
	save.saveObjectsToFile(filePath);
}
PLUGIN_API void loadObjectData(char* filePath) {
	save.loadObjectData(filePath);
}
PLUGIN_API GameForSendingToUnityObject getLoadedObjects() {
	return save.getLoadedObjects();
}
PLUGIN_API int popObject() {
	return save.popObject();
}