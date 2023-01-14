
# Examine System

An Simple Examine System like Resident Evil, with a simple inventory. Created with URP  
and FPS Controller.

Every item has a title, description and an icon. All items in Inventory have a Tooltip 
and can be inspect again.
Also some basic reaction when you have all the object, like change scene 
and open a secret door/panel.
## How its Work!

Inside the __Manager Object__ is the __Player_Interactions__ script that gets the keys
press and call the fuctions like inspect / pickUp / interact.
![App Screenshot](https://via.placeholder.com/468x300?text=App+Screenshot+Here)

__ExamineSystem__ is a script that control the current examined item. 
One of the main functions in this script is the resize function that take any item 
and resize it to fit the ExaminePoint Collider. Also reset the position if you 
dont pick up the object.
![App Screenshot](https://via.placeholder.com/468x300?text=App+Screenshot+Here)

Every item you want to inpsect must have the __InspectSystem__. It's control the 
rotation , the zoomIn/zoomOut and to change the cursor icon.
![App Screenshot](https://via.placeholder.com/468x300?text=App+Screenshot+Here)

__PlayerInventory__ uses a simple List<GameObject> to "save" the items. An update the 
canvas of the inventory.
![App Screenshot](https://via.placeholder.com/468x300?text=App+Screenshot+Here)

__CursorIcon__ changes the cursor icon as you see different items 
( pickUp / intercation / Examine and Reaction )
![App Screenshot](https://via.placeholder.com/468x300?text=App+Screenshot+Here)

__ChangeSceneReatction__ it's work with 2 different ways.
The first one is by checking if you need items from the inventory 
or if you need some action like an animation to be played or a door to be open.
![App Screenshot](https://via.placeholder.com/468x300?text=App+Screenshot+Here)

__AnimationReaction__ works be checking if you have the the correct items inside 
the invetory.
![App Screenshot](https://via.placeholder.com/468x300?text=App+Screenshot+Here)

__Player_UIText__ just shows the UI text 
![App Screenshot](https://via.placeholder.com/468x300?text=App+Screenshot+Here)





## 🚀 About Me
I'm a junior software enginer...


## 🔗 Links
[![portfolio](https://img.shields.io/badge/my_portfolio-000?style=for-the-badge&logo=ko-fi&logoColor=white)](https://katherineoelsner.com/)
[![linkedin](https://img.shields.io/badge/linkedin-0A66C2?style=for-the-badge&logo=linkedin&logoColor=white)](https://www.linkedin.com/in/giannis-sakkas-a67587260/)
[![facebook](https://img.shields.io/badge/facebook-0000FF?style=for-the-badge&logo=facebook&logoColor=white)](https://www.facebook.com/giannis.sakkas.5)


## Support

For support, email troumjohn@gmail.com .

