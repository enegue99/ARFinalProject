# AR Final Project
Physics Euducational App Created for AR Class Final Project
-By Jingguo Liang and Yujin Park

In this project, we created a physics educational app that tracks at most 2 markers to display at most 2 virtual particles (shown as spheres).
We used Unity to develop this app, and used the following packages:
- Vugoria Engine AR
- BezierSolution developed by Yasirukla (https://github.com/yasirkula/UnityBezierSolution)


The scene we created for this project is located under Assets/Scenes/DoubleMarkerTracking.

The 2 markers used for object tracking in this project can be found in the Markers folder in the root of this project.

The majority of the code we wrote is in Controller.cs script which is a component of the Controller game object.
Here are the main parts of the Controller.cs script:
- Public functions to keep track of which markers are detected (hence which objects are being tracked) at a given time.
- For each object, starting points of field lines are sampled throughout the surface of the sphere using Euler Angles.
- For each starting point, a Bezier Spline with 30 control points are constructed.
> - The number of points (hence number of field lines shown for each object) can be changed by changing the value of const int numPoints in controller.cs
- The control points are updated at every frame with the unit direction vector of the net electric field at that point. This allows us to show the filed line behavior/interaction in real time. 
> - You can change how far apart each point in the bezier splin is by change the value of 
- Tangent lines at these control points are constructed automatically by using BezierSolution's built-in functions.
- 
- 
- we do a few thitngs: functions track of which objectst are being tracked at a given tiem.
For each object, we sample points (called starting points) aroudn thte sface of hte sphere using euler angle.
Then, for each starting pointt, we have a bezier spline. For each bezier spline, it thas 30 controll points. (Thii number of points can be changed by changing the value of const int numPoints = 30; in controller.cs)

The tatngentt lines are constructed automatically by using Bezier Spline asset's auttoconstructtspline and linear construct functions.


To make the splines orient themselves properly and interact, we call getDirection on each bezier point of a spline at every frame. This gives us tthe uniit direction vecotr of the net electric field of that point, scaled by a factor of 10 to make longer and more visible. 

The steplength variable in the controller.cs defines the increment between a spline point and the point that comes after it - so, if steplength is set to a small variablee, that means we are taking smaller steps between each bezier point, hence better capture of the shape of the field line, but also more computationally intense.
