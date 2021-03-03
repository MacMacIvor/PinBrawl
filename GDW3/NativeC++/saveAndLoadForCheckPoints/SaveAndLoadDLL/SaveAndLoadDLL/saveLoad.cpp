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
    file << "\n";
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
    bool firstLine = true;
    std::string theFilePath = filePath;

    GameForSendingToUnityObject objectLoaded;

    std::ifstream files;
    files.open(filePath);
    std::string workd;
    while (std::getline(files, workd)) {
        switch (firstLine) {
        case true:
            questIsAt = std::stof(workd);
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

void SaveLoad::saveNewPlayerInfo(char* filePath, playerInformation theInfo)
{
    //Use this to replace the current info for a fresh start
    infoHolder = theInfo;

    std::string theFilePath = filePath;
    std::ofstream file;
    file.open(theFilePath, std::ofstream::out | std::ofstream::trunc);
    //hitAccuracy, numberOfChargedAttacks, numberOfTimesHit, numberOfKills, healthHealed, numberOfDeaths;
    file << infoHolder.hitAccuracy;
    file << "\n";
    file << infoHolder.numberOfChargedAttacks;
    file << "\n";
    file << infoHolder.numberOfTimesHit;
    file << "\n";
    file << infoHolder.numberOfKills;
    file << "\n";
    file << infoHolder.healthHealed;
    file << "\n";
    file << infoHolder.numberOfDeaths;

    file.close();
}

void SaveLoad::savePlayerInfo(char* filePath, playerInformation theInfo)
{
    infoHolder.healthHealed += theInfo.healthHealed;
    infoHolder.numberOfChargedAttacks += theInfo.numberOfChargedAttacks;
    infoHolder.numberOfDeaths += theInfo.numberOfDeaths;
    infoHolder.numberOfKills += theInfo.numberOfKills;
    infoHolder.numberOfTimesHit += theInfo.numberOfTimesHit;
    infoHolder.hitAccuracy = infoHolder.numberOfTimesHit / infoHolder.numberOfChargedAttacks;

    std::string theFilePath = filePath;
    std::ofstream file;
    file.open(theFilePath, std::ofstream::out | std::ofstream::trunc);
    //hitAccuracy, numberOfChargedAttacks, numberOfTimesHit, numberOfKills, healthHealed, numberOfDeaths;
    file << infoHolder.hitAccuracy;
    file << "\n";
    file << infoHolder.numberOfChargedAttacks;
    file << "\n";
    file << infoHolder.numberOfTimesHit;
    file << "\n";
    file << infoHolder.numberOfKills;
    file << "\n";
    file << infoHolder.healthHealed;
    file << "\n";
    file << infoHolder.numberOfDeaths;

    file.close();
}

void SaveLoad::loadPlayerInfo(char* filePath) {
    int count = 0;
    
    std::ifstream files;
    files.open(filePath);
    std::string workd;

    while (std::getline(files, workd)) {
        
            switch (count) {
            case 0:
                infoHolder.hitAccuracy = std::stof(workd);
                break;
            case 1:
                infoHolder.numberOfChargedAttacks = std::stof(workd);
                break;
            case 2:
                infoHolder.numberOfTimesHit = std::stof(workd);
                break;
            case 3:
                infoHolder.numberOfKills = std::stof(workd);
                break;
            case 4:
                infoHolder.healthHealed = std::stof(workd);
                break;
            case 5:
                infoHolder.numberOfDeaths = std::stof(workd);
                break;
            }
            count++;            
    }
    files.close();
}


playerInformation SaveLoad::getPlayerInfo()
{
    return infoHolder;
}
