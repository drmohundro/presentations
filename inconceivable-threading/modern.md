# Modern Threading

<..>

## Example 05: IAsyncResult

* Sometimes called the [Asynchronous Programming Model \(APM\)](http://msdn.microsoft.com/en-us/library/ms228963.aspx)
* 80+% of the time you consume APM instead of writing it yourself
* Usually only have to implement it when you're writing your own libraries

<..>

## Example 06: Why IAsyncResult versus...?

* So... why would I go through all of that complexity as compared to just using a ThreadPool thread on my own?
    * It has *everything* to do with having a blocking thread or not

Note:
- ResourceDownloader / GetResource.ashx is used basically to proxy images on templates through our site so that all resources are served over HTTPS
- Why did it need to be threaded in the first place?
    - Remember that ticketing bug with the Singing Christmas Tree and the Bellevue website from last year?
    - They were using an old template that referenced an image on the Bellevue website instead of our website
    - Their site couldn't handle the load (or had a bug or something)
    - Our site could handle it fine... except that we were waiting on their site to serve the image
    - And thus, our site was now having issues
    - Note that this isn't the kind of problem that can be solved by adding hardware either
        - We were running at very low CPU because we weren't doing ANYTING
        - IIS was still serving requests, but remember the scheduling discussion from last time? The CPU has to give all of the threads a chance to work... it doesn't know if one is blocking or not.
- Initial version doesn't even pretend to use threading
- Threaded version uses a thread... but still waits for the results
In fact, with that version, we've got TWO blocking threads now instead of just one – it got WORSE
- Final version is more complex, but the .NET Framework is trying to guide you down the right path
    - Look at IAsyncHttpHandler versus the IHttpHandler
    - See http://msdn.microsoft.com/en-us/library/system.web.ihttphandler(v=vs.110).aspx
    - ResourceDownloader is actually used by GetResource.ashx.cs
    - Note that the real version actually uses a custom AsyncData object instead of an object array

<..>

## Example 07: Task Parellel Library

* AKA Parallel Extensions AKA PFX AKA TPL...
* Built by Microsoft Research and CTPs were available as early as 2007 for .NET 3.5
* Introduced formally in .NET 4.0
* In addition to `Parallel.ForEach` and friends, also includes PLINQ
* *Very* much about parallelism (as opposed to removing blocking)

<..>

## Tasks

* Tasks are really just a simpler model for thread pool threads
* `Task.Run(() => { })`
    * Start the specified lambda/delegate in a new task (thread)
    * Shortcut to `Task.Factory.StartNew`
* `Task.WhenAll(...)`
    * Wait for all tasks passed in to complete
* `Task.Factory.FromAsync`
    * Remember `IAsyncResult`? This can create a task from a `BeginInvoke`/`EndInvoke` pair (NICE)
    * `Task<SqlDataReader>.Factory.FromAsync(command.BeginExecuteReader, command.EndExecuteReader, null);`

Note:
- The *central* part of TPL
    - Everything in TPL deals with Tasks...
- Tasks are a new way to look at threads
- In the future, core .NET components will instead be dealing with Tasks instead of IAsyncResult (note that Tasks provide an IAsyncResult, so that isn't tossed out... again, it is just encapsulated!)
- In fact, async/await deal directly with Tasks!
- Can also cancel tasks, continue with other tasks when this task completes (called a continuation)

<..>

## Parallel Looping

<div style="float: left; width: 45%;">
<pre>
<code class="cs">
Parallel.ForEach(list, item => {
    // op (all in parallel)
});

Parallel.For(0, 100, idx => {
    // op (all in parallel)
});
</code></pre>
</div>

<div style="float: right; width: 45%;">
<pre>
<code class="cs">
foreach (var item in list) {
    // op (not in parallel)
}

for (var idx = 0; idx < 100, idx++) {
    // op (not in parallel)
}
</code></pre>
</div>

Note:
- TPL is smart about utilizing your cores efficiently
    - Having *too* many threads running can hurt performance more than just going synchronously – the CPU could spend all of its time swapping between threads instead of giving each one enough time to get things done
- Supports aggregation
    - You might want to just add a lock and gather results in each thread, but you would be locking more than necessary
    - Instead, TPL keeps a threadlocal storage where you can gather results for each thread independently and a another callback where you can gather those results
- Supports partitioning
    - If you need more finegrained control over calls... if you know the number of results you have and the size of the data involved, you can break the work up and have control over how the TPL breaks up tasks

<..>

## PLINQ

<div style="float: left; width: 45%;">
<pre>
<code class="cs">
(from x in someResults
where x % 2 == 0
select x).
AsParallel().
Aggregate((x, y) => x + y);
</code></pre>
</div>

<div style="float: right; width: 45%;">
<pre>
<code class="cs">
(from x in someResults
where x % 2 == 0
select x).
Aggregate((x, y) => x + y);
</code></pre>
</div>

Note:
- Yeah, pretty complicated, I know
    - Honestly, PLINQ will probably do what you want it to more easily (and correctly) than you if you had done it directly with TPL

<..>

## Example 08: TPL, PLINQ, etc.

<..>

## Async/Await

* Technically, async/await provide compiler support for native continuations by building a state machine for you
* *Very* much about removing blocking calls (as opposed to the TPL)

<div style="float: left; width: 45%;">
![Prettified Decompiled Code](/images/async-01.png)
</div>

<div style="float: right; width: 45%;">
![Straight Decompiled Code](/images/async-02.png)
</div>

<..>

## Example 09: Continuations! (and async)

* Quick vocabulary
    * Coroutine
        * Yield in C# (and other languages)
        * Compiler support to break a function apart
    * Continuation
        * A pointer to a function to call when done
        * Ajax callbacks, JavaScript pointers, etc.

<..>

## Example 10: Deployer - Async Port

* Things to note
    * .NET 4.5
    * Lots of boilerplate async code was removed (Framework\Async.cs)
    * Async, await, task are everywhere now
* It is cleaner? Yes, definitely
* Is the complexity removed? Yes, but not by a lot...

Note:
- See `git vdf master..port-to-async` for details
- See also `git diff --stat master`

<..>

## Async/Await Gotchas

![Rolling Down Hill](/images/rolling-down-hill.gif)

<..>

## Async/Await Gotchas (part 1)

* Async/await will "barf" all over your code
    * You'll think you can just add a single async/await pair here... but then you forget that every call above (unless you block) has to now have it
* Can't use them with properties (i.e. no async getter/setters)
* Can't use them with all LINQ (simple ones yes, complex no)
* Have to return Tasks all over the place now
    * See WCF calls

Note:
- As an example of a LINQ statement not supported... you cannot use await with a SelectMany because there are multiple items being enumerated
- An alternative to throwing Tasks all over your WCF code is to just instead let Visual Studio (i.e. svcutil) generate your proxies for you
    - It has async support... we historically have always coded our own WCF proxies, but this would be a reason to consider changing
    - Note – the primary reason we code our own WCF proxies is so that we don't inadvertently forget to regen our proxies and then break code

<..>

## Async/Await Gotchas (part 2)

* Can't use inline statements with async/await

        // invalid
        if (!await GetIsDoneAsync())

        // valid
        var isDone = await GetIsDoneAsync()
        if (!isDone)

* Lots of built-in .NET calls do not yet support async/await
    * Process (wait for exit)
    * I/O (in some places yes, in others not)

Note:
- For example, File.ReadAllLines doesn't have an async version... there are async versions to read single lines, but the helper methods don't have async yet, so you'll be doing more work

<..>

## Gotcha: Parallel Looping Over Async

So, you've got the following:

        foreach (var category in categories) {
            await category.GetStatusAsync();
        }

Let's refactor to this!

        Parallel.ForEach(categories, cat => {
            await category.GetStatusAsync();
        }

OH NO!

Note:
- http://blogs.msdn.com/b/pfxteam/archive/2012/03/05/10278165.aspx
- http://blogs.msdn.com/b/pfxteam/archive/2012/03/04/10277325.aspx
- Note that just using the wrong looping construct can also bite you
    - List.ForEach has issues with async lambdas because you're never actually calling await
- You better test it... it might just skip right over all of your async lambdas (because they don't block) and you'll never get results back
- Here is a general rule – if you're using Parallel.ForEach and async at the same time, you're doing it wrong
