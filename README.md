# Introduction

The purpose of the project is purely as a demo for getting a simple dotnet core Api, built, tested, publish and running in a Docker container.

Master branch is purely the Api and test projects. There are multiple branches which add different states of a Docker file, i.e. a file to Build & Publish, then a branch with unit tests running as part of the image build. These branches can be used as a milestone points of reference.



### The API

The Api is very simple and very pointless, it simple returns dates and times in a human sentence. For example, instead of returning the date and time in a format of 11:30 10/01/2019, the service will return "Half past Eleven, AM on  Thursday, 10th January 2019".

The main purpose of the Api is just as a demo for dockerising a web api app and running the container within a Service Fabric Cluster.



##### Routes

The Api offers two main routes, one for "now"  and one for a specific date in the ISO 8601 format, the following routes are available:

**Now**

All the routes return specific data about the current date and time:

- api/now

  Returns the current time and date in the format of : 
  "Two Minutes Past One, PM on Monday, 11th February 2019"

- api/now/time

  Returns the current time in the format of:
  "Two Minutes Past One, PM"

- api/now/date

  Returns the current date in the format of:

  "Monday, 11th February 2019"

- api/now.day

  Return the current day of the week in the format of:

  "Today is Monday"

**Dates**

All of the routes return data about the date provided in the route, for example e.g. api/dates/2019-02-01/day. The following routes are available:

- api/dates/{date}/date

  Returns the date provided in the route in the format of:
  "Monday, 11th February 2019"

- api/dates/{date}/day

  Returns the day of the week for the day provided in the route, e.g.
  "Monday"



### Tests

There are a number of tests which are written in a BDD style. They utilise the Web Api Test Host which allows for the Api to be executed in an integrated fashion in process. The tests could be easily amended to run out of process too.