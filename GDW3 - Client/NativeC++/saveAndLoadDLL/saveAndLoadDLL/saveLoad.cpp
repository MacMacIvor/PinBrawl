#include "saveLoad.h"
#include <fstream>


SaveLoad::SaveLoad() {
	//GameObject temp = GameObject{ "", 0,0,0 };
	//object.push_back(temp);
}

void SaveLoad::saveObjects(char* name, float posX, float posY, float posZ) {
	GameObject temp = GameObject{ name, posX, posY, posZ };
	objectHolder.insert(objectHolder.begin(), temp);
	int hellp = 0;
}

void SaveLoad::saveObjectsToFile(char* filePath) {
    std::string theFilePath = filePath;
    
    std::ofstream file;
    file.open(theFilePath, std::ofstream::out | std::ofstream::trunc);
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

void convertToChar(std::string stringy, char* chary) {
    
    //char please[iHateHowIHaveToDoThis] 


    while (!stringy.empty()) {
        ;
    }
}

void SaveLoad::loadObjectData(char* filePath)
{
    int count = 0;
    std::string theFilePath = filePath;

    GameForSendingToUnityObject objectLoaded;

    std::ifstream files;
    files.open(filePath);
    std::string workd;
    while (std::getline(files, workd)) {
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
