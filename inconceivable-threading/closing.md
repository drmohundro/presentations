## General Thread Tips

* VS2013 has even better support for debugging threading than 2012
* If you're using Parallel.ForEach and await at the same time, you're probably doing it wrong

<..>

## Closing Tips

* TPL is for parallelism
* Async/await is for reducing blocking
* Reducing blocking can help with parallelism/throughput, but at the macro level\*
* Between TPL and async/await, you're probably rarely going to need TPL...
* Measure performance! Do you need those additional threads? And remember scalability, too!

Note:

* For example, if you don't have lots of ASP.NET threads blocking, IIS can send more requests through. If you do have lots of ASP.NET threads that are blocked, your server can't respond as quickly.
* How many LOB applications can really use parallelization? _Particularly if you're on the web._ Client-side may require more parallelization.
* Scalability is very important when talking about the web - if you're spinning up a lot of threads, you're hurting the overall performance of your site.

<..>

# Questions?

David Mohundro

[mohundro.com](http://mohundro.com)

[Presentation is available on Github](https://github.com/drmohundro/presentations)

![Laughing](./images/laughing.gif)
