# History

<..>

## Quick Review... Moore's Law

<div style="float: left; width: 45%;">
    <ul>
        <li>1965</li>
        <li>Number of transistors on integrated circuits doubles approximately every two years</li>
        <li>Doesn't have to mean clock speed... but I used to think so</li>
    </ul>
</div>

<div style="float: right; width: 45%;">
![Moore's Law](/images/moores-law.png)
</div>


Note:
- This is not a "natural law" - it was just worded this way and has held true
- Actually was changed to every 18 months for a period
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

- Early DOS and Windows OS’s weren’t actually multitasked... at least not until Windows 3.1
    - Win3.1x used a non-preemptive scheduler
    - Win95 introduced a "rudimentary preemptive scheduler"
- Windows NT+, OSX, FreeBSD, NetBSD, and Solaris all now use a multilevel feedback queue
    - Linux 2.6.23+, on the other hand, uses the Completely Fair Scheduler (CFS)

Note:
- Without a scheduler, they run only one thing at a time
    - Remember DOS?
    - You sat at C: and typed in a program... then it ran that program.
    - It ran it until you exited out back to DOS
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
