# AR Final Project
Physics Euducational App Created for AR Class Final Project
-By Jingguo Liang and Yujin Park

In this project, we created a physics educational app that tracks at most 2 markers to display at most 2 virtual particles (shown as spheres).
For now, the app is only avialable through Unity's play mode.

We used Unity to develop this app, and used the following packages:
- Vugoria Engine AR
- BezierSolution developed by Yasirukla (https://github.com/yasirkula/UnityBezierSolution)


The scene we created for this project is located under Assets/Scenes/DoubleMarkerTracking.

The 2 markers used for object tracking in this project can be found in the Markers folder in the root of this project.

The majority of the code we wrote is in Controller.cs script which is a component of the Controller game object. (Located in Assets/Scripts/Controller.cs)
Here are the main parts of the Controller.cs script:
- Public functions to keep track of which markers are detected (hence which objects are being tracked) at a given time.
- For each object, starting points of field lines are sampled throughout the surface of the sphere using Euler Angles.
- For each starting point, a Bezier Spline with 30 control points are constructed.
> - The number of points (hence number of field lines shown for each object) can be changed by changing the value of const int numPoints in controller.cs
- The control points are updated at every frame with the unit direction vector of the net electric field at that point. This allows us to show the filed line behavior/interaction in real time. 
> - You can change how far apart each point in the bezier spline is by change the value of const float stepLength in controller.cs
- Tangent lines at these control points are constructed automatically by using BezierSolution's built-in functions.



