# AR Final Project
Physics Euducational App Created for AR Class Final Project
-By Jingguo Liang and Yujin Park

In this project, we created a physics educational app that tracks at most 2 markers to display at most 2 virtual particles (shown as spheres).
We used Unity to develop this app, and used the following packages:
- Vugoria Engine AR
- BezierSolution developed by Yasirukla (https://github.com/yasirkula/UnityBezierSolution)

The 2 markers used for object tracking in this project can be found under 



markers can be found under reosurces....



The scene we created for this project is located under Assets/Scenes/DoubleMarkerTracking

The meat of the script is locatetd under Controller object in Controller.cs script.

Here we do a few thitngs: functions track of which objectst are being tracked at a given tiem.
For each object, we sample points (called starting points) aroudn thte sruface of hte sphere using euler angle.
Then, for each starting pointt, we have a bezier spline. For each bezier spline, it thas 30 controll points. (Thii number of points can be changed by changing the value of const int numPoints = 30; in controller.cs)

The tatngentt lines are constructed automatically by using Bezier Spline asset's auttoconstructtspline and linear construct functions.


To make the splines orient themselves properly and interact, we call getDirection on each bezier point of a spline at every frame. This gives us tthe uniit direction vecotr of the net electric field of that point, scaled by a factor of 10 to make longer and more visible. 

The steplength variable in the controller.cs defines the increment between a spline point and the point that comes after it - so, if steplength is set to a small variablee, that means we are taking smaller steps between each bezier point, hence better capture of the shape of the field line, but also more computationally intense.
