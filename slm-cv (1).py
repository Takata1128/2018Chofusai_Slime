import cv2
import socket
from copy import deepcopy
import urllib.request
import json
import numpy as np

cap = cv2.VideoCapture(0)

def mouse_event(event, x, y, flags, param):
	global y_check_value
	if event == cv2.EVENT_LBUTTONUP:
		print("print y : %d" % y)
		y_check_value = y

pframe = None
time_flag = False
y_value = 480
y_check_value = 480

host = "127.0.0.1"
port = 22222

while True:

	ret, frame = cap.read()

	# y:max = 480
	frame = frame[0:y_value , 0:640]

	if pframe is None:
		pframe = np.zeros(frame.shape, dtype=np.uint8)

	if not ret:
		print('failed')
		time.sleep(0.01)
		continue

	cv2.imshow('frame', frame)
	cv2.setMouseCallback('frame', mouse_event)
	frame = cv2.cvtColor(frame, cv2.COLOR_BGR2GRAY)

	# ココの 180 ~ 255 はある程度調整しておく必要性がある
	_, _bin = cv2.threshold(frame, 180, 255, cv2.THRESH_BINARY_INV | cv2.THRESH_OTSU)

	ksize = 7
	_bin = cv2.medianBlur(_bin, ksize)

	cv2.imshow('bin', _bin)

	_, contours, _ = cv2.findContours(_bin, cv2.RETR_EXTERNAL, cv2.CHAIN_APPROX_SIMPLE)

	bollon_index = 0
	bollon_area = 0

	# print("contours length : %d" % len(contours))

	for i in range(len(contours)):
		area = cv2.contourArea(contours[i])
		if area < 1e2 or 1e5 < area:
			continue

		if bollon_area < area:
			bollon_index = i

		cv2.drawContours(frame, contours, i, (255, ))

	bollon_list = deepcopy(contours[bollon_index])
	bollon_list = np.reshape(bollon_list, (2, int(bollon_list.size / 2)), order="F")

	bollon_list = [bollon_list.max(axis=1), bollon_list.min(axis=1)]
	bollon_redius = [(bollon_list[0][0] - bollon_list[1][0])/2, (bollon_list[0][1] - bollon_list[1][1])]
	# print("redius : %f : %f" % (bollon_redius[0], bollon_redius[1]))

	cv2.imshow('raw', frame)

	with socket.socket(socket.AF_INET, socket.SOCK_DGRAM) as s:
		s.sendto(str(bollon_redius[0]).encode(), (host, port))

	key = cv2.waitKey(1)
	if key & 0xFF == ord('q'):
		break
	elif key & 0xFF == ord('c'):
		pframe = frame
	elif key & 0xFF == ord('i'):
		y_value = y_check_value
		print("update y_value : %d" % y_value)
