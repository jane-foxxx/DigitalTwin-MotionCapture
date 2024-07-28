# DigitalTwin-MotionCapture
 A VR Motion Capture System for Third-Person  Perspectives in CAVE

Brief Guideline of DigitalTwin_MoitonCapture Project
1.	Software Used
   * Python 3.11
   * Unity 2020.3.21f1
   * VotanicXR 2020SDK
3.	Python Environment Setting for Mocap
   * Python File is used to open camera and capture frame image of video.
   * Utilize python package such as OpenCV/Mediapipe to capture and identify human pose information, then transfer pose data to unity through UDP.
![image](https://github.com/user-attachments/assets/376de5d4-6707-4fa2-9f42-c72cd3e441da)

 
2.1.	Python File
Download mediapipe, cvzone, opencv-python, opencv-contrib-python in python
 ![image](https://github.com/user-attachments/assets/c9ba7d91-f7f0-43ed-8bf0-d67e2e7d2f51)

2.2.	Executable File
To build an executable file that opens the camera and runs on any computer, regardless of whether it has a Python environment, follow these steps:
1.	Install PyInstaller: Use the command `pip install pyinstaller`.
2.	Prepare your Python script: Ensure your script (e.g., `MotionCap.py`) is ready.
3.	Generate the executable: Run `pyinstaller -F MotionCap.py` in the terminal.
4.	Locate the executable: Find the generated `.exe` file in the `dist` directory.
This process can create an executable file that can be run on any computer without Python installation.
![image](https://github.com/user-attachments/assets/3ff0465c-4440-4abc-aa46-e6ee82b8dc79)

** Remember to change python file/name to your own work
 ![image](https://github.com/user-attachments/assets/ec010be2-bd04-4280-80a2-6e1f11646c80)

 
3.	Unity
•	Based on exiting unity file, directly run/build file would be fine
 ![image](https://github.com/user-attachments/assets/df89f9fc-7e66-4200-b064-69f3fb295172)

