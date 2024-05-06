import cv2
from cvzone.PoseModule import PoseDetector
import socket
import mediapipe as mp


# import time


# cap = cv2.VideoCapture('1.mp4')

cap=cv2.VideoCapture(0) # Open Camera
cap.open(0) # Keep Camera Open

# time.sleep(0.5)

sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
serverAddressPort = ("127.0.0.1", 5055)


detector = PoseDetector()
posList = []


lmString = ''
mp_drawing = mp.solutions.drawing_utils

if not (cap.isOpened()):
    print("Could not open video device")
while True:
    success, img = cap.read()
    img = detector.findPose(img)
    lmList, bboxInfo = detector.findPosition(img)
    if bboxInfo:
        lmString = ''
        for lm in lmList:
            print(lm)
            lmString += f'{lm[0]}, {img.shape[0] - lm[1]}, {lm[2]},'
        posList.append(lmString)
        

    # print(posList)


    data = lmString
    sock.sendto(str.encode(str(data)), serverAddressPort)

    cv2.imshow("Image", img)
    key = cv2.waitKey(1)
    if key == ord('s'):
        with open("AnimationFile.txt", "w") as f:
            f.writelines(["%s\n" % item for item in posList])
    if key == 27: # Press Esc to close camera
        break

    

cap.release()
cv2.destroyAllWindows()

'''
if key == ord('q'):
    # exit()
    break
'''

# pip install pyinstaller
# pyinstaller -F MotionCap.py
# pyinstaller --add-data "C:\Users\USER\AppData\Local\Programs\Python\Python310\Lib\site-packages\mediapipe;mediapipe/" MotionCap.py