# Back to our question...

<..>

## What Does Threading Solve?

You said... (discuss)

<..>

## Threading Solves Two Things

![Two Things](/images/two-things.jpg)

<..>

## Parellelism

* Computational intensive processing that doesn't depend on the output of other steps
* Retrieving data from multiple services in parallel

<..>

## Removing Blocking

* Not blocking the UI or web thread
* Blocking usually means blocking I/O (database, network, file, etc.)

<..>

## Does that surprise you?

* Does this definition exclude anything?
* Did we miss anything?
* Only one of these *directly* affects speed

(discuss)

Note:
- See [Hanselminutes](http://hanselminutes.com/327/everything-net-programmers-know-about-asynchronous-programming-is-wrong)
	- Actually has 3 things they list, 1) blocking I/O, 2) pulling lots of data from various places, and 3) long running requests (i.e. situations like what SignalR solves)
	- It is fairly specific to ASP.NET, which is the main reason I generalize to two reasons
- Note that removing blocking calls *can* help with increased throughput and parallelism, but mostly at the macro level (i.e. if you don't have lots of ASP.NET threads blocking, IIS can send more requests through)
