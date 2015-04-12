# Swift (the language)

<..>

Swift was introduced by Apple at WWDC just *six months ago*.

<..>

Swift took language ideas

> ["from Objective-C, Rust, Haskell, Ruby, Python, C#, CLU, and far too many others to list."](http://nondot.org/sabre/)

(Swift language creator Chris Lattner)

<..>

Features include:

strong typing, implicit typing, mutable and immutable variables, OO, functional language features, generics, and more.

<-->

# Basics

<..>

## Variables

```swift
// string type, explicitly defined
var x: String = "Hello World"

// integer type, implicitly defined
var y = 5

// immutable!
let pi = 3.14159265359
```

Note:
Don't be confused, this *isn't* dynamic typing like in JavaScript; this is more akin to Haskell in that the compiler determines the type at compile time.

Also, immutable types __cannot__ change. Show this off.

<..>

## Collections

```swift
var arr = [1,2,3]

let hash = [1:"a", 2:"b", 3:"c"]
```

Note:
Notice the methods that exist on the mutable array versus the immutable array. Same thing with the hash. The compiler will not let you mess up.

<..>

## Control Flow

```swift
for item in ["abc", "def", "ghi"] {
    if item.hasPrefix("d") {
        "Matched on \(item)"
    }
}
```

Note:
The other normal control flow statements exist, too, like while loops.

<..>

## Functions

```swift
func getFirstAndLastName(firstName:String, lastName:String) -> String {
    return "Your name is \(firstName) \(lastName)"
}
getFirstAndLastName("David", "Mohundro")
```

Note:
Strangely enough, the function format reminds me of VB more than anything (with the return type at the end)

<-->

# Advanced Features

<..>

## Classes

```swift
class Person {
    // no concept of "fields" like in C#, everything is considered a property... you can just add a getter, setter (and willSet or didSet)
    // still... a property without additional modifiers is just a field.
    var firstName: String
    var lastName: String
    var age: Int

    var fullName: String {
        get {
            return "\(self.firstName) \(self.lastName)"
        }
    }

    init(firstName:String, lastName:String, age:Int) {
        self.firstName = firstName
        self.lastName = lastName
        self.age = age
    }

    func eat(food:String, withDrink:String) {
        "\(self.fullName) is about to eat \(food) and enjoy \(withDrink) to drink"
    }
}
```

<..>

## Protocols

```swift
protocol CoolCat {
    func Greetings() -> String
}
```

<..>

## Enums

```swift
enum AwesomenessLevel {
    case Lame, Normal, Awesome
}

let level = AwesomenessLevel.Awesome
```

<..>

## Extensions

```swift
extension Int {
    func Double() -> Int {
        return self * 2
    }

    mutating func DoubleSelf() {
        self = self * 2
    }
}
```

<..>

## Generics

```swift
var genericArray = Array<Int>()
genericArray.append(5)
```

<..>

## Functional

```swift
let results = ["abc", "def", "ghi"]
  .filter { $0.hasPrefix("d") }
  .map { $0.uppercaseString }
```

Note:
Really begins to feel like a next-gen language with this.
