#include "saveLoad.h"
#include <fstream>


SaveLoad::SaveLoad() {}

void SaveLoad::saveObjects(char* name, float posX, float posY, float posZ, int questAt) {
	GameObject temp = GameObject{ name, posX, posY, posZ };
	objectHolder.insert(objectHolder.begin(), temp);
    questIsAt = questAt;
}

void SaveLoad::saveObjectsToFile(char* filePath) {
    std::string theFilePath = filePath;
    
    std::ofstream file;
    file.open(theFilePath, std::ofstream::out | std::ofstream::trunc);
    file << questIsAt;
    while (!objectHolder.empty()) {
        file << objectHolder[objectHolder.size() - 1].name;
        file << "\n";
        file << objectHolder[objectHolder.size() - 1].x;
        file << "\n";
        file << objectHolder[objectHolder.size() - 1].y;
        file << "\n";
        file << objectHolder[objectHolder.size() - 1].z;
        file << "\n";
        objectHolder.pop_back();
    }
    file.close();
}


void SaveLoad::loadObjectData(char* filePath)
{
    int count = 0;
    bool firstLine = false;
    std::string theFilePath = filePath;

    GameForSendingToUnityObject objectLoaded;

    std::ifstream files;
    files.open(filePath);
    std::string workd;
    while (std::getline(files, workd)) {
        switch (firstLine) {
        case true:
            firstLine = false;
            break;
        case false:
            switch (count) {
            case 0:
                objectNames.insert(objectNames.begin(), workd);
                break;
            case 1:
                objectLoaded.x = std::stof(workd);
                break;
            case 2:
                objectLoaded.y = std::stof(workd);
                break;
            case 3:
                objectLoaded.z = std::stof(workd);
                break;
            }
            count++;
            if (count > 3) {
                count = 0;
                objectsToSendBack.insert(objectsToSendBack.begin(), objectLoaded);
            }
            break;
        }
    }
    for (int i = 0; i < objectNames.size(); i++) {
        objectsToSendBack[i].name = const_cast<char*>(objectNames[i].c_str());
    }
    files.close();
}

GameForSendingToUnityObject SaveLoad::getLoadedObjects()
{
    return objectsToSendBack[objectsToSendBack.size() - 1];
}

int SaveLoad::popObject() {
    objectsToSendBack.pop_back();
    return objectsToSendBack.size();
}

int SaveLoad::getQuestAt()
{
    return questIsAt;
}
