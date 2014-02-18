## Closing Tips

* VS2013 has even better support for debugging threading than 2012
* In general, if you're using Parallel.ForEach and await at the same time, you're doing it wrong
* TPL is for parallelism
* Async/await is for reducing blocking
* Reducing blocking can help with parallelism/throughput, but at the macro level*

Note:
- For example, if you don't have lots of ASP.NET threads blocking, IIS can send more requests through. If you do have lots of ASP.NET threads that are blocked, your server can't respond as quickly.

<..>

# Questions?

David Mohundro

[mohundro.com](http://mohundro.com)

[Presentation is available on Github](https://github.com/drmohundro/presentations)

![Laughing](/images/laughing.gif)
