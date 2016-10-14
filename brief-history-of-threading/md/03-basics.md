# Basic Threading

![Basics](/images/hello-computer.gif)

(.NET 1.0-2.0)

<..>

## Example 01: No (Additional) Threading

![No Threading](/images/basics-01.png) <!-- .element width="60%" -->

(wait... why are there four threads???)

Note:
- The starting addresses give us a few hints
    - For example, gdiplus.dll is related to GDI+ (graphics)
    - mscorwks.dll is one of the core CLR assemblies
    - And of course there is our assembly
- But... VS2012 has some really nice threading tools

<..>

## Example 02: Single Thread (The Wrong Way)

```cs
var thread = new System.Threading.Thread(DoWork);
thread.Start();
```

* Disclaimer - this code breaks some rules... in fact, it breaks *the golden rule of threading*...

<..>

## Example 02: Fix the GUI Thread

```cs
BeginInvoke(new Action<object>(x => {
   txtCount.Text = x.ToString();
}), i);
```

* In WinForms, you use `Control.Invoke` or `Control.BeginInvoke`
* In WPF, you use `Dispatcher.Invoke` or `Dispatcher.BeginInvoke`
* In addition, see `SynchronizationContext.Current` and use `Send` or `Post`

Note:
- `Invoke` executes on the UI thread, but waits for completion before continuing (can prevent shared state issues)
- `BeginInvoke` executes on the UI thread, but doesn't wait for completion before continuing (fire and forget... sort of)

<..>

## Example 03: BackgroundWorker

```cs
using (var worker = new System.ComponentModel.BackgroundWorker()) {
   worker.WorkerReportsProgress = true;

   worker.DoWork += DoWork;
   worker.ProgressChanged += (o, args) => {
      txtCount.Text = args.UserState.ToString();
   };
   worker.RunWorkerCompleted += (o, args) => {
      txtTotalTime.Text = args.Result.ToString();
   };

   worker.RunWorkerAsync();
}
```

<..>

## Example 03: BackgroundWorker

* Introduced in .NET 2.0 because threading is hard
   * And because everyone was updating the UI thread from the background thread
* It falls in the easy category because...
   * It wasn't written by the threading team, but instead by the WinForms team (even lives under the `System.ComponentModel` namespace)
   * It can be dropped on your design surface
   * It automatically marshals calls back to the UI thread for you (via its events)

<..>

## Example 04: ThreadPool Threads

```cs
ThreadPool.QueueUserWorkItem(DoWork);
```

* Thread construction is expensive
   * They're good for long running background tasks, but if you're doing lots of small tasks, use a `ThreadPool` thread instead
* The CLR provides a "pool of threads" for you that are all ready to go
   * ASP.NET uses the ThreadPool for all of its requests
   * WCF uses the ThreadPool
   * And so on

<..>

## Quick aside... when should I use which?

* `BackgroundWorker`
   * Only use this if you're in WinForms or WPF or another GUI technology
* `ThreadPool`
   * Efficiency
   * Default to using this
   * Tasks (we're not there yet) are ThreadPool threads…
* `Thread`
   * Long running request
   * You need more control over thread details (e.g. priority, identity, etc.)

Note:
- Not even taking into account Tasks and the TPL right now… but those are ThreadPool threads

<..>

## Example 05: IAsyncResult

```cs
var iar = dlg.BeginInvoke(
   arg,
   Callback,
   new Tuple<Func<int, int>, int>(dlg, arg));
```

* Sometimes called the [Asynchronous Programming Model \(APM\)](http://msdn.microsoft.com/en-us/library/ms228963.aspx)
* Most of the time you consume APM instead of writing it yourself
* Usually only have to implement it when you're writing your own libraries

<..>

## Example 06: Why IAsyncResult versus...?

* So... why would I go through all of that complexity as compared to just using a ThreadPool thread on my own?
* It has everything to do with having a blocking thread or not <!-- .element class="fragment" -->

![Blocking](/images/shared-state-traffic.gif) <!-- .element class="fragment" -->

Note:
- Initial version doesn't even pretend to use threading
- Threaded version uses a thread... but still waits for the results
In fact, with that version, we've got TWO blocking threads now instead of just one – it got WORSE
- Final version is more complex, but the .NET Framework is trying to guide you down the right path
    - Look at IAsyncHttpHandler versus the IHttpHandler
    - See http://msdn.microsoft.com/en-us/library/system.web.ihttphandler(v=vs.110).aspx

