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
      <li>Kernel threads vs user threads</li>
      <ul>
         <li>Distinction is who schedules them
         <li>Managed threads are a hybrid</li>
      </ul>
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

## Fiber

- "Lightweight thread"
- Fibers have to yield control back when they're done
- This means that fibers cannot be truly multiprocessor, though, because two fibers cannot run at the same time
- Process memory is shared *but is not accessed at the same time*

Note:
- No screenshot because I have no idea which Windows processes even use fibers
- This slide is more for informational purposes... you'll see fibers show up more in non-Windows environments (e.g. Rails can use fiber-based parallelism)

- It is theoretically possible to implement a CLR host that can manage threading entirely on its own
   - SQL Server Yukon (2005) wanted to use fibers for its threads in its CLR host (i.e. SQLCLR) instead of full threads
   - In response, the CLR team tried to add fiber support in Whidbey (.NET 2.0)

> Eventually, mostly due to schedule pressure and a long stress bug tail related to fiber-mode, we threw up our hands and declared it unsupported." (via http://joeduffyblog.com/2006/11/09/fibers-and-the-clr/)

<..>

## So, I'm a .NET developer... do I care about this?

![Don't Care](/images/dont-care.gif)

<..>

## You SHOULD care

* It helps you understand why you shouldn't believe that adding threads makes your application faster
   * It *can* make it faster, but only in specific scenarios
* Moore's law is changing which means our programming practices need to change, too
