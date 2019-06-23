import cv2
import socket
from copy import deepcopy
import urllib.request
import json
import numpy as np

cap = cv2.VideoCapture(1)

pframe = None
time_flag = False

x_right_value = 640
x_left_value = 0
y_up_value = 0
y_down_value = 480

x_check_value = 640
y_check_value = 480

def mouse_event(event, x, y, flags, param):
	global x_check_value, y_check_value
	if event == cv2.EVENT_LBUTTONUP:
		print("print y : %d" % y)
		x_check_value = x
		y_check_value = y

# brack_flag ... 0.stop / 1.push
brack_flag = 0
slime_start_flag = 0

host = "127.0.0.1"
port = 22222

while True:

	ret, frame = cap.read()

	# y:max = 480
	used_display = frame[y_up_value:y_down_value , x_left_value:x_right_value]

	if pframe is None:
		pframe = np.zeros(used_display.shape, dtype=np.uint8)

	if not ret:
		print('failed')
		time.sleep(0.01)
		continue

	cv2.imshow('used_display', used_display)
	used_display = cv2.cvtColor(used_display, cv2.COLOR_BGR2GRAY)

	# ココの 180 ~ 255 はある程度調整しておく必要性がある
	_, _bin = cv2.threshold(used_display, 180, 255, cv2.THRESH_BINARY_INV | cv2.THRESH_OTSU)

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

	cv2.imshow('raw_frame', frame)
	cv2.setMouseCallback('raw_frame', mouse_event)

	with socket.socket(socket.AF_INET, socket.SOCK_DGRAM) as s:
		s.sendto((str(bollon_redius[0]) + chr(58) + str(brack_flag)).encode(), (host, port))

	if len(contours) <= 1:
		if slime_start_flag == 0 and brack_flag == 0:
			slime_start_flag = 1
		else:
			pass

	else:
		if slime_start_flag == 1 and brack_flag == 0:
			slime_start_flag = 0
			brack_flag = 1
		else:
			pass

	# print("slime_start_flag : %d, brack_flag : %d" % (slime_start_flag, brack_flag))

	key = cv2.waitKey(1)

	if key & 0xFF == ord('q'):
		break
	elif key & 0xFF == ord('r'):
		brack_flag = 0
	elif key & 0xFF == ord('p'):
		slime_start_flag = slime_start_flag * -1

	elif key % 0xFF == ord('2'):
		y_up_value = y_check_value
	elif key % 0xFF == ord('4'):
		x_left_value = x_check_value
	elif key % 0xFF == ord('5'):
		y_down_value = y_check_value
	elif key % 0xFF == ord('6'):
		x_right_value = x_check_value
