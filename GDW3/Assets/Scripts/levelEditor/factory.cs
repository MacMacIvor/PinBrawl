using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;

public class factory : MonoBehaviour
{
    public abstract class instantiatingObjects
    {
    
        public abstract string objName { get; }
        public abstract void initiateObj();
    }
    
    public class instantiateNormalCube : instantiatingObjects
    {
        [SerializeField]
        private GameObject cube;
    
        [SerializeField]
        Vector3 objectPosition;
        public instantiateNormalCube(Vector3 position, GameObject obj)
        {
            objectPosition = position;
            cube = obj;

        }
        public override string objName => "NormalCube";
    
        public override void initiateObj()
        {
            GameObject cloneOfObject = Instantiate(cube) as GameObject;
            cube.name += UnityEngine.Random.Range(UnityEngine.Random.Range(-1000000.0f, 0.0f), UnityEngine.Random.Range(0.0f, 1000000.0f));
            cube.transform.position = objectPosition;
        }
    }
    public class instantiateBookShelf : instantiatingObjects
    {
        [SerializeField]
        private GameObject BookShelf;

        [SerializeField]
        Vector3 objectPosition;
        public instantiateBookShelf(Vector3 position, GameObject obj)
        {
            objectPosition = position;
            BookShelf = obj;

        }
        public override string objName => "BookShelf";

        public override void initiateObj()
        {
            GameObject cloneOfObject = Instantiate(BookShelf) as GameObject;
            BookShelf.name += UnityEngine.Random.Range(UnityEngine.Random.Range(-1000000.0f, 0.0f), UnityEngine.Random.Range(0.0f, 1000000.0f));
            BookShelf.transform.position = objectPosition;

        }
    }

    public class instantiateBookShelfWide : instantiatingObjects
    {
        [SerializeField]
        private GameObject BookShelfWide;

        [SerializeField]
        Vector3 objectPosition;
        public instantiateBookShelfWide(Vector3 position, GameObject obj)
        {
            objectPosition = position;
            BookShelfWide = obj;

        }
        public override string objName => "BookShelfWide";

        public override void initiateObj()
        {
            GameObject cloneOfObject = Instantiate(BookShelfWide) as GameObject;
            BookShelfWide.name += UnityEngine.Random.Range(UnityEngine.Random.Range(-1000000.0f, 0.0f), UnityEngine.Random.Range(0.0f, 1000000.0f));
            BookShelfWide.transform.position = objectPosition;
        }
        

    }

    public class instantiateDoor : instantiatingObjects
    {
        [SerializeField]
        private GameObject Door;

        [SerializeField]
        Vector3 objectPosition;
        public instantiateDoor(Vector3 position, GameObject obj)
        {
            objectPosition = position;
            Door = obj;

        }
        public override string objName => "Door";

        public override void initiateObj()
        {
            GameObject cloneOfObject = Instantiate(Door) as GameObject;
            Door.name += UnityEngine.Random.Range(UnityEngine.Random.Range(-1000000.0f, 0.0f), UnityEngine.Random.Range(0.0f, 1000000.0f));
            Door.transform.position = objectPosition;
        }
    }
    public class instantiateDoor_Frame : instantiatingObjects
    {
        [SerializeField]
        private GameObject Door_Frame;

        [SerializeField]
        Vector3 objectPosition;
        public instantiateDoor_Frame(Vector3 position, GameObject obj)
        {
            objectPosition = position;
            Door_Frame = obj;

        }
        public override string objName => "Door_Frame";

        public override void initiateObj()
        {
            GameObject cloneOfObject = Instantiate(Door_Frame) as GameObject;
            Door_Frame.name += UnityEngine.Random.Range(UnityEngine.Random.Range(-1000000.0f, 0.0f), UnityEngine.Random.Range(0.0f, 1000000.0f));
            Door_Frame.transform.position = objectPosition;
        }
    }
    public class instantiateFive_Rail : instantiatingObjects
    {
        [SerializeField]
        private GameObject Five_Rail;

        [SerializeField]
        Vector3 objectPosition;
        public instantiateFive_Rail(Vector3 position, GameObject obj)
        {
            objectPosition = position;
            Five_Rail = obj;

        }
        public override string objName => "Five_Rail";

        public override void initiateObj()
        {
            GameObject cloneOfObject = Instantiate(Five_Rail) as GameObject;
            Five_Rail.name += UnityEngine.Random.Range(UnityEngine.Random.Range(-1000000.0f, 0.0f), UnityEngine.Random.Range(0.0f, 1000000.0f));
            Five_Rail.transform.position = objectPosition;
        }
    }
    public class instantiateFour_Rail : instantiatingObjects
    {
        [SerializeField]
        private GameObject Four_Rail;

        [SerializeField]
        Vector3 objectPosition;
        public instantiateFour_Rail(Vector3 position, GameObject obj)
        {
            objectPosition = position;
            Four_Rail = obj;

        }
        public override string objName => "Four_Rail";

        public override void initiateObj()
        {
            GameObject cloneOfObject = Instantiate(Four_Rail) as GameObject;
            Four_Rail.name += UnityEngine.Random.Range(UnityEngine.Random.Range(-1000000.0f, 0.0f), UnityEngine.Random.Range(0.0f, 1000000.0f));
            Four_Rail.transform.position = objectPosition;
        }
    }
    public class instantiateRail_Post : instantiatingObjects
    {
        [SerializeField]
        private GameObject Rail_Post;

        [SerializeField]
        Vector3 objectPosition;
        public instantiateRail_Post(Vector3 position, GameObject obj)
        {
            objectPosition = position;
            Rail_Post = obj;

        }
        public override string objName => "Rail_Post";

        public override void initiateObj()
        {
            GameObject cloneOfObject = Instantiate(Rail_Post) as GameObject;
            Rail_Post.name += UnityEngine.Random.Range(UnityEngine.Random.Range(-1000000.0f, 0.0f), UnityEngine.Random.Range(0.0f, 1000000.0f));
            Rail_Post.transform.position = objectPosition;
        }
    }
    public class instantiateRising_Rail : instantiatingObjects
    {
        [SerializeField]
        private GameObject Rising_Rail;

        [SerializeField]
        Vector3 objectPosition;
        public instantiateRising_Rail(Vector3 position, GameObject obj)
        {
            objectPosition = position;
            Rising_Rail = obj;

        }
        public override string objName => "Rising_Rail";

        public override void initiateObj()
        {
            GameObject cloneOfObject = Instantiate(Rising_Rail) as GameObject;
            Rising_Rail.name += UnityEngine.Random.Range(UnityEngine.Random.Range(-1000000.0f, 0.0f), UnityEngine.Random.Range(0.0f, 1000000.0f));
            Rising_Rail.transform.position = objectPosition;
        }
    }
    public class instantiateRoom_Plane : instantiatingObjects
    {
        [SerializeField]
        private GameObject Room_Plane;

        [SerializeField]
        Vector3 objectPosition;
        public instantiateRoom_Plane(Vector3 position, GameObject obj)
        {
            objectPosition = position;
            Room_Plane = obj;

        }
        public override string objName => "Room_Plane";

        public override void initiateObj()
        {
            GameObject cloneOfObject = Instantiate(Room_Plane) as GameObject;
            Room_Plane.name += UnityEngine.Random.Range(UnityEngine.Random.Range(-1000000.0f, 0.0f), UnityEngine.Random.Range(0.0f, 1000000.0f));
            Room_Plane.transform.position = objectPosition;
        }
    }
    public class instantiateStair_Support : instantiatingObjects
    {
        [SerializeField]
        private GameObject Stair_Support;

        [SerializeField]
        Vector3 objectPosition;
        public instantiateStair_Support(Vector3 position, GameObject obj)
        {
            objectPosition = position;
            Stair_Support = obj;

        }
        public override string objName => "Stair_Support";

        public override void initiateObj()
        {
            GameObject cloneOfObject = Instantiate(Stair_Support) as GameObject;
            Stair_Support.name += UnityEngine.Random.Range(UnityEngine.Random.Range(-1000000.0f, 0.0f), UnityEngine.Random.Range(0.0f, 1000000.0f));
            Stair_Support.transform.position = objectPosition;
        }
    }
    public class instantiateStairs : instantiatingObjects
    {
        [SerializeField]
        private GameObject Stairs;

        [SerializeField]
        Vector3 objectPosition;
        public instantiateStairs(Vector3 position, GameObject obj)
        {
            objectPosition = position;
            Stairs = obj;

        }
        public override string objName => "Stairs";

        public override void initiateObj()
        {
            GameObject cloneOfObject = Instantiate(Stairs) as GameObject;
            Stairs.name += UnityEngine.Random.Range(UnityEngine.Random.Range(-1000000.0f, 0.0f), UnityEngine.Random.Range(0.0f, 1000000.0f));
            Stairs.transform.position = objectPosition;
        }
    }
    public class instantiateStool : instantiatingObjects
    {
        [SerializeField]
        private GameObject Stool;

        [SerializeField]
        Vector3 objectPosition;
        public instantiateStool(Vector3 position, GameObject obj)
        {
            objectPosition = position;
            Stool = obj;

        }
        public override string objName => "Stool";

        public override void initiateObj()
        {
            GameObject cloneOfObject = Instantiate(Stool) as GameObject;
            Stool.name += UnityEngine.Random.Range(UnityEngine.Random.Range(-1000000.0f, 0.0f), UnityEngine.Random.Range(0.0f, 1000000.0f));
            Stool.transform.position = objectPosition;
        }
    }
    public class instantiateTable : instantiatingObjects
    {
        [SerializeField]
        private GameObject Table;

        [SerializeField]
        Vector3 objectPosition;
        public instantiateTable(Vector3 position, GameObject obj)
        {
            objectPosition = position;
            Table = obj;

        }
        public override string objName => "Table";

        public override void initiateObj()
        {
            GameObject cloneOfObject = Instantiate(Table) as GameObject;
            Table.name += UnityEngine.Random.Range(UnityEngine.Random.Range(-1000000.0f, 0.0f), UnityEngine.Random.Range(0.0f, 1000000.0f));
            Table.transform.position = objectPosition;
        }
    }
    public class instantiateTop_Rail : instantiatingObjects
    {
        [SerializeField]
        private GameObject Top_Rail;

        [SerializeField]
        Vector3 objectPosition;
        public instantiateTop_Rail(Vector3 position, GameObject obj)
        {
            objectPosition = position;
            Top_Rail = obj;

        }
        public override string objName => "Top_Rail";

        public override void initiateObj()
        {
            GameObject cloneOfObject = Instantiate(Top_Rail) as GameObject;
            Top_Rail.name += UnityEngine.Random.Range(UnityEngine.Random.Range(-1000000.0f, 0.0f), UnityEngine.Random.Range(0.0f, 1000000.0f));
            Top_Rail.transform.position = objectPosition;
        }
    }
    public class instantiateUpper_Floor : instantiatingObjects
    {
        [SerializeField]
        private GameObject Upper_Floor;

        [SerializeField]
        Vector3 objectPosition;
        public instantiateUpper_Floor(Vector3 position, GameObject obj)
        {
            objectPosition = position;
            Upper_Floor = obj;

        }
        public override string objName => "Upper_Floor";

        public override void initiateObj()
        {
            GameObject cloneOfObject = Instantiate(Upper_Floor) as GameObject;
            Upper_Floor.name += UnityEngine.Random.Range(UnityEngine.Random.Range(-1000000.0f, 0.0f), UnityEngine.Random.Range(0.0f, 1000000.0f));
            Upper_Floor.transform.position = objectPosition;
        }
    }
    public class instantiateEnemyShooter : instantiatingObjects
    {
        [SerializeField]
        private GameObject EnemyShooter;

        [SerializeField]
        Vector3 objectPosition;
        public instantiateEnemyShooter(Vector3 position, GameObject obj)
        {
            objectPosition = position;
            EnemyShooter = obj;

        }
        public override string objName => "EnemyShooter";

        public override void initiateObj()
        {
            GameObject cloneOfObject = Instantiate(EnemyShooter) as GameObject;
            EnemyShooter.name += UnityEngine.Random.Range(UnityEngine.Random.Range(-1000000.0f, 0.0f), UnityEngine.Random.Range(0.0f, 1000000.0f));
            EnemyShooter.transform.position = objectPosition;
        }
    }
    public class instantiateMainCharacter : instantiatingObjects
    {
        [SerializeField]
        private GameObject MainCharacter;

        [SerializeField]
        Vector3 objectPosition;
        public instantiateMainCharacter(Vector3 position, GameObject obj)
        {
            objectPosition = position;
            MainCharacter = obj;

        }
        public override string objName => "Player";

        public override void initiateObj()
        {
            GameObject cloneOfObject = Instantiate(MainCharacter) as GameObject;
            MainCharacter.name += UnityEngine.Random.Range(UnityEngine.Random.Range(-1000000.0f, 0.0f), UnityEngine.Random.Range(0.0f, 1000000.0f));
            MainCharacter.transform.position = objectPosition;
        }
    }
    public class instantiateFire_Place : instantiatingObjects
    {
        [SerializeField]
        private GameObject Fire_Place;

        [SerializeField]
        Vector3 objectPosition;
        public instantiateFire_Place(Vector3 position, GameObject obj)
        {
            objectPosition = position;
            Fire_Place = obj;

        }
        public override string objName => "Fire_Place";

        public override void initiateObj()
        {
            GameObject cloneOfObject = Instantiate(Fire_Place) as GameObject;
            Fire_Place.name += UnityEngine.Random.Range(UnityEngine.Random.Range(-1000000.0f, 0.0f), UnityEngine.Random.Range(0.0f, 1000000.0f));
            Fire_Place.transform.position = objectPosition;
        }
    }


    public class instantiateFactory
    {
        public instantiatingObjects spawn(string name, Vector3 position, GameObject obj)
        {
            switch (name)
            {
                case "NormalCube":
                    return new instantiateNormalCube(position, obj);
                case "BookShelf":
                    return new instantiateBookShelf(position, obj);
                case "BookShelfWide":
                    return new instantiateBookShelfWide(position, obj);
                case "Door":
                    return new instantiateBookShelfWide(position, obj);
                case "Door_Frame":
                    return new instantiateBookShelfWide(position, obj);
                case "Five_Rail":
                    return new instantiateBookShelfWide(position, obj);
                case "Rail_Post":
                    return new instantiateBookShelfWide(position, obj);
                case "Rising_Rail":
                    return new instantiateBookShelfWide(position, obj);
                case "Room_Plane":
                    return new instantiateBookShelfWide(position, obj);
                case "Stairs":
                    return new instantiateBookShelfWide(position, obj);
                case "Stool":
                    return new instantiateBookShelfWide(position, obj);
                case "Table":
                    return new instantiateBookShelfWide(position, obj);
                case "Top_Rail":
                    return new instantiateBookShelfWide(position, obj);
                case "Upper_Floor":
                    return new instantiateBookShelfWide(position, obj);
                case "EnemyShooter":
                    return new instantiateBookShelfWide(position, obj);
                case "Player":
                    return new instantiateBookShelfWide(position, obj);
                case "Fire_Place":
                    return new instantiateBookShelfWide(position, obj);
                default:
                    return null;
            }
        }
    
    
    }

    public void spawn(string objName, Vector3 objPosition, GameObject obj)
    {
        instantiateFactory hello = new instantiateFactory();
        hello.spawn(objName, objPosition, obj).initiateObj();
    }
    //Couldn't get this to work so idk
    //If someone understands how it works let me know so I can try



    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
