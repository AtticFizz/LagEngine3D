# LagEngine3D
A very laggy 3D engine. At first, the idea was to create a 4D cube visualization, but later the project evolved into a 3D engine (you may still find some references to 4D in the code).

![Screenshot_1](https://github.com/AtticFizz/LagEngine3D/assets/66317048/b8712b4f-ae8a-448a-b290-91ed6d2d7e26)

![Screenshot_2](https://github.com/AtticFizz/LagEngine3D/assets/66317048/27ac09d6-c5ea-4651-a0fe-ec2168c53138)

![Screenshot_3](https://github.com/AtticFizz/LagEngine3D/assets/66317048/345f8556-e391-4201-b48f-0debd8387b3d)

# How To
  ## Custom meshes
  1. Export mesh as .stl in ASCII format.
  2. Add mesh to the Meshes directory (or any other)
  3. Adjust LagEngine3D/View.cs lines 11 and 12 to load the mesh
     ```
     private static string _file = "Donut.stl";
     private static string _filepath = Path.Combine(Directory.GetCurrentDirectory(), "Meshes", _file);
     ```
  4. In the same file in OnStart method adjust mesh rotation and position.
     ```
     _simpleMesh.Rotate(Math.PI, 0.0, 0.0);
     _simpleMesh.SetPosition(new Vector3(0.0, 0.5, 1.25));
     ```
  5. If you want you can disable OnUpdate method to remove rotation of the mesh, you can also add other mesh manipulations.
  6. You can also add more objects to the scene.
     ```
     Renderer.ObjectsToRender.Add(_simpleMesh);
     ```

  ## Rendering Options
  1. Can be found in Rendering/RenderOptions.cs
  2. MOST of the options work and can be changed.
     
     ![Screenshot_4](https://github.com/AtticFizz/LagEngine3D/assets/66317048/d07b4157-3579-4359-8d85-2fa36e6b4548)


# Known Issues & Ideas For Future
  - [ ] GPU usage - right now everything is computed on the CPU, GPU computations could help with the framerate if memory copying won't bottleneck.
  - [ ] Bitmap might not be a good option for image display, should look up other options - you can notice how much worse the framerate gets if the view window is bigger, and how much better the framerate hets if view is shrunk.
  - [ ] General refactoring - a lot of functionality is not used at all, some code is barely readable.
  - [ ] Addition of model textures.
  - [ ] Camera control using mouse & keyboard.
  - [ ] Mesh clipping with the camera - crashes are possible if the camera is placed inside of a mesh.
  - [ ] Cast shadows - only self-shadowing right now is possible.
  - [ ] Phong / smooth shading - you can find attempts of this shading in the code, but none of the approaches worked so far.
  - [ ] Could add render pipelines to get more wonky effects.
  
