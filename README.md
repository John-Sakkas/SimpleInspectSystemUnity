# Examine System

An Simple Examine System like Resident Evil, with a simple inventory. Created with URP  
and FPS Controller.

Every item has a title, description and an icon. All items in Inventory have a Tooltip 
and can be inspect again.
Also some basic reactions when you have all the objects, like change scene 
and open a secret door/panel.

## How its Work!

Inside the __Manager Object__ is the __Player_Interactions__ script that gets the keys
press and call the fuctions like inspect / pickUp / interact.
![](GitImg/Manager.png)

__ExamineSystem__ is a script that control the current examined item. 
One of the main functions in this script is the resize function that take any item 
and resize it to fit the ExaminePoint Collider. Also reset the position if you 
dont pick up the object.
![](GitImg/Examine-FPS.png)

Every item you want to inpsect must have the __InspectSystem__. It's control the 
rotation , the zoomIn/zoomOut and to change the cursor icon.
![](GitImg/Inspect-Item.png)

__PlayerInventory__ uses a simple List<GameObject> to "save" the items. An update the 
canvas of the inventory.
![](GitImg/Inventory.png)

__CursorIcon__ changes the cursor icon as you see different items 
( pickUp / intercation / Examine and Reaction )
![](GitImg/Mouse_cursor.png)

__ChangeSceneReatction__ it's work with 2 different ways.
The first one is by checking if you need items from the inventory 
or if you need some action like an animation to be played or a door to be open.
![](GitImg/ChangeScene.png)

__AnimationReaction__ works similar to __ChangeSceneReaction__ be checking if you have 
the the correct items inside the invetory.

__Player_UIText__ just shows the UI text 
![](GitImg/Ui-info.png)





## 🚀 About Me
My name is John and i'm a junior software engineer...


## 🔗 Links
[![linkedin](https://img.shields.io/badge/linkedin-0A66C2?style=for-the-badge&logo=linkedin&logoColor=white)](https://www.linkedin.com/in/giannis-sakkas-a67587260/)

## Support

For support, email troumjohn@gmail.com .

