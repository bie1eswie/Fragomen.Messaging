Skip to content
DEV Community
Search...
Powered by  Algolia
Log in
Create account

10
Jump to Comments
46
Save

Cover image for Building and Deploying a Minimal API on AWS ECS/Fargate using Terraform
AWS Community Builders  profile imageOluwasegun Adedigba
Oluwasegun Adedigba for AWS Community Builders
Posted on Sep 11, 2023


17

2

2

2

3
Building and Deploying a Minimal API on AWS ECS/Fargate using Terraform
#
terraform
#
aws
#
api
#
docker
Introduction:
The aim of this blog post is to show in detail the process of creating a minimal API with two endpoints and deploying it on AWS ECS/Fargate using Terraform. The endpoint API codes are written in Python.

These APIs provide two simple functionalities which are listed below:

1. Timestamp API: This is responsible for retrieving the current Unix timestamp.

2. Random Numbers API: This is responsible for generating a list of 10 random numbers in the range of 0 to 5.

The aim of this article is to walk the reader through the process of creating a working API running in a Docker container on AWS ECS/Fargate which is accessible to the public.

Requirement:
To be able to do this on your device, make sure you have the following in place:

AWS account with appropriate permissions to create ECS/Fargate resources. Visit here to get started.

AWS CLI installed and configured on your local device. Visit here to install and configure AWS CLI on your device.

Docker installed on your local machine. Visit here to install Docker on your device.

Terraform installed on your local machine. Visit here to install Terraform on your device.

Procedure:
Creating the Minimal APIs
The first step is creating the python APIs for the two endpoints 'time' and 'random'. The Flask framework is used to create the endpoints. The code snippets below are used to create the endpoints:

Time Endpoint:
# app.py
from flask import Flask
import time

app = Flask(__name__)

@app.route('/time')
def get_current_time():
    timestamp = str(int(time.time()))
    response = {
        "data": {"unix_timestamp": timestamp},
        "message": "success"
    }
    return response
Random Numbers Endpoint:
# app.py
from flask import Flask
import random

app = Flask(__name__)

@app.route('/random')
def get_random_numbers():
    numbers = [random.randint(0, 5) for _ in range(10)]
    response = {
        "data": {"random_number": numbers},
        "message": "success"
    }
    return response


if __name__ == '__main__':
    app.run(host='0.0.0.0', port=5000)
Creating the Dockerfile
Next, a Dockerfile that includes the Python APIs and specifies the necessary configurations is created using the Dockerfile snippet shown below:
FROM python:3.10-alpine3.18

# WORKDIR /app

COPY . .

RUN pip install -r requirements.txt

EXPOSE 5000

ENV PYTHONUNBUFFERED=1

CMD ["python" ,"app.py"]
What this Dockerfile does is that it sets up the container environment using Python 3.10-alpine3.18 as the base container image. This base image keeps the container size at a minimal level due to it being lightweight.

The 'COPY' line copies everything from the working directory into the working directory of the Docker image.

The 'RUN' command installs the dependencies of the application which are listed in the requirements file.

The 'EXPOSE' command lets the container listen on port 5000.

The 'CMD' line runs the app.py file when the container is up and running.

Building and Pushing the Docker Image
After writing the codes for the APIs and the Dockerfile commands, the Docker image is built and pushed to a public container registry. In this case, Amazon Elastic Container Registry Amazon ECR is used.

First, create your own repository on Amazon ECR. Select your own preferred configurations and give you repository a preferred name.

Create ECR Repository

Then, build the Docker image using the AWS CLI commands below:
# Get authentication token and authenticate Docker client to the registry

aws ecr get-login-password --region <your-region> | docker login --username AWS --password-stdin <your-account-id>.dkr.ecr.<your-region>.amazonaws.com

#Build the image

docker build -t <your-repo-name>:<tag> .

After building the image, tag the image with a preferred tag with which it will be pushed into the repository
# Tag the Image 

docker tag <your-repo-name>:<tag> <your-account-id>.dkr.ecr.<your-region>.amazonaws.com/<your-repo-name>:<tag>

After the image has been correctly tagged, the command below pushes the image into the repository
# Push the Docker image to Docker Hub

docker push <your-account-id>.dkr.ecr.<your-region>.amazonaws.com/<your-repo-name>:<tag>

Alternatively, the commands to build and push the Docker image to a repository on Amazon ECR can be found by clicking the "View push commands" button in the repository page.

View Push Commands

Creating the Terraform script for Deployment
After the image has been uploaded to the created ECR repository, the terraform script which will be used to deploy the container on Amazon Elastic Container service Amazon ECS is then created.

Firstly, configure the necessary AWS credentials using the AWS CLI command below. Use the AWS Access Key ID and Secret Access Key, set the default region and the preferred output format.
aws configure

After configuring the AWS credentials, create a file named 'main.tf' and paste the code snippet below:
# Provider definition
provider "aws" {
  region = "us-east-1"
}

# VPC definition
data "aws_vpc" "existing" {
  id = "vpc-XXXXXXXXXXXXXXXXX"  # Replace "vpc-XXXXXXXXXXXXXXXXX" with your VPC ID
}

# Security group for the ECS tasks
resource "aws_security_group" "ecs_sg" {
  vpc_id = data.aws_vpc.existing.id
  name   = "ecs-security-group"
  # Inbound and outbound rules
  ingress {
    from_port   = 5000
    to_port     = 5000
    protocol    = "tcp"
    cidr_blocks = ["0.0.0.0/0"]
  }
  egress {
    from_port   = 0
    to_port     = 0
    protocol    = "-1"
    cidr_blocks = ["0.0.0.0/0"]
  }
}

# ECS task definition
resource "aws_ecs_task_definition" "task_definition" {
  family                = "miniflask-api-task"
  network_mode          = "awsvpc"
  memory                = "512"
  requires_compatibilities = ["FARGATE"]

  # Task execution role (Replace "XXX" with your IAM role ARN)
  execution_role_arn    = "arn:aws:iam::XXX:role/ecr_task_role"  # Replace "XXX" with your IAM role ARN

  # Container definition
  container_definitions = jsonencode([
    {
      name      = "miniflask-api-container"
      image     = "public.ecr.aws/g1s5q2a7/miniflask-api:latest" 
      cpu       = 256
      memory    = 512
      port_mappings = [
        {
          container_port = 5000
          host_port      = 5000
          protocol       = "tcp"
        }
      ]
    }
  ])

  # Defining the task-level CPU
  cpu = "256"  
}

# ECS service
resource "aws_ecs_cluster" "ecs_cluster" {
  name = "minimal-api-cluster"  
}

resource "aws_ecs_service" "service" {
  name            = "miniflask-api-service"
  cluster         = aws_ecs_cluster.ecs_cluster.id
  task_definition = aws_ecs_task_definition.task_definition.arn
  desired_count   = 1
  launch_type     = "FARGATE"

  # Network configuration
  network_configuration {
    subnets          = ["subnet-XXX", "subnet-XXX", "subnet-XXX", "subnet-XXX", "subnet-XXX", "subnet-XXX"]  # Replace "subnet-XXX" with your subnet IDs
    security_groups  = [aws_security_group.ecs_sg.id]
    assign_public_ip = true
  }
}
Deploying the APIs
After the terraform script has been written with the appropriate values of the variables in the main.tf file, the APIs are deployed to AWS ECS/Fargate using Terraform with the following terraform commands:
# Initialize Terraform in the project directory
terraform init

# Preview the resources that will be created
terraform plan

# Deploy the resources to AWS
terraform apply
Accessing the APIs
Once the terraform apply command is complete and successful, you should see an output message which shows the number of resources added, changed and destroyed.

The public IP address of the API endpoint can then be gotten from the Amazon ECS page in the AWS management console or by using the following AWS CLI command in the terminal:
aws ecs describe-tasks --cluster YOUR_CLUSTER_NAME --tasks YOUR_TASK_ID --query 'tasks[0].attachments[0].details[?name==`networkInterfaceId`].value'
After the public IP address is gotten, the endpoints can be accessed by using a browser or using the terminal.

If a browser is used, the two endpoints can be accessed by using the public IP address as shown below:

Current Unix timestamp:
http://x.x.x.x:5000/time
Unix Timestamp

List of 10 random numbers (0 < X < 5):
http://x.x.x.x:5000/random
Random Numbers

If a terminal is used, the two endpoints can be accessed by using the public IP address as shown below:

Current Unix timestamp:
curl http://x.x.x.x:5000/time
List of 10 random numbers (range: 0 to 5):
curl http://x.x.x.x:5000/random
Conclusion
After following this procedure, a minimal API built using Flask, containerized with Docker, deployed on AWS ECS with a Fargate configuration using Terraform would have been achieved. The knowledge gotten from this can be built upon to develop more complex solutions on AWS.

Thank you for making it to this point. Comments and feedback are greatly appreciated. Happy coding!

👋 Do you have a minute?

Please leave your appreciation by commenting on this post!

It takes one minute and is worth it for your career.

Get started

Top comments (10)
Subscribe
pic
Add to the discussion
 
 
yogini16 profile image
yogini16
•
Sep 12 '23

Great post !!
Minimal api is great concept for building fast HTTP APIs with ASP.NET Core.
Though this post indirectly talks about use case of minimal api. If you mention use cases of minimal apis, it will give more insights to readers.


1
 like
Like
Reply
 
 
cloudsege profile image
Oluwasegun Adedigba 
•
Sep 14 '23

Thanks for this. I will address this in another post. Thank you


Like
Reply
 
 
seuncaleb profile image
seuncaleb
•
Sep 11 '23

This is so helpful thanks for sharing


2
 likes
Like
Reply
 
 
cloudsege profile image
Oluwasegun Adedigba 
•
Sep 14 '23

Thank you!


1
 like
Like
Reply
 
 
cvanti profile image
Cristian V
•
Sep 11 '23

But you know that for an app like this, AppRunner or even Lambda would be much better options, and that configuration of ECS is not production-ready


2
 likes
Like
Reply
 
 
cloudsege profile image
Oluwasegun Adedigba 
•
Sep 14 '23

Definitely. This was a problem I came across once, which is why I decided to write on it. Thank you


1
 like
Like
Reply
 
 
zaheer profile image
Abdul-zahir Alao
•
Sep 11 '23

Great, thanks for sharing!


2
 likes
Like
Reply
 
 
cloudsege profile image
Oluwasegun Adedigba 
•
Sep 14 '23

Thank you!


1
 like
Like
Reply
 
 
eluedev profile image
Wisdom
•
Sep 11 '23

This is great! do one for Node.js next🥂


2
 likes
Like
Reply
 
 
cloudsege profile image
Oluwasegun Adedigba 
•
Sep 14 '23

I'll look into that. Thank you!


1
 like
Like
Reply
Code of Conduct • Report abuse
profile
AWS Community Builders

Create a simple OTP system with AWS Serverless cover image

Create a simple OTP system with AWS Serverless
Implement a One Time Password (OTP) system with AWS Serverless services including Lambda, API Gateway, DynamoDB, Simple Email Service (SES), and Amplify Web Hosting using VueJS for the frontend.

Read full post

Read next
kelvinskell profile image
Comparing AWS RDS to NoSQL Databases like DynamoDB: When to Use Which
Kelvin Onuchukwu - Jun 19

suravshrestha profile image
Install Apache Web Server in Ubuntu AWS EC2 Instance
Surav Shrestha - May 27

lechnerc77 profile image
Terramate this SAP BTP!
Christian Lechner - Jun 17

zahraajawad profile image
Setting Up a Secure Wazuh Environment by AWS EC2
Zahraa Jawad - Jun 16


AWS Community Builders
Follow
Build On!
Would you like to become an AWS Community Builder? Learn more about the program and apply to join when applications are open next.

Learn more
More from AWS Community Builders
Spring Boot 3 application on AWS Lambda - Part 8 Introduction to Spring Cloud Function
#java #springboot #aws #serverless
Amazon EC2 or Amazon RDS, when to choose?
#ec2 #rds #aws #communitybuilder
How to Cloud: IaC
#aws #beginners #terraform
profile
AWS Community Builders

How I obtained all AWS associate level certificates in two weeks. cover image

How I obtained all AWS associate level certificates in two weeks.
The author shares their personal journey and tips for passing all AWS associate level exams in 2 weeks, highlighting courses, hands-on experience, research, and practice exams as essential success factors.

Read full post

# Provider definition
provider "aws" {
  region = "us-east-1"
}

# VPC definition
data "aws_vpc" "existing" {
  id = "vpc-XXXXXXXXXXXXXXXXX"  # Replace "vpc-XXXXXXXXXXXXXXXXX" with your VPC ID
}

# Security group for the ECS tasks
resource "aws_security_group" "ecs_sg" {
  vpc_id = data.aws_vpc.existing.id
  name   = "ecs-security-group"
  # Inbound and outbound rules
  ingress {
    from_port   = 5000
    to_port     = 5000
    protocol    = "tcp"
    cidr_blocks = ["0.0.0.0/0"]
  }
  egress {
    from_port   = 0
    to_port     = 0
    protocol    = "-1"
    cidr_blocks = ["0.0.0.0/0"]
  }
}

# ECS task definition
resource "aws_ecs_task_definition" "task_definition" {
  family                = "miniflask-api-task"
  network_mode          = "awsvpc"
  memory                = "512"
  requires_compatibilities = ["FARGATE"]

  # Task execution role (Replace "XXX" with your IAM role ARN)
  execution_role_arn    = "arn:aws:iam::XXX:role/ecr_task_role"  # Replace "XXX" with your IAM role ARN

  # Container definition
  container_definitions = jsonencode([
    {
      name      = "miniflask-api-container"
      image     = "public.ecr.aws/g1s5q2a7/miniflask-api:latest" 
      cpu       = 256
      memory    = 512
      port_mappings = [
        {
          container_port = 5000
          host_port      = 5000
          protocol       = "tcp"
        }
      ]
    }
  ])

  # Defining the task-level CPU
  cpu = "256"  
}

# ECS service
resource "aws_ecs_cluster" "ecs_cluster" {
  name = "minimal-api-cluster"  
}

resource "aws_ecs_service" "service" {
  name            = "miniflask-api-service"
  cluster         = aws_ecs_cluster.ecs_cluster.id
  task_definition = aws_ecs_task_definition.task_definition.arn
  desired_count   = 1
  launch_type     = "FARGATE"

  # Network configuration
  network_configuration {
    subnets          = ["subnet-XXX", "subnet-XXX", "subnet-XXX", "subnet-XXX", "subnet-XXX", "subnet-XXX"]  # Replace "subnet-XXX" with your subnet IDs
    security_groups  = [aws_security_group.ecs_sg.id]
    assign_public_ip = true
  }
}
DEV Community — A constructive and inclusive social network for software developers. With you every step of your journey.

Home
Podcasts
Videos
Tags
DEV Help
Forem Shop
Advertise on DEV
DEV Challenges
DEV Showcase
About
Contact
Guides
Software comparisons
Code of Conduct
Privacy Policy
Terms of use
Built on Forem — the open source software that powers DEV and other inclusive communities.

Made with love and Ruby on Rails. DEV Community © 2016 - 2024.