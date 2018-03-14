# Modern Threading

<!-- .slide: data-background-image="./images/modern.gif" -->

<!-- .slide: class="shadowed-text" -->

<!--![Modern](./images/modern.gif)-->

(.NET 3.5-...)

<..>

## Task Parallel Library

* AKA Parallel Extensions AKA PFX AKA TPL...
* Built by Microsoft Research and CTPs were available as early as 2007 for .NET 3.5
* Introduced formally in .NET 4.0
* In addition to `Parallel.ForEach` and friends, also includes PLINQ
* _Very_ much about parallelism (as opposed to removing blocking)

<..>

## Tasks

* Tasks are really just a simpler model for thread pool threads
* `Task.Run(() => { })`
* `Task.WhenAll(...)`
* `Task.Factory.FromAsync`

```cs
Task<SqlDataReader>.Factory.FromAsync(
    command.BeginExecuteReader,
    command.EndExecuteReader, null);
```

Note:

* The _central_ part of TPL
  * Everything in TPL deals with Tasks...
* Tasks are a new way to look at threads
* In the future, core .NET components will instead be dealing with Tasks instead of IAsyncResult (note that Tasks provide an IAsyncResult, so that isn't tossed out... again, it is just encapsulated!)
* In fact, async/await deal directly with Tasks!
* Can also cancel tasks, continue with other tasks when this task completes (called a continuation)

<..>

## Parallel Looping

<div style="float: left; width: 52%;">
<pre>
<code class="cs">foreach (var item in list) {
    // op (not in parallel)
}

for (var idx = 0; idx < 100, idx++) {
// op (not in parallel)
}
</code></pre>

</div>

<div style="float: right; width: 48%;" class="fragment">
<pre>
<code class="cs">Parallel.ForEach(list, item => {
    // op (all in parallel)
});

Parallel.For(0, 100, idx => {
// op (all in parallel)
});
</code></pre>

</div>

Note:

* TPL is smart about utilizing your cores efficiently
  * Having _too_ many threads running can hurt performance more than just going synchronously – the CPU could spend all of its time swapping between threads instead of giving each one enough time to get things done
* Supports aggregation
  * You might want to just add a lock and gather results in each thread, but you would be locking more than necessary
  * Instead, TPL keeps a threadlocal storage where you can gather results for each thread independently and a another callback where you can gather those results
* Supports partitioning
  * If you need more fine-grained control over calls... if you know the number of results you have and the size of the data involved, you can break the work up and have control over how the TPL breaks up tasks

<..>

## PLINQ

<div style="float: left; width: 45%;">
<pre>
<code data-noescape data-trim class="cs">(from x in someResults
 where x % 2 == 0
 select x).
Aggregate((x, y) => x + y);
</code></pre>
</div>

<div style="float: right; width: 45%;" class="fragment">
<pre>
<code data-noescape data-trim class="cs">(from x in someResults
 where x % 2 == 0
 select x).
<mark>AsParallel().</mark>
Aggregate((x, y) => x + y);
</code></pre>
</div>

Note:

* Yeah, pretty complicated, I know
  * Honestly, PLINQ will probably do what you want it to more easily (and correctly) than you if you had done it directly with TPL

<..>

## Example 07: TPL, PLINQ, etc.

<..>

## Async/Await

* Technically, async/await provide compiler support for native continuations by building a state machine for you
* _Very_ much about removing blocking calls (as opposed to the TPL)

<div style="float: left; width: 45%;">
![Prettified Decompiled Code](./images/async-01.png)
</div>

<div style="float: right; width: 55%;">
![Straight Decompiled Code](./images/async-02.png)
</div>

<..>

## Example 08: Continuations! (and async)

* Quick vocabulary
  * Coroutine
    * Yield in C# (and other languages)
    * Compiler support to break a function apart
  * Continuation
    * A pointer to a function to call when done
    * Ajax callbacks, JavaScript pointers, etc.

<..>

## Example 09: What about .NET Core???

<..>

## Async/Await Gotchas

<!-- .slide: data-background-image="./images/dumb-and-dumber-sad.gif" -->

<!-- .slide: class="shadowed-text" -->

<!-- ![Shoving Computer](./images/fail-door-prank.gif) -->

<..>

## Async/Await Gotchas (part 1)

* Async/await will "barf" all over your code
  * You'll think you can just add a single async/await pair here... but then you forget that every call above (unless you block) has to now have it
* Can't use them with properties (i.e. no async getter/setters)
* Can't use them with all LINQ (simple ones yes, complex no)
* Have to return Tasks all over the place now
  * See WCF calls

Note:

* See `~/Dropbox/documents/from-laptop/Presentations` for details
  * both diff stat and patch show amount of changes
* As an example of a LINQ statement not supported... you cannot use await with a SelectMany because there are multiple items being enumerated
* An alternative to throwing Tasks all over your WCF code is to just instead let Visual Studio (i.e. svcutil) generate your proxies for you
  * It has async support... we historically have always coded our own WCF proxies, but this would be a reason to consider changing
  * Note – the primary reason we code our own WCF proxies is so that we don't inadvertently forget to regen our proxies and then break code

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

* For example, File.ReadAllLines doesn't have an async version... there are async versions to read single lines, but the helper methods don't have async yet, so you'll be doing more work

<..>

## Gotcha: Parallel Looping Over Async

<div>
So, you've got the following:

<pre><code class="cs">foreach (var category in categories) {
  await category.GetStatusAsync();
}</code></pre>

</div>

<div class="fragment">
Let's refactor to this!

<pre><code class="cs">Parallel.ForEach(categories, category => {
  await category.GetStatusAsync();
}</code></pre>

</div>

### OH NO! <!-- .element class="fragment" -->

Note:

* http://blogs.msdn.com/b/pfxteam/archive/2012/03/05/10278165.aspx
* http://blogs.msdn.com/b/pfxteam/archive/2012/03/04/10277325.aspx
* Note that just using the wrong looping construct can also bite you
  * List.ForEach has issues with async lambdas because you're never actually calling await
* You better test it... it might just skip right over all of your async lambdas (because they don't block) and you'll never get results back
* Here is a general rule – if you're using Parallel.ForEach and async at the same time, you're doing it wrong
  Modern
