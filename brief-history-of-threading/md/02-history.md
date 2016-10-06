# Prehistory

![History](/images/history.gif)

(before .NET)

<..>

## Quick Review... Moore's Law

<div style="float: left; width: 45%;">
    <ul>
        <li>1965</li>
        <li>Number of transistors on integrated circuits doubles approximately every two years</li>
    </ul>
</div>

<div style="float: right; width: 45%;">
![Moore's Law](/images/moores-law.png)
</div>

Note:
- This is not a "natural law" - it was just worded this way and has held true
- Actually was changed to every 18 months for a period
- Doesn't have to mean clock speed... but I used to think so
- Regarding clock speed
    - In other words... 286, 386, 486, 233MHz, 500MHz, 1GHz, 2GHz, 3GHz... (between 1980 and 2000)
    - But now... my CPU is 2.40 GHz... but has 8 cores
- [Wikipedia](http://en.wikipedia.org/wiki/Moore's_law)

<..>

## How Does a CPU Work?

<div style="float: left; width: 45%;">
    <ul>
        <li>The ALU (Arithmetic Logic Unit) is the brain of the CPU</li>
        <li>As CPU architecture stands today, it can only do one thing at a time... it just does it very quickly</li>
    </ul>
</div>

<div style="float: right; width: 45%;">
![CPU](/images/cpu.gif)
</div>

Note:
- The implication is that, before multi-core processors, there was no way to actually run two things at once...
- So, how did we do "multi-tasking" before multiple cores or CPUs?
    - (discussion time)
- This leads us to... MULTITASKING

<..>

## Multitasking

> Multitasking is a method where multiple tasks, also known as processes, are performed during the same period of time

![Multitasking](/images/multitasking.png) <!-- .element: style="width: 60%;" -->

Note:
- [Wikipedia](http://en.wikipedia.org/wiki/Computer_multitasking)

- Just because it looks like it is doing two things at once doesn't mean it actually is!
- That core is *only* running procexp64.exe

> In computing, multitasking is a method where multiple tasks, also known as processes, are performed during the same period of time. The tasks share common processing resources, such as a CPU and main memory.

> In the case of a computer with a single CPU, only one task is said to be running at any point in time, meaning that the CPU is actively executing instructions for that task. Multitasking solves the problem by scheduling which task may be the one running at any given time, and when another waiting task gets a turn. The act of reassigning a CPU from one task to another one is called a context switch. When context switches occur frequently enough the illusion of parallelism is achieved."

- So... how does it actually decide when to switch to a new task or process?

<..>

## Scheduling

- Early DOS and Windows OS's weren't actually multitasked... at least not until Windows 3.1
    - Win3.1x used a non-preemptive scheduler
    - Win95 introduced a "rudimentary preemptive scheduler"
- Windows NT+, OSX, etc. all now use a multilevel feedback queue
    - Linux 2.6.23+, on the other hand, uses the Completely Fair Scheduler (CFS)

Note:
- Non-preemptive schedulers don’t interrupt tasks
    - This means that, when the OS gives you control... you can KEEP IT FOREVER!
    - Remember how, when a program hung and the background would just wipe? That's because you still had control and your program hadn’t yielded control back so that the OS could redraw the background
    - I tried finding a good image, but failed
- Multilevel feedback queues use a queuing mechanism to determine priority and which process needs the most time

<..>

## Multilevel Feedback Queues

What Windows uses today (and almost every other modern OS)

![Windows](/images/windows-multilevel-feedback-queue.png) <!-- .element: style="float: left; width: 45%;" -->
![OSX](/images/osx-multilevel-feedback-queue.png) <!-- .element: style="float: right; width: 45%;" -->

Note:
- Basically, it is a smart scheduler that will interrupt your tasks, but it also has a priority queue so that more important processes get preference
    - Things like IIS on a web server as opposed to the desktop environment
    - Task Manager itself runs at a higher priority
    - You can select the "Base Priority" column in Task Manager if you’re curious about it

<-->

## So, what does this have to do with threading?

Lots, as you'll see...

<..>

## Process

<div style="float: left; width: 45%;">
   <ul>
      <li>Very heavy</li>
      <li>Container for threads</li>
      <li>Cannot share memory with other processes</li>
      <li>Scheduled by the OS</li>
   </ul>
</div>

<div style="float: right; width: 45%;">
![Process](/images/definition-process.png)
</div>

Note:
- "heavy" is relative, but when we're talking about threading, it is the heaviest unit of work
- Note that the rule of sharing memory with other processes is actually a security boundary... and you can get to the memory of other processes if you have high enough privileges
    - As an example, debuggers can peak into your program's memory. And this is why debuggers require elevated permissions.
    - Still, you have to jump through hoops to do this.

<..>

## Thread

<div style="float: left; width: 45%;">
   <ul>
      <li><em>Every</em> process has <em>at least</em> one thread</li>
      <li>Process memory is shared between threads</li>
      <ul>
         <li>Insert ominous music here</li>
      </ul>
   </ul>
</div>

<div style="float: right; width: 45%;">
![Process](/images/definition-thread.png)
</div>

Note:
- As an aside, it is interesting that in Linux, scheduling is done at the "task" level
   - A task could be a thread or a single-threaded process... to the scheduler, they're the same thing

<..>

## So, I'm a .NET developer... do I care about this?

![Don't Care](/images/dont-care.gif)

<..>

## Well sure you should care

* It helps you understand why you shouldn't believe that adding threads makes your application faster
   * It *can* make it faster, but only in specific scenarios
* Moore's law is changing which means our programming practices need to change, too

<-->

# Back to our question...

<..>

## What Does Threading Solve?

You said... (discuss)

<..>

## Threading Solves Two Things

![Two Things](/images/two-things.jpg)

<..>

## Parallelism

* Computational intensive processing that doesn't depend on the output of other steps
* Retrieving data from multiple services in parallel

![Parallel](/images/parallel-traffic.gif)

<..>

## Removing Blocking

* Not blocking the UI or web thread
* Blocking usually means blocking I/O (database, network, file, etc.)

![Parallel](/images/non-blocking-assembly-line.gif)

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
