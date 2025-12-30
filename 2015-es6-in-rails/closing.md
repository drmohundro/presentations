# Closing Thoughts

<..>

Am I really suggesting using this over CoffeeScript?

Maybe.

<..>

Existential checks still stink...

```javascript
if (typeof x !== "undefined") {
  // this is still gross
}
```

versus...

```coffeescript
if x?
```

<..>

But... transpilers allow us to use ES7 features, too! ES7 will add `async`/`await` support.

It sort of depends on how fast you're willing to move!

<..>

# Questions?

David Mohundro

[mohundro.com](http://mohundro.com)

[Presentation is available on Github](https://github.com/drmohundro/presentations)

![Gasp!](./images/space-cat.gif)

Note:

* that image was used on a [BabelJS change log](https://github.com/babel/babel/blob/master/CHANGELOG.md) because it can compile itself now... awesome.
