#!/bin/bash

sudo docker build -t lifemanager .
sudo docker run -p 5050:80 lifemanager
