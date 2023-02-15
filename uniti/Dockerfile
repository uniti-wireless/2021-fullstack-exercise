# pull official base image
FROM python:3.10-slim

# set environment variables
ENV PYTHONDONTWRITEBYTECODE 1
ENV PYTHONUNBUFFERED 1

# set work directory
WORKDIR /app/src

# install system dependencies
RUN apt-get update && \
    apt-get upgrade -y && \
    apt-get install -y gcc bash && \
    apt-get clean

# copy requirements.txt to run it in working directory
COPY requirements.txt /app/src

# install python dependencies
ENV LIBRARY_PATH=/lib:/app/lib
RUN pip install --upgrade pip
RUN pip install --upgrade setuptools
RUN pip --no-cache-dir install -r requirements.txt

# copy project
COPY ./src/ /app/src

# change workdirectory
WORKDIR /app/src

# run command
CMD gunicorn --bind 0.0.0.0:5000 main:app -k uvicorn.workers.UvicornWorker
